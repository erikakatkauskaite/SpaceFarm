using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action OnLevelCompleted;
    public static event Action OnLevelFailed;
    public static event Action OnSaveNeeded;
    public static event Action OnLoadNeeded;
    public static event Action OnEnemySpawnNeeded;
    public static event Action OnLevelFailedSoundNeeded;

    [SerializeField]
    private LevelReqData[] levelReqData;

    [SerializeField]
    private SceneSettingsData selectedLevel;

    [SerializeField]
    PlanetData planetData;

    [SerializeField]
    private Canvas levelCompletedCanvas;

    [SerializeField]
    private Canvas levelFailedCanvas;

    public PlayerData playerData;

    private int milkCount;
    private int meatCount;
    private int eggCount;
    private int woolCount;
    private float timeCounted;

    private bool allMilkCollected;
    private bool allEggsCollected;
    private bool allMeatCollected;
    private bool allWoolCollected;
    private bool levelCompleted;

    private int levelIndex;
    private int levelsUnlocked;
    private int maxLevelIndex;

    private int maxEnemiesCount;
    private int currentEnemiesCount;
    private float enemySpawnTime;

    private const string MILK                    = "Milk";
    private const string EGGS                    = "Eggs";
    private const string MEAT                    = "Meat";
    private const string WOOl                    = "Wool";
    private const float ENEMY_SPAWN_TIME_MIN     = 0f;
    private const float ENEMY_SPAWN_TIME_MAX     = 30f;


    private void Start()
    {
        levelIndex = selectedLevel.currentLevelIndex;
        playerData.money = levelReqData[levelIndex].moneyGiven;

        OnSaveNeeded?.Invoke();
        OnLoadNeeded?.Invoke();

        currentEnemiesCount = 0;
        maxEnemiesCount = levelReqData[levelIndex].enemiesCount;

        milkCount = 0;
        meatCount = 0;
        eggCount = 0;
        woolCount = 0;
        timeCounted = 0f;

        allMilkCollected = false;
        allMeatCollected = false;
        allEggsCollected = false;
        allWoolCollected = false;
        levelCompleted = false;

        AddCollectable();
        SetEnemySpawnTime(10f, 20f);
        StartCoroutine(nameof(CallSpawnEnemy));
    }

    private void SetEnemySpawnTime(float min, float max)
    {
        enemySpawnTime = UnityEngine.Random.Range(min, max);
    }

    private void Update()
    {
        timeCounted = timeCounted + Time.deltaTime;
        CheckIfWon();
    }

    IEnumerator CallSpawnEnemy()
    {
        while (currentEnemiesCount < maxEnemiesCount)
        {
            currentEnemiesCount++;
            yield return new WaitForSeconds(enemySpawnTime);
            SetEnemySpawnTime(ENEMY_SPAWN_TIME_MIN, ENEMY_SPAWN_TIME_MAX);
            OnEnemySpawnNeeded?.Invoke();
        }
    }

    private void AddCollectable()
    {        
        Collectable.OnEggCollected += AddEgg;
        Collectable.OnMeatCollected += AddMeat;
        Collectable.OnMilkCollected += AddMilk;
        Collectable.OnWoolCollected += AddWool;
        Collectable.OnEggCollected += SetTrueIfAllCollectableTypeCollected;
        Collectable.OnMeatCollected += SetTrueIfAllCollectableTypeCollected;
        Collectable.OnMilkCollected += SetTrueIfAllCollectableTypeCollected;
        Collectable.OnWoolCollected += SetTrueIfAllCollectableTypeCollected;
    }

    private void OnDestroy()
    {
        Collectable.OnEggCollected -= AddEgg;
        Collectable.OnMeatCollected -= AddMeat;
        Collectable.OnMilkCollected -= AddMilk;
        Collectable.OnWoolCollected -= AddWool;
        Collectable.OnEggCollected -= SetTrueIfAllCollectableTypeCollected;
        Collectable.OnMeatCollected -= SetTrueIfAllCollectableTypeCollected;
        Collectable.OnMilkCollected -= SetTrueIfAllCollectableTypeCollected;
        Collectable.OnWoolCollected -= SetTrueIfAllCollectableTypeCollected;
    }

    private void CheckIfWon()
    {
        if(timeCounted <= levelReqData[levelIndex].time)
        {
            if (allEggsCollected && allMeatCollected && allMilkCollected && allWoolCollected && !levelCompleted)
            {
                planetData.levelsUnlocked[selectedLevel.currentLevelIndex] = true;
                Invoke(nameof(CallOnLevelCompleted), 1f);
            }
        }
        else
        {
            OnLevelFailedSoundNeeded?.Invoke();
            Invoke(nameof(CallOnLevelFailed), 0.1f);
        }        
    }

    private void CallOnLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }

    private void CallOnLevelFailed()
    {
        OnLevelFailed?.Invoke();
    }

    private void SetTrueIfAllCollectableTypeCollected()
    {
        allEggsCollected = CheckIfAllCollectableTypeCollected(eggCount, levelReqData[levelIndex].eggsNeededCount, EGGS);
        allMeatCollected = CheckIfAllCollectableTypeCollected(meatCount, levelReqData[levelIndex].meatNeededCount, MEAT);
        allMilkCollected = CheckIfAllCollectableTypeCollected(milkCount, levelReqData[levelIndex].milkNeededCount, MILK);
        allWoolCollected = CheckIfAllCollectableTypeCollected(woolCount, levelReqData[levelIndex].woolNeededCount, WOOl);
    }

    private bool CheckIfAllCollectableTypeCollected(int collectableCount, int levelRequiredCount, string collectableName)
    {
        if (collectableCount >= levelRequiredCount)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public void NextLevel()
    {
        selectedLevel.currentLevelIndex += 1;
    }
}
