using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Scene currentScene;
    private UIManager uiManager;
    private SpawnManager spawnManager;
    private PlayerController playerController;
    private bool isLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        uiManager = gameObject.GetComponent<UIManager>();
        spawnManager = gameObject.GetComponent<SpawnManager>();

        if (currentScene.name != "MainScene")
        {
            playerController = GameObject.Find("Duck").GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        if (!isLoading)
        {
            isLoading = true;
            if (playerController)
            {
                playerController.enabled = false;
            }
            switch (currentScene.name)
            {
                case "MainScene":
                    PlayerPrefs.SetInt("Score", 0);
                    PlayerPrefs.SetInt("Stage", 1);
                    StartCoroutine(LoadStage1());
                    break;
                case "Stage1Scene":
                    PlayerPrefs.SetInt("Stage", 2);
                    StartCoroutine(LoadStage2());
                    break;
                case "Stage2Scene":
                    PlayerPrefs.SetInt("Stage", 3);
                    StartCoroutine(LoadStage3());
                    break;
                case "Stage3Scene":
                    StartCoroutine(LoadEnding());
                    break;
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(currentScene.name);
    }

    IEnumerator LoadStage1()
    {
        uiManager.ShowStoryUI();
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene("Stage1Scene");
        spawnManager.SpawnStage1();
    }

    IEnumerator LoadStage2()
    {
        uiManager.ShowStageClearUI();
        yield return new WaitForSeconds(2.5f);
        uiManager.ShowStoryUI();
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene("Stage2Scene");
        spawnManager.SpawnStage2();
    }

    IEnumerator LoadStage3()
    {
        uiManager.ShowStageClearUI();
        yield return new WaitForSeconds(2.5f);
        uiManager.ShowStoryUI();
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene("Stage3Scene");
        spawnManager.SpawnStage3();
    }

    IEnumerator LoadEnding()
    {
        uiManager.ShowStageClearUI();
        yield return new WaitForSeconds(2.5f);
        uiManager.ShowStoryUI();
    }    
}
