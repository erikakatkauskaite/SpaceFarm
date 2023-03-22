using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "newPlayerData")]
public class PlayerData : ScriptableObject
{
    public string farmName;
    public int currentPlanet;
    public int currentLevel;
    public int money;
    public int maxAvailablePlanet;
    public int maxAvailableLevel;
    public int resolutionIndex;
}
