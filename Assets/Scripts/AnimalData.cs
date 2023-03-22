using UnityEngine;

[CreateAssetMenu(fileName = "AnimalData", menuName = "newAnimalData")]
public class AnimalData : ScriptableObject
{
    public AnimalType animalType;

    public enum AnimalType
    {
        Chicken, Cow, Pig, Sheep
    }
}