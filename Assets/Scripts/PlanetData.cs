using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "newPlanetData")]
public class PlanetData : ScriptableObject
{
    public int planetNumber;
    public bool isPlanetLocked;

    public bool[] levelsUnlocked;
}
