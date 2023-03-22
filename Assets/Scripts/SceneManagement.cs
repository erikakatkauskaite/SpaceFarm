using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{   
    [SerializeField]
    private SavingData savingData;

    private const int MAIN_MENU_SCENE_ID            = 0;
    private const int PLANETS_MAP_SCENE_ID          = 1;
    private const int FIRST_PLANET_LEVEL_MAP_ID     = 2;
    private const int SELECTED_LEVEL_SCENE_ID       = 3;

    private void Start()
    {
        PlanetSelector.OnFirstPlanetSelected += LoadFirstPlanetLevelMap;
        PlanetSelector.OnSecondPlanetSelected += LoadFirstPlanetLevelMap;
        PlanetSelector.OnThirdPlanetSelected += LoadFirstPlanetLevelMap;
        LevelMenuUI.OnLoadLevelMapSelected += LoadFirstPlanetLevelMap;
        MainMenuUIManager.OnPlaySelected += LoadPlanetsMap;
        LevelSelector.OnLevelSelected += LoadSelectedLevel;
        LevelMenuUI.OnLoadNextLevelSelected += LoadSelectedLevel;
        LevelMenuUI.OnRepeatLevelSelected += LoadSelectedLevel;
        LevelMenuUI.OnMainMenuSelected += LoadMainMenu;
        LevelMenuUI.OnPlanetMapSelected += LoadPlanetsMap;
        LevelMenuUI.OnQuitSelected += QuitGame;
        MainMenuUIManager.OnQuitGame += QuitGame;
    }
     
    private void OnDestroy()
    {
        PlanetSelector.OnFirstPlanetSelected -= LoadFirstPlanetLevelMap;
        PlanetSelector.OnSecondPlanetSelected -= LoadFirstPlanetLevelMap;
        PlanetSelector.OnThirdPlanetSelected -= LoadFirstPlanetLevelMap;
        LevelMenuUI.OnLoadLevelMapSelected -= LoadFirstPlanetLevelMap;
        MainMenuUIManager.OnPlaySelected -= LoadPlanetsMap;
        LevelSelector.OnLevelSelected -= LoadSelectedLevel;
        LevelMenuUI.OnLoadNextLevelSelected -= LoadSelectedLevel;
        LevelMenuUI.OnRepeatLevelSelected -= LoadSelectedLevel;
        LevelMenuUI.OnMainMenuSelected -= LoadMainMenu;
        LevelMenuUI.OnPlanetMapSelected -= LoadPlanetsMap;
        LevelMenuUI.OnQuitSelected -= QuitGame;
        MainMenuUIManager.OnQuitGame -= QuitGame;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_ID);
    }
    private void LoadFirstPlanetLevelMap()
    {
        SceneManager.LoadScene(FIRST_PLANET_LEVEL_MAP_ID);
    }

    private void LoadPlanetsMap()
    {
        SceneManager.LoadScene(PLANETS_MAP_SCENE_ID);
    }

    private void LoadSelectedLevel()
    {       
        SceneManager.LoadScene(SELECTED_LEVEL_SCENE_ID);
    }

    public void SAVETEST()
    {
        savingData.SaveGame();
    }

    public void LOADTEST()
    {
        savingData.LoadGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
