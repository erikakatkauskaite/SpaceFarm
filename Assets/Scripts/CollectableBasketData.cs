using UnityEngine;

[CreateAssetMenu(fileName = "collectableBasketData", menuName = "newCollectableBasketData")]
public class CollectableBasketData : ScriptableObject
{
    public CollectableData.CollectableType collectableType;
    public int collectableCount;
}
