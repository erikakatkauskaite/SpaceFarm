using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUI : MonoBehaviour
{
    public static event Action OnLoadLevelMapSelected;
    public static event Action OnLoadNextLevelSelected;
    public static event Action OnRepeatLevelSelected;
    public static event Action OnLevelCompleted;
    public static event Action OnPlanetMapSelected;
    public static event Action OnMainMenuSelected;
    public static event Action OnLevelFailedLoaded;
    public static event Action OnAnimalShopCheckNeeded;
    public static event Action OnQuitSelected;

    [Header("Canvases")]

    [SerializeField]
    private GameObject levelCompletedCanvas;

    [SerializeField]

    private GameObject settingsCanvas;
    [SerializeField]
    private TextMeshProUGUI timeLeftTextWin;

    [SerializeField]
    private GameObject levelFailedCanvas;

    [SerializeField]
    private TextMeshProUGUI timeLeftTextFail;

    [SerializeField]
    private GameObject levelBegginingCanvas;

    [SerializeField]
    private GameObject pauseCanvas;

    [SerializeField]
    private GameObject rocketCanvas;

    [Header("ForRequirementsUI")]
    [SerializeField]
    private GameObject[] requirementsHolders;

    [SerializeField]
    private GameObject[] requirementsImages;

    [SerializeField]
    private TextMeshProUGUI[] requirementsCurrentText;

    [SerializeField]
    private TextMeshProUGUI[] requirementsNeededText;

    [Header("ForLevelBegginingCanvasRequirements")]
    [SerializeField]
    private GameObject[] requirementsHolders2;

    [SerializeField]
    private GameObject[] requirementsImages2;

    [SerializeField]
    private TextMeshProUGUI[] requirementsNeededText2;

    [SerializeField]
    private TextMeshProUGUI timeRequired;

    [Header("Images")]
    [SerializeField]
    private Sprite eggImage;

    [SerializeField]
    private Sprite milkImage;

    [SerializeField]
    private Sprite meatImage;

    [SerializeField]
    private Sprite woolImage;

    [Header("Common")]
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private TextMeshProUGUI currentLevelText;

    [SerializeField]
    private SceneSettingsData sceneSettings;

    [SerializeField]
    private LevelReqData[] levelReqData;

    [SerializeField]
    private TextMeshProUGUI timePassedText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI coinText;

    [SerializeField]
    private TextMeshProUGUI scoreTextLevelFailed;

    [SerializeField]
    private TextMeshProUGUI coinTextLevelFailed;

    [Header("Animal Shop Buttons")]
    [SerializeField]
    private Button[] animalButtons;

    private int neededSlotsCount;
    private int currentLevel;

    private int milkCount;
    private int meatCount;
    private int eggCount;
    private int woolCount;

    private int eggSlot;
    private int meatSlot;
    private int milkSlot;
    private int woolSlot;

    private float timeRemaning;
    private int score;

    private const string LEVEL_STRING   = "Level ";
    private const string SLASH          = "/";
    private const string SECONDS        = " s.";
    private const string TIME           = "TIME: ";
    private const string SCORE          = "SCORE: ";
    private const int TIME_MULTIPLIER   = 100;
    private const string COINS          = "Coins: ";


    private void Start()
    {
        score = 0;
        currentLevel = sceneSettings.currentLevelIndex;
        timeRemaning = levelReqData[currentLevel].time;
        timePassedText.SetText(timeRemaning.ToString());
        timeRequired.SetText(levelReqData[currentLevel].time.ToString() + SECONDS);
        rocketCanvas.SetActive(false);

        RocketSelector.OnRocketSelected += ActivateRocketMenu;
        LevelManager.OnLevelCompleted += SetCurrectLevelInUI;
        LevelManager.OnLevelCompleted += EnableLevelCompletedCanvas;
        LevelManager.OnLevelFailed += EnableLevelFailedCanvas;
        Collectable.OnEggCollected += AddEgg;
        Collectable.OnEggCollected += UpdateRequirementsUIForEgg;
        Collectable.OnMeatCollected += AddMeat;
        Collectable.OnMeatCollected += UpdateRequirementsUIForMeat;
        Collectable.OnMilkCollected += AddMilk;
        Collectable.OnMilkCollected += UpdateRequirementsUIForMilk;
        Collectable.OnWoolCollected += AddWool;
        Collectable.OnWoolCollected += UpdateRequirementsUIForWool;
        CollectableBasket.OnGoodiesSentToEarth += BackToGame;
        AnimalShop.OnChickenDisableNeeded += DisableButtonAnimalIntheShop;
        AnimalShop.OnCowDisableNeeded += DisableButtonAnimalIntheShop;
        AnimalShop.OnPigDisableNeeded += DisableButtonAnimalIntheShop;
        AnimalShop.OnSheepDisableNeeded += DisableButtonAnimalIntheShop;
        AnimalShop.OnHippoDisableNeeded += DisableButtonAnimalIntheShop;
        AnimalShop.OnChickenEnableNeeded += EnableButtonAnimalIntheShop;
        AnimalShop.OnCowEnableNeeded += EnableButtonAnimalIntheShop;
        AnimalShop.OnPigEnableNeeded += EnableButtonAnimalIntheShop;
        AnimalShop.OnSheepEnableNeeded += EnableButtonAnimalIntheShop;
        AnimalShop.OnHippoEnableNeeded += EnableButtonAnimalIntheShop;

        milkCount = 0;
        meatCount = 0;
        eggCount = 0;
        woolCount = 0;

        eggSlot = 0;
        milkSlot = 0;
        meatSlot = 0;
        woolCount = 0;

        neededSlotsCount = 0;

        EnableLevelBegginingCanvas();
        DisableLevelFailedCanvas();
        DisableLevelCompletedCanvas();
        DisablePauseCanvas();
        SetCurrectLevelInUI();
        CheckHowManySlotsAreNeeded();
        RenderRequiredSlots();

        StopTimer();
    }

    private void OnDestroy()
    {
        RocketSelector.OnRocketSelected -= ActivateRocketMenu;
        LevelManager.OnLevelCompleted -= SetCurrectLevelInUI;
        LevelManager.OnLevelCompleted -= EnableLevelCompletedCanvas;
        LevelManager.OnLevelFailed -= EnableLevelFailedCanvas;
        Collectable.OnEggCollected -= AddEgg;
        Collectable.OnEggCollected -= UpdateRequirementsUIForEgg;
        Collectable.OnMeatCollected -= AddMeat;
        Collectable.OnMeatCollected -= UpdateRequirementsUIForMeat;
        Collectable.OnMilkCollected -= AddMilk;
        Collectable.OnMilkCollected -= UpdateRequirementsUIForMilk;
        Collectable.OnWoolCollected -= AddWool;
        Collectable.OnWoolCollected -= UpdateRequirementsUIForWool;
        CollectableBasket.OnGoodiesSentToEarth -= BackToGame;
        AnimalShop.OnChickenDisableNeeded -= DisableButtonAnimalIntheShop;
        AnimalShop.OnCowDisableNeeded -= DisableButtonAnimalIntheShop;
        AnimalShop.OnPigDisableNeeded -= DisableButtonAnimalIntheShop;
        AnimalShop.OnSheepDisableNeeded -= DisableButtonAnimalIntheShop;
        AnimalShop.OnHippoDisableNeeded -= DisableButtonAnimalIntheShop;
        AnimalShop.OnChickenEnableNeeded -= EnableButtonAnimalIntheShop;
        AnimalShop.OnCowEnableNeeded -= EnableButtonAnimalIntheShop;
        AnimalShop.OnPigEnableNeeded -= EnableButtonAnimalIntheShop;
        AnimalShop.OnSheepEnableNeeded -= EnableButtonAnimalIntheShop;
        AnimalShop.OnHippoEnableNeeded -= EnableButtonAnimalIntheShop;
    }

    private void Update()
    {
        timeRemaning = timeRemaning - Time.deltaTime;
        timePassedText.SetText(timeRemaning.ToString("F0"));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopTimer();
            EnablePauseCanvas();
        }
    }

    private void EnableButtonAnimalIntheShop(int buttonIndex)
    {
        animalButtons[buttonIndex].GetComponent<Image>().color = Color.white;
    }

    private void DisableButtonAnimalIntheShop(int buttonIndex)
    {
        animalButtons[buttonIndex].GetComponent<Image>().color = Color.grey;
    }

    private void RenderRequiredSlots()
    {
        for (int i = 0; i < requirementsHolders.Length; i++)
        {
            requirementsHolders[i].SetActive(false);

            if (i < neededSlotsCount )
            {
                requirementsHolders[i].SetActive(true);
                requirementsHolders2[i].SetActive(true);
            }
            else
            {
                requirementsHolders[i].SetActive(false);
                requirementsHolders2[i].SetActive(false);
            }
        } 
    }

    private void SetTheRequiredCollectable(out int collectableSlot, int collecatbleAmount, int requiredAmountOfCollectable, Sprite collectableImage)
    {
        neededSlotsCount++;
        collectableSlot = neededSlotsCount - 1;
        requirementsImages[collectableSlot].GetComponent<UnityEngine.UI.Image>().sprite = collectableImage;
        requirementsImages2[collectableSlot].GetComponent<UnityEngine.UI.Image>().sprite = collectableImage;
        requirementsNeededText[collectableSlot].SetText(requiredAmountOfCollectable.ToString());
        requirementsNeededText2[collectableSlot].SetText(requiredAmountOfCollectable.ToString());
        requirementsCurrentText[collectableSlot].SetText(collecatbleAmount + SLASH);
    }

    private void CheckHowManySlotsAreNeeded()
    {
        int _eggsNeeded = levelReqData[currentLevel].eggsNeededCount;
        int _milkNeeded = levelReqData[currentLevel].milkNeededCount;
        int _meatNeeded = levelReqData[currentLevel].meatNeededCount;
        int _woolNeeded = levelReqData[currentLevel].woolNeededCount;

        if (IsCollectableRequired(_eggsNeeded))
        {
            SetTheRequiredCollectable(out eggSlot, eggCount, _eggsNeeded, eggImage);
        }

        if (IsCollectableRequired(_milkNeeded))
        {
            SetTheRequiredCollectable(out milkSlot, milkCount, _milkNeeded, milkImage);
        }

        if (IsCollectableRequired(_meatNeeded))
        {
            SetTheRequiredCollectable(out meatSlot, meatCount, _meatNeeded, meatImage);
        }

        if (IsCollectableRequired(_woolNeeded))
        {
            SetTheRequiredCollectable(out woolSlot, woolCount, _woolNeeded, woolImage);
        }
    }

    private bool IsCollectableRequired(int collectableAmountNeeded)
    {
        if (collectableAmountNeeded > 0)
        {
            return true;
        }
        else return false;
    }

    private void UpdateRequirementsUIForEgg()
    {
        if (levelReqData[currentLevel].eggsNeededCount > 0)
        {
            requirementsCurrentText[eggSlot].SetText(eggCount + SLASH);
        }
    }

    private void UpdateRequirementsUIForMilk()
    {
        if (levelReqData[currentLevel].milkNeededCount > 0)
        {
            requirementsCurrentText[milkSlot].SetText(milkCount + SLASH);
        }
    }

    private void UpdateRequirementsUIForMeat()
    {
        if(levelReqData[currentLevel].meatNeededCount > 0)
        {
            requirementsCurrentText[meatSlot].SetText(meatCount + SLASH);
        }
    }

    private void UpdateRequirementsUIForWool()
    {
        if (levelReqData[currentLevel].woolNeededCount > 0)
        {
            requirementsCurrentText[woolSlot].SetText(woolCount + SLASH);
        }
    }

    private void SetCurrectLevelInUI()
    {
        currentLevelText.SetText(LEVEL_STRING + (currentLevel + 1));
    }

    private void EnableLevelBegginingCanvas()
    {
        levelBegginingCanvas.SetActive(true);
    }

    private void DisableLevelBegginingCanvas()
    {
        levelBegginingCanvas.SetActive(false);
    }

    private void EnableLevelCompletedCanvas()
    {
        int _timeRemainingInt = Convert.ToInt32(timeRemaning);
        int _money = playerData.money;
        coinText.SetText(COINS + _money.ToString());
        score = _timeRemainingInt * TIME_MULTIPLIER + _money ;
        scoreText.SetText(SCORE + score.ToString());
        levelCompletedCanvas.SetActive(true);
        SetTimeResult(timeLeftTextWin);
        StopTimer();
        OnLevelCompleted?.Invoke();
    }

    private void EnableLevelFailedCanvas()
    {
        int _money = playerData.money;
        coinTextLevelFailed.SetText(COINS + _money.ToString());
        score = _money;
        scoreTextLevelFailed.SetText(SCORE + score.ToString());
        OnLevelFailedLoaded?.Invoke();
        levelFailedCanvas.SetActive(true);
        SetTimeResult(timeLeftTextFail);
        StopTimer();
    }

    private void ActivateRocketMenu()
    {
        rocketCanvas.SetActive(true);
        StopTimer();
    }

    private void DisableLevelCompletedCanvas()
    {
        levelCompletedCanvas.SetActive(false);
    }

    private void DisableLevelFailedCanvas()
    {
        levelFailedCanvas.SetActive(false);
    }

    public void LoadLevelMap()
    {
        DisableLevelFailedCanvas();
        DisableLevelCompletedCanvas();
        OnLoadLevelMapSelected?.Invoke();
    }
    public void LoadNextLevel()
    {
        DisableLevelCompletedCanvas();
        sceneSettings.currentLevelIndex += 1;
        OnLoadNextLevelSelected?.Invoke();
    }

    public void RepeatCurrentLevel()
    {
        DisableLevelFailedCanvas();
        OnRepeatLevelSelected?.Invoke();     
    }

    public void LevelBegginingPlay()
    {
        DisableLevelBegginingCanvas();
        ResumeGame();
        OnAnimalShopCheckNeeded?.Invoke();
    }


    private void EnablePauseCanvas()
    {
        pauseCanvas.SetActive(true);
    }

    private void DisablePauseCanvas()
    {
        pauseCanvas.SetActive(false);
    }

    public void ResumeButton()
    {
        DisablePauseCanvas();
        ResumeGame();
    }

    public void MainMenuButton()
    {
        OnMainMenuSelected?.Invoke();
    }

    public void PlanetMapButton()
    {
        OnPlanetMapSelected?.Invoke();
    }

    public void QuitGame()
    {
        OnQuitSelected?.Invoke();
    }

    public void SettingsButton()
    {
        settingsCanvas.SetActive(true);
    }

    public void DisableSettingsCanvas()
    {
        settingsCanvas.SetActive(false);
    }

    public void BackToGame()
    {
        rocketCanvas.SetActive(false);
        ResumeGame();
    }

    private void SetTimeResult(TextMeshProUGUI timeText)
    {
        float _time = levelReqData[currentLevel].time - timeRemaning; 
        timeText.SetText(TIME + _time.ToString("F0") + SECONDS);
    }

    private void AddMilk()
    {
        milkCount++;
        
    }

    private void AddMeat()
    {
        meatCount++;
    }

    private void AddEgg()
    {
        eggCount++;
    }

    private void AddWool()
    {
        woolCount++;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void StopTimer()
    {
        Time.timeScale = 0f;
    }
}
