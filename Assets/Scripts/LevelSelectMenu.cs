using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
public class LevelSelectMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public List<LevelData> availableLevels;

    private void Start()
    {
        foreach (LevelData level in availableLevels)
        {
            GameObject buttonGO = Instantiate(buttonPrefab, buttonContainer);
            buttonGO.GetComponentInChildren<TextMeshProUGUI>().text = level.levelName;


            buttonGO.GetComponent<Button>().onClick.AddListener(() => {
                LoadLevel(level);
            });
        }
    }

    public void LoadLevel(LevelData level)
    {
        GameManager.selectedLevel = level; // We'll set this in GameManager
        SceneManager.LoadScene(level.sceneName);
    }
}
