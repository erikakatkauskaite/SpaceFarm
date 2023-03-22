using System;
using UnityEngine;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    public static event Action OnPlaySelected;
    public static event Action OnPlaySelectedForAnimations;
    public static event Action OnDeleteFarmSelected;
    public static event Action OnSaveNeeded;
    public static event Action OnLoadNeeded;
    public static event Action<string> OnFarmAdded;
    public static event Action OnQuitGame;


    [Header("Canvases")]
    [SerializeField]
    private GameObject mainMenuCanvas;

    [SerializeField]
    private GameObject settingsMenuCanvas;

    [Header("Settings UI")]
    [SerializeField]
    private GameObject backButton;

    [Header("Main Menu UI")]
    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject settingsButton;

    [SerializeField]
    private GameObject quitButton;

    [Header("Play UI")]
    [SerializeField]
    private GameObject farmsList;

    [SerializeField]
    private TextMeshProUGUI farmName;

    [SerializeField]
    private GameObject farmFrame;

    [SerializeField]
    private GameObject createNewButton;

    [SerializeField]
    private GameObject newFarmNameInput;

    [SerializeField]
    private GameObject addNewButton;

    [SerializeField]
    private TextMeshProUGUI prompt;

    [SerializeField]
    private GameObject promptCloud;

    [SerializeField]
    private GameObject promptParticles;

    [SerializeField]
    private GameObject deleteButton;

    [Header("Player data")]
    public PlayerData playerData;
    public SavingData savingData;

    private const string FARM_NAME_PROMPT   = "Don't forget to name your farm!";
    private const string EMPTY_STRING       = "";

    private void Start()
    {
        mainMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
        farmFrame.SetActive(false);
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        quitButton.SetActive(true);
        farmsList.SetActive(false);
        prompt.enabled = false;
        promptCloud.SetActive(false);
        prompt.text = FARM_NAME_PROMPT;
        promptParticles.SetActive(false);
        deleteButton.SetActive(false);
    }
    public void Play()
    {
        OnPlaySelectedForAnimations?.Invoke();
        if (savingData.IsFarmCreated())
        {   
            if (playerData.farmName == EMPTY_STRING)
            {
                EnableWhenFarmDoesNotExist();
            }
            else
            {
                OnLoadNeeded?.Invoke();
                addNewButton.SetActive(false);
                createNewButton.SetActive(false);
                newFarmNameInput.SetActive(false);
                farmsList.SetActive(true);
                farmName.SetText(playerData.farmName);
                farmFrame.SetActive(true);
                prompt.enabled = false;
                deleteButton.SetActive(true);
            }
        }
        else
        {
            EnableWhenFarmDoesNotExist();
        }

        DisableMainMenu();
    }

    public void EnableCreateNewFarm()
    {
        deleteButton.SetActive(false);
        newFarmNameInput.SetActive(true);
        addNewButton.SetActive(true);
        createNewButton.SetActive(false);
    }

    public void AddNewFarm()
    {
        if (newFarmNameInput.GetComponent<TMP_InputField>().text.Length > 0)
        {
            string _name = newFarmNameInput.GetComponent<TMP_InputField>().text;
            farmName.enabled = true;
            newFarmNameInput.SetActive(false);
            playerData.farmName = _name;
            OnSaveNeeded?.Invoke();
            farmName.SetText(_name);
            prompt.enabled = false;
            addNewButton.SetActive(false);
            deleteButton.SetActive(true);
            farmFrame.SetActive(true);
        }
        else
        {
            prompt.enabled = true;
            promptCloud.SetActive(true);
            promptParticles.SetActive(true);
            Invoke(nameof(SetCloudNotActive), 4f);
        }
    }

    public void EnableWhenFarmDoesNotExist()
    {
        newFarmNameInput.GetComponent<TMP_InputField>().text = EMPTY_STRING;
        addNewButton.SetActive(false);
        createNewButton.SetActive(true);
        newFarmNameInput.SetActive(false);
        farmsList.SetActive(true);
        farmName.enabled = false;
        prompt.enabled = false;
        deleteButton.SetActive(false);
        farmFrame.SetActive(false);
    }

    public void DisableMainMenu()
    {
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
    }
    public void DeleteCurrentFarm()
    {
        OnDeleteFarmSelected?.Invoke();
        EnableWhenFarmDoesNotExist();
    }

    public void LoadPlanetMap()
    {
        OnPlaySelected?.Invoke();
    }

    private void SetCloudNotActive()
    {
        promptParticles.SetActive(false);
    }

    public void OpenSettingsWindow()
    {
        settingsMenuCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void OpenMainMenu()
    {
        settingsMenuCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        OnQuitGame?.Invoke();
    }
}
