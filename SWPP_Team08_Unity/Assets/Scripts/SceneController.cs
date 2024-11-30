using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Scene currentScene;
    private UIManager uiManager;
    private PlayerController playerController;
    private bool isLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        uiManager = gameObject.GetComponent<UIManager>();

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
            // if (playerController)
            // {
            //     playerController.enabled = false;
            // }
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
        uiManager.ShowStoryUI(0);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(1);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(2);
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("Stage1Scene");
    }

    IEnumerator LoadStage2()
    {
        uiManager.ShowStageClearUI();
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(0);
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("Stage2Scene");
    }

    IEnumerator LoadStage3()
    {
        uiManager.ShowStageClearUI();
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(0);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(1);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(2);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(3);
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("Stage3Scene");
    }

    IEnumerator LoadEnding()
    {
        uiManager.ShowStageClearUI();
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(0);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(1);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(2);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(3);
        yield return new WaitForSeconds(4.0f);
        uiManager.ShowStoryUI(14);
        yield return new WaitForSeconds(1.5f);
        if (playerController.GetScore() >= 241)
        {
            uiManager.ShowStoryUI(4);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(5);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(8);
            yield return new WaitForSeconds(4.0f);
        } else if (playerController.GetScore() >= 161)
        {
            uiManager.ShowStoryUI(6);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(7);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(8);
            yield return new WaitForSeconds(4.0f);
        } else if (playerController.GetScore() >= 81)
        {
            uiManager.ShowStoryUI(9);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(11);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(12);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(13);
            yield return new WaitForSeconds(4.0f);
        } else
        {
            uiManager.ShowStoryUI(10);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(11);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(12);
            yield return new WaitForSeconds(4.0f);
            uiManager.ShowStoryUI(13);
            yield return new WaitForSeconds(4.0f);
        }
        uiManager.ShowReplayUI();
    }    
}
