using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LevelLocker : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private PlanetData planetData;

    [SerializeField]
    private LevelSelector[] levels;

    [SerializeField]
    private GameObject[] levelImages;

    [SerializeField]
    private TextMeshProUGUI[] levelsText;

    [SerializeField]
    private GameObject[] levelLocks;    

    private void Start()
    {
        CheckIfLevelIsLocked();
    }

    private void Update()
    {
        CheckIfLevelIsLocked();
    }

    private void CheckIfLevelIsLocked()
    {

        for (int i = 0; i < planetData.levelsUnlocked.Length; i++)
        {
            if (!planetData.levelsUnlocked[i])
            {
                LockTheLevel(i);
            }
            else
            {
                UnlockTheLevel(i);
            }
        }
    }

    private void UnlockTheLevel(int index)
    {
        levelImages[index].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        levelImages[index].GetComponent<Button>().interactable = true;
        levelLocks[index].GetComponent<Image>().enabled = false;
        levelsText[index].enabled = true;
    }

    private void LockTheLevel(int index)
    {
        levelImages[index].GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        levelImages[index].GetComponent<Button>().interactable = false;
        levelLocks[index].GetComponent<Image>().enabled = true;
        levelsText[index].enabled = false;
    }
}
