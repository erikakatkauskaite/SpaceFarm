using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static event Action OnButtonPressedSoundNeeded;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private SavingData savingData;

    public Toggle[] resolutionToggles;
    public int[] screenWidths;
    private int activeScreenIndex;

    private const int FULLSCREEN_RESOLUTION_INDEX = 3;

    private void Start()
    {
        savingData.LoadGame();
        activeScreenIndex = playerData.resolutionIndex;

        SetGameResolution();
    }

   private void SetGameResolution()
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            if (i == activeScreenIndex)
            {
                resolutionToggles[i].isOn = true;
                if (activeScreenIndex == 3)
                {
                    SetFullscreen();
                }
                else
                {
                    SetScreenResolution(activeScreenIndex);
                }
            }
        }
    }

    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenIndex = i;
            float _aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / _aspectRatio), false);
            playerData.resolutionIndex = activeScreenIndex;
            savingData.SaveGame();
        }
    }

    public void SetFullscreen()
    {
        if (resolutionToggles[FULLSCREEN_RESOLUTION_INDEX].isOn)
        {          
            activeScreenIndex = FULLSCREEN_RESOLUTION_INDEX;
            Resolution[] _allResolutions = Screen.resolutions;
            Resolution _maxResolution = _allResolutions[_allResolutions.Length - 1];
            Screen.SetResolution(_maxResolution.width, _maxResolution.height, true);
            playerData.resolutionIndex = activeScreenIndex;
            savingData.SaveGame();
        }      
    }
}
