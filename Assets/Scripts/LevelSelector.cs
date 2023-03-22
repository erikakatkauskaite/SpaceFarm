using System;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public static event Action OnLevelSelected;

    public SceneSettingsData selectedLevel;

    public int levelIndex;
    public bool isUnlocked;

    private void OnMouseUp()
    {
        SetSelectedLevelIndex();
        OnLevelSelected?.Invoke();

        if (Input.GetMouseButtonDown(0))
        {
            SetSelectedLevelIndex();
            OnLevelSelected?.Invoke();
        }
    }

    private void SetSelectedLevelIndex()
    {
        selectedLevel.currentLevelIndex = (levelIndex - 1);
    }

    public void LoadSelectedLevel()
    {
        SetSelectedLevelIndex();
        OnLevelSelected?.Invoke();
    }
}
