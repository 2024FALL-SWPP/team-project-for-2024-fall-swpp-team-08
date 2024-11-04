using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        switch (currentScene.name)
        {
            case "MainScene":
                SceneManager.LoadScene("Stage1Scene");
                break;
            case "Stage1Scene":
                SceneManager.LoadScene("Stage2Scene");
                break;
            case "Stage2Scene":
                SceneManager.LoadScene("Stage3Scene");
                break;
            case "Stage3Scene":
                break;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1Scene");
    }
}
