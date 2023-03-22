using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableBasket : MonoBehaviour
{
    public static event Action<int> OnEggAdded = delegate { };
    public static event Action<int> OnMilkAdded = delegate { };
    public static event Action<int> OnMeatAdded = delegate { };
    public static event Action<int> OnWoolAdded = delegate { };

    public static event Action OnSaveDataRequired;
    public static event Action OnLoadDataRequired;
    public static event Action OnGoodiesSentToEarth;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    [Header("Baskets")]
    [SerializeField]
    private CollectableBasketData eggsBasketData;

    [SerializeField]
    private CollectableBasketData milkBasketData;

    [SerializeField]
    private CollectableBasketData meatBasketData;

    [SerializeField]
    private CollectableBasketData woolBasketData;

    [Header("Collectable elements")]
    [SerializeField]
    private TextMeshProUGUI[] collectableTotalCount;

    [SerializeField]
    private Button[] collectablePlusButton;

    [SerializeField]
    private int[] collectableCount;

    [SerializeField]
    private int[] collectableCountForSelling;

    [SerializeField]
    private TextMeshProUGUI[] collectableCurrentSelected;

    [SerializeField]
    private TextMeshProUGUI[] collectableCurrentSelected2;

    [SerializeField]
    private TextMeshProUGUI moneyForGoodiesText;

    [SerializeField]
    private GameObject coinVFX;

    private int moneyForGoodies;
    private int allMoney;

    private const int EGG_INDEX         = 0;
    private const int MILK_INDEX        = 1;
    private const int MEAT_INDEX        = 2;
    private const int WOOL_INDEX        = 3;
    private const int MONEY_PER_EGG     = 10;
    private const int MONEY_PER_MILK    = 20;
    private const int MONEY_PER_MEAT    = 50;
    private const int MONEY_PER_WOOL    = 100;

    private void Start()
    {
        coinVFX.SetActive(false);
        moneyText.SetText(playerData.money.ToString());

        CountMoneyPerCurrentSelling(); 
    }

    private void OnDestroy()
    {
        AnimalShop.OnNewAnimalBought -= UpdateMoneyUI;
    }

    private void CollectMoney()
    {
        OnLoadDataRequired?.Invoke();
        allMoney = playerData.money;

        if (moneyForGoodies > 0)
        {
            OnGoodiesSentToEarth?.Invoke();
            coinVFX.SetActive(true);
        }
        allMoney += moneyForGoodies;
        playerData.money = allMoney;
        moneyText.SetText(playerData.money.ToString());
        moneyForGoodies = 0;
        moneyForGoodiesText.SetText(moneyForGoodies.ToString());

        OnSaveDataRequired?.Invoke();
        OnLoadDataRequired?.Invoke();
    }

    private void UpdateMoneyUI()
    {
        allMoney = playerData.money;
        moneyText.SetText(allMoney.ToString());
    }

    private void SetRocketCanvas()
    {
        for (int i = 0; i < collectableCount.Length; i++)
        {
            for (int j = 0; j < collectableTotalCount.Length; j++)
            {
                if (i == j)
                {
                    collectableTotalCount[j].SetText("/" + collectableCount[i].ToString());
                    collectableCurrentSelected[j].SetText(collectableCountForSelling[i].ToString());
                    collectableCurrentSelected2[j].SetText(collectableCountForSelling[i].ToString());
                }
            }
        }
    }

    public void SellGoodies()
    {
        for (int i = 0; i < collectableCount.Length; i++)
        {
            collectableCount[i] = collectableCount[i] - collectableCountForSelling[i];
            collectableCountForSelling[i] = 0;
            SetRocketCanvas();
        }
        CollectMoney();
    }

    public void IncreaseCollectableCount(int collectableIndex)
    {
        for (int i = 0; i < collectablePlusButton.Length; i++)
        {
            if (i == collectableIndex)
            {
                int _maxCount = collectableCount[i];
                if (collectableCountForSelling[i] < _maxCount)
                {
                    collectableCountForSelling[i]++;
                    collectableCurrentSelected[i].SetText(collectableCountForSelling[i].ToString());
                    collectableCurrentSelected2[i].SetText(collectableCountForSelling[i].ToString());
                }
            }
        }
        CountMoneyPerCurrentSelling();
    }

    public void DecreaseCollectableCount(int collectableIndex)
    {
        for (int i = 0; i < collectablePlusButton.Length; i++)
        {
            if (i == collectableIndex)
            {
                int _minCount = 0;

                if (collectableCountForSelling[i] > _minCount)
                {
                    collectableCountForSelling[i]--;
                    collectableCurrentSelected[i].SetText(collectableCountForSelling[i].ToString());
                    collectableCurrentSelected2[i].SetText(collectableCountForSelling[i].ToString());
                }
            }
        }
        CountMoneyPerCurrentSelling();
    }

    private void CountMoneyPerCurrentSelling()
    {
        moneyForGoodies = collectableCountForSelling[EGG_INDEX] * MONEY_PER_EGG +
                        collectableCountForSelling[MILK_INDEX] * MONEY_PER_MILK +
                        collectableCountForSelling[MEAT_INDEX] * MONEY_PER_MEAT +
                        collectableCountForSelling[WOOL_INDEX] * MONEY_PER_WOOL;

        moneyForGoodiesText.SetText(moneyForGoodies.ToString());
    }

    private void SetMaxCollectableCountToZero()
    {
        for (int i = 0; i < collectableCount.Length; i++)
        {
            collectableCount[i] = 0;
        }
    }

    private void SetSelectedCollectableCountToZero()
    {
        for (int i = 0; i < collectableCount.Length; i++)
        {
            collectableCountForSelling[i] = 0;
        }
    }


    private void AddCollectable()
    {
        Collectable.OnEggCollected += AddEgg;
        Collectable.OnMeatCollected += AddMeat;
        Collectable.OnMilkCollected += AddMilk;
        Collectable.OnWoolCollected += AddWool;
    }

    private void AddMilk()
    {
        collectableCount[MILK_INDEX]++;
        milkBasketData.collectableCount = collectableCount[MILK_INDEX];
        SetRocketCanvas();
    }

    private void AddMeat()
    {
        collectableCount[MEAT_INDEX]++;
        meatBasketData.collectableCount = collectableCount[MEAT_INDEX];
        SetRocketCanvas();
    }

    private void AddEgg()
    {
        collectableCount[EGG_INDEX]++;
        eggsBasketData.collectableCount = collectableCount[EGG_INDEX];
        SetRocketCanvas();
    }

    private void AddWool()
    {
        collectableCount[WOOL_INDEX]++;
        woolBasketData.collectableCount = collectableCount[WOOL_INDEX];
        SetRocketCanvas();
    }
}
