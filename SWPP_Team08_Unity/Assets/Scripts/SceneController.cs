using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Scene currentScene;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        uiManager = gameObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        switch (currentScene.name)
        {
            case "MainScene":
                PlayerPrefs.SetInt("Score", 0);
                PlayerPrefs.SetInt("Stage", 1);
                SceneManager.LoadScene("Stage1Scene");
                break;
            case "Stage1Scene":
                PlayerPrefs.SetInt("Stage", 2);
                SceneManager.LoadScene("Stage2Scene");
                break;
            case "Stage2Scene":
                PlayerPrefs.SetInt("Stage", 3);
                SceneManager.LoadScene("Stage3Scene");
                break;
            case "Stage3Scene":
                uiManager.ShowGameClearUI();
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(currentScene.name);
    }
}
