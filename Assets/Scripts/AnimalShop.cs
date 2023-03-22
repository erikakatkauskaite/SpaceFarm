using System;
using UnityEngine;

public class AnimalShop : MonoBehaviour
{
    public static event Action OnSaveDataRequired;
    public static event Action OnLoadDataRequired;
    public static event Action OnNewAnimalBought;
    public static event Action OnChickenBought;
    public static event Action OnCowBought;
    public static event Action OnPigBought;
    public static event Action OnSheepBought;
    public static event Action OnHippoBought;

    public static event Action<int> OnChickenDisableNeeded = delegate { };
    public static event Action<int> OnCowDisableNeeded = delegate { };
    public static event Action<int> OnPigDisableNeeded = delegate { };
    public static event Action<int> OnSheepDisableNeeded = delegate { };
    public static event Action<int> OnHippoDisableNeeded = delegate { };

    public static event Action<int> OnChickenEnableNeeded = delegate { };
    public static event Action<int> OnCowEnableNeeded = delegate { };
    public static event Action<int> OnPigEnableNeeded = delegate { };
    public static event Action<int> OnSheepEnableNeeded = delegate { };
    public static event Action<int> OnHippoEnableNeeded = delegate { };

    [SerializeField]
    private GameObject[] animalCages;

    [SerializeField]
    private PlayerData playerData;

    private int money;

    private const int CHICKEN_INDEX         = 0;
    private const int COW_INDEX             = 1;
    private const int PIG_INDEX             = 2;
    private const int SHEEP_INDEX           = 3;
    private const int CHICKEN_PRICE         = 100;
    private const int COW_PRICE             = 200;
    private const int PIG_PRICE             = 500;
    private const int SHEEP_PRICE           = 1000;
    private const int HIPPO_PRICE           = 1500;

    private void Start()
    {
        DisableCages();
        OnLoadDataRequired?.Invoke();
        money = playerData.money;

        LevelMenuUI.OnAnimalShopCheckNeeded +=EnableAndDisableAnimalShopButtons;
        CollectableBasket.OnGoodiesSentToEarth += EnableAndDisableAnimalShopButtons;
    }

    private void OnDestroy()
    {
        LevelMenuUI.OnAnimalShopCheckNeeded -= EnableAndDisableAnimalShopButtons;
        CollectableBasket.OnGoodiesSentToEarth -= EnableAndDisableAnimalShopButtons;
    }

    private void DisableCages()
    {
        for (int i = 0; i < animalCages.Length; i++)
        {
            animalCages[i].SetActive(false);
        }
    }

    private void EnableAndDisableAnimalShopButtons()
    {
        money = playerData.money;
        if (money < CHICKEN_PRICE)
        {
            OnChickenDisableNeeded(CHICKEN_INDEX);
        }
        else
        {
            OnChickenEnableNeeded(CHICKEN_INDEX);
        }
        if(money < COW_PRICE)
        {
            OnCowDisableNeeded(COW_INDEX);
        }
        else
        {
            OnCowEnableNeeded(COW_INDEX);
        }
        if(money < PIG_PRICE)
        {
            OnPigDisableNeeded(PIG_INDEX);
        }
        else
        {
            OnPigEnableNeeded(PIG_INDEX);
        }
        if (money < SHEEP_PRICE)
        {
            OnSheepDisableNeeded(SHEEP_INDEX);
        }
        else
        {
            OnSheepEnableNeeded(SHEEP_INDEX);
        }
    }

    public void BuyChicken()
    {
        money = playerData.money;
        if (CheckIfEnoughMoney(CHICKEN_PRICE))
        {
            animalCages[CHICKEN_INDEX].SetActive(true);
            money = money - CHICKEN_PRICE;
            playerData.money = money;
            OnSaveDataRequired?.Invoke();
            OnLoadDataRequired?.Invoke();
            OnNewAnimalBought?.Invoke();
            OnChickenBought?.Invoke();
            EnableAndDisableAnimalShopButtons();
        }
    }

    public void BuyCow()
    {
        money = playerData.money;
        if (CheckIfEnoughMoney(COW_PRICE))
        {
            animalCages[COW_INDEX].SetActive(true);
            money = money - COW_PRICE;
            playerData.money = money;
            OnSaveDataRequired?.Invoke();
            OnLoadDataRequired?.Invoke();
            OnNewAnimalBought?.Invoke();
            OnCowBought?.Invoke();
            EnableAndDisableAnimalShopButtons();
        }
    }

    public void BuyPig()
    {
        money = playerData.money;
        if (CheckIfEnoughMoney(PIG_PRICE))
        {
            animalCages[PIG_INDEX].SetActive(true);
            money = money - PIG_PRICE;
            playerData.money = money;
            OnSaveDataRequired?.Invoke();
            OnLoadDataRequired?.Invoke();
            OnNewAnimalBought?.Invoke();
            OnPigBought?.Invoke();
            EnableAndDisableAnimalShopButtons();
        }
    }

    public void BuySheep()
    {
        money = playerData.money;
        if (CheckIfEnoughMoney(SHEEP_PRICE))
        {
            animalCages[SHEEP_INDEX].SetActive(true);
            money = money - SHEEP_PRICE;
            playerData.money = money;
            OnSaveDataRequired?.Invoke();
            OnLoadDataRequired?.Invoke();
            OnNewAnimalBought?.Invoke();
            OnSheepBought?.Invoke();
        }
    }

    private bool CheckIfEnoughMoney(int animalPrice)
    {
        if (animalPrice <= money)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
