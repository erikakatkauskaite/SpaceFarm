using UnityEngine;

[CreateAssetMenu(fileName = "LevelRequirementData", menuName = "NewLevelRequirementData")]
public class LevelReqData : ScriptableObject
{
    public int level;
    public int milkNeededCount;
    public int eggsNeededCount;
    public int meatNeededCount;
    public int woolNeededCount;
    public float time;
    public int moneyGiven;
    public int enemiesCount;
}
