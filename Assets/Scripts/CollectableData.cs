using UnityEngine;

[CreateAssetMenu(fileName = "CollectableData", menuName = "newCollectableData")]
public class CollectableData : ScriptableObject
{
    public CollectableType collectableType;

    public enum CollectableType
    {
        Egg, Milk, Meat, Wool
    }
}
