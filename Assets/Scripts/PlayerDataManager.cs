using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private SavingData savingData;

    [SerializeField]
    private PlanetData planetData1;

    [SerializeField]
    private PlanetData planetData2;

    [SerializeField]
    private PlanetData planetData3;

    [SerializeField]
    private SceneSettingsData sceneSettingsData;

    private const string EMPTY_STRING = "";

    private void Start()
    {
        MainMenuUIManager.OnDeleteFarmSelected += ResetPlayerData;
        MainMenuUIManager.OnSaveNeeded += SaveFarmData;
        MainMenuUIManager.OnLoadNeeded += LoadFarmData;
        LevelMenuUI.OnLoadNextLevelSelected += LevelUp;
    }

    private void OnDestroy()
    {
        MainMenuUIManager.OnDeleteFarmSelected -= ResetPlayerData;
        MainMenuUIManager.OnSaveNeeded -= SaveFarmData;
        MainMenuUIManager.OnLoadNeeded -= LoadFarmData;
        LevelMenuUI.OnLoadNextLevelSelected -= LevelUp;
    }

    private void ResetPlayerData()
    {
        playerData.farmName = EMPTY_STRING;
        playerData.currentLevel = 0;
        playerData.money = 0;
        playerData.currentPlanet = 0;
        playerData.maxAvailableLevel = 0;
        playerData.maxAvailablePlanet = 0;

        ResetLevelsData(planetData1);
        ResetLevelsData(planetData2);
        ResetLevelsData(planetData3);

        ResetPlanetData(planetData2);
        ResetPlanetData(planetData3);
    }

    private void SaveFarmData()
    {
        savingData.SaveGame();
    }

    private void LoadFarmData()
    {
        savingData.LoadGame();
    }

    private void ResetLevelsData(PlanetData planetData)
    {
        for(int i = 1; i < planetData.levelsUnlocked.Length; i++)
        {
            planetData.levelsUnlocked[i] = false;
        }
    }

    private void ResetPlanetData(PlanetData planetData)
    {
        planetData.isPlanetLocked = true;
    }

    private void LevelUp()
    {
        sceneSettingsData.currentLevelIndex++;

        for (int i=0; i <= planetData1.levelsUnlocked.Length; i++)
        {
            if(i == sceneSettingsData.currentLevelIndex)
            {
                planetData1.levelsUnlocked[i] = true;
            }
        }
    }
}
