using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SavingData : MonoBehaviour
{
    public static event Action OnFarmDataDeleted;
    public static SavingData instance;

    [SerializeField]
    private PlayerData playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        MainMenuUIManager.OnDeleteFarmSelected += DeleteFarm;
        CollectableBasket.OnSaveDataRequired += SaveGame;
        CollectableBasket.OnLoadDataRequired += LoadGame;
        AnimalShop.OnSaveDataRequired += SaveGame;
        AnimalShop.OnLoadDataRequired += LoadGame;
        LevelManager.OnLoadNeeded += LoadGame;
        LevelManager.OnSaveNeeded += SaveGame;
    }

    private void OnDestroy()
    {
        MainMenuUIManager.OnDeleteFarmSelected -= DeleteFarm;
        CollectableBasket.OnSaveDataRequired -= SaveGame;
        CollectableBasket.OnLoadDataRequired -= LoadGame;
        AnimalShop.OnSaveDataRequired -= SaveGame;
        AnimalShop.OnLoadDataRequired -= LoadGame;
        LevelManager.OnLoadNeeded -= LoadGame;
        LevelManager.OnSaveNeeded -= SaveGame;
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "game_save");
    }

    public bool IsFarmCreated()
    {
        return File.Exists(Application.persistentDataPath + "/game_save/player_data/player_saved.txt");
    }

    public void DeleteFarm()
    {
        File.Delete(Application.persistentDataPath + "/game_save/player_data/player_saved.txt");
    }

    public void SaveGame()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/player_data");
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/game_save/player_data/player_saved.txt");

        var json = JsonUtility.ToJson(playerData);
        binaryFormatter.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/player_data");
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/game_save/player_data/player_saved.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/player_data/player_saved.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), playerData);
            file.Close();
        }
    }
}
