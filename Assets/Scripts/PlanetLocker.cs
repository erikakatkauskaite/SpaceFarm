using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetLocker : MonoBehaviour
{
    [SerializeField]
    private PlanetData[] planetData;

    [SerializeField]
    private GameObject[] planetSprite;

    [SerializeField]
    private GameObject[] planetLockSprite;

    private void Start()
    {
        UnlockThePlanet();
        CheckIfPlanetIsLocked();
    }

    private void UnlockThePlanet()
    {
        for (int i = 0; i < planetData.Length; i++)
        {
            for (int j = 0; j < planetData[i].levelsUnlocked.Length; j++)
            {
                if (j == 23)
                {
                    if (planetData[i].levelsUnlocked[j] == true)
                    {
                        planetData[i + 1].isPlanetLocked = false;
                    }
                }                
            }
        }
    }

    private void CheckIfPlanetIsLocked()
    {
        for (int i = 0; i < planetData.Length; i++)
        {
            if (planetData[i].isPlanetLocked)
            {
                DoWhenPlanetIsLocked(i);
            }
            else
            {
                DoWhenPlanetIsUnlocked(i);
            }
        }
    }
    private void DoWhenPlanetIsLocked(int index)
    {
        planetSprite[index].GetComponent<Button>().interactable = false;
        planetSprite[index].GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        planetLockSprite[index].SetActive(true);
    }

    private void DoWhenPlanetIsUnlocked(int index)
    {
        planetSprite[index].GetComponent<Button>().interactable = true;
        planetSprite[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        planetLockSprite[index].SetActive(false);
    }

    private void Update()
    {
        UnlockThePlanet();
        CheckIfPlanetIsLocked();
    }
}
