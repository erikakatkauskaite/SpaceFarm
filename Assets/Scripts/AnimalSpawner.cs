using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] animals;

    [SerializeField]
    private GameObject[] animalSpawnPosition;

    private const int CHICKEN_INDEX         = 0;
    private const int COW_INDEX             = 1;
    private const int PIG_INDEX             = 2;
    private const int SHEEP_INDEX           = 3;
    private const float INSTANTIATE_OFFSET  = 0.3f;

    private void Start()
    {
        AnimalShop.OnChickenBought += InvokeInstantiateChicken;
        AnimalShop.OnCowBought += InvokeInstantiateCow;
        AnimalShop.OnPigBought += InvokeInstantiatePig;
        AnimalShop.OnSheepBought += InvokeInstantiateSheep;
    }

    private void OnDestroy()
    {
        AnimalShop.OnChickenBought -= InvokeInstantiateChicken;
        AnimalShop.OnCowBought -= InvokeInstantiateCow;
        AnimalShop.OnPigBought -= InvokeInstantiatePig;
        AnimalShop.OnSheepBought -= InvokeInstantiateSheep;
    }

    private void InstantiateChicken()
    {
        GameObject _newChicken = Instantiate(animals[CHICKEN_INDEX]) as GameObject;
        _newChicken.transform.position = animalSpawnPosition[CHICKEN_INDEX].transform.position;
    }

    private void InstantiateCow()
    {
        GameObject _newCow = Instantiate(animals[COW_INDEX]) as GameObject;
        _newCow.transform.position = animalSpawnPosition[COW_INDEX].transform.position;
    }

    private void InstantiatePig()
    {
        GameObject _newPig = Instantiate(animals[PIG_INDEX]) as GameObject;
        _newPig.transform.position = animalSpawnPosition[PIG_INDEX].transform.position;
    }

    private void InstantiateSheep()
    {
        GameObject _newSheep = Instantiate(animals[SHEEP_INDEX]) as GameObject;
        _newSheep.transform.position = animalSpawnPosition[SHEEP_INDEX].transform.position;
    }

    private void InvokeInstantiateChicken()
    {
        Invoke(nameof(InstantiateChicken), INSTANTIATE_OFFSET);
    }

    private void InvokeInstantiateCow()
    {
        Invoke(nameof(InstantiateCow), INSTANTIATE_OFFSET);
    }

    private void InvokeInstantiatePig()
    {
        Invoke(nameof(InstantiatePig), INSTANTIATE_OFFSET);
    }

    private void InvokeInstantiateSheep()
    {
        Invoke(nameof(InstantiateSheep), INSTANTIATE_OFFSET);
    }
}
