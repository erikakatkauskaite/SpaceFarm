using System;
using UnityEngine;

public class PlanetSelector : MonoBehaviour
{
    public static event Action OnFirstPlanetSelected;
    public static event Action OnSecondPlanetSelected;
    public static event Action OnThirdPlanetSelected;

    [SerializeField]
    private PlanetData planetData;

    public void OpenFirstPlanetLevelMap()
    {
        OnFirstPlanetSelected?.Invoke();
    }

    public void OpenSecondPlanetLevelMap()
    {
        OnSecondPlanetSelected?.Invoke();
    }

    public void OpenThirdPlanetLevelMap()
    {
        OnThirdPlanetSelected?.Invoke();
    }
}
