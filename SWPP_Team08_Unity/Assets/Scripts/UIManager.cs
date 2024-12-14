using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Slider progressBar;
    public GameObject backgroundColor;

    public GameObject gameStartButton;
    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject questionButton;
    public GameObject settingsButton;
    public GameObject closeButton;

    public GameObject questionDescription;

    public GameObject gameOverImage;
    public GameObject stageClearImage;
    public GameObject[] stageClearStories;

    public GameObject gameRestartButton;
    public GameObject stageRestartButton;
    public GameObject allStageRestartButton;
    public GameObject goToMainButton;
    
    public GameObject keyArrowButton;
    public GameObject keyArrowButtonDeactivated;
    public GameObject keyWASDButton;
    public GameObject keyWASDButtonDeactivated;

    public GameObject settingsPlay;
    public GameObject settingsKeyArrow;
    public GameObject settingsKeyWASD;
    public GameObject settingsSound;
    public GameObject soundSlider;
    public Slider soundSliderValue;

    private List<GameObject> gameButtons;
    private GameStateManager gameStateManager;
    private EffectManager effectManager;

    // Start is called before the first frame update
    void Start()
    {
        // Canvas Size
        float fixedAspectRatio = 9f / 16f;
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        if(currentAspectRatio > fixedAspectRatio)
        {
            gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        } else if(currentAspectRatio < fixedAspectRatio)
        {
            gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }

        // Game Object Initialization
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = "x" + PlayerPrefs.GetInt("Score").ToString();
        progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();

        // If not setted (need activation)
        // backgroundColor = GameObject.Find("BackgroundColor");
        // backgroundColor.SetActive(false);

        // gameStartButton = GameObject.Find("GameStartButton");
        // playButton = GameObject.Find("PlayButton");
        // playButton.SetActive(false);
        // pauseButton = GameObject.Find("PauseButton");
        // pauseButton.SetActive(false);
        // questionButton = GameObject.Find("QuestionButton");
        // questionButton.SetActive(false);
        // settingsButton = GameObject.Find("SettingsButton");
        // settingsButton.SetActive(false);
        // closeButton = GameObject.Find("CloseButton");
        // closeButton.SetActive(false);

        // questionDescription = GameObject.Find("QuestionDescription");
        // questionDescription.SetActive(false);

        // gameOverImage = GameObject.Find("GameOverImage");
        // gameOverImage.SetActive(false);
        // stageClearImage = GameObject.Find("StageClearImage");
        // stageClearImage.SetActive(false);
        // stageClearStory = GameObject.Find("StageClearStory");
        // stageClearStory.SetActive(false);
        // gameRestartButton = GameObject.Find("GameRestartButton");
        // gameRestartButton.SetActive(false);

        gameButtons = new List<GameObject>();
        gameButtons.Add(playButton);
        gameButtons.Add(pauseButton);
        gameButtons.Add(questionButton);
        gameButtons.Add(settingsButton);

        SetScale();
        
        gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();

        
        if(PlayerPrefs.GetInt("VolumeSetted") != 0)
        {
            soundSliderValue.value = PlayerPrefs.GetFloat("Volume");
        } else
        {
            soundSliderValue.value = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "x" + score.ToString();
    }

    public void UpdateProgressBar(float processRate)
    {
        progressBar.value = processRate;
    }

    public void ShowGamePlayUI()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void ShowGamePauseUI()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        DestroyItemUI();
        backgroundColor.SetActive(true);
        gameOverImage.SetActive(true);
        gameRestartButton.SetActive(true);
    }

    public void ShowStageClearUI()
    {
        DestroyItemUI();
        stageClearImage.SetActive(true);
    }

    public void ShowStoryUI(int number)
    {
        backgroundColor.SetActive(true);
        if(stageClearImage)
        {
            stageClearImage.SetActive(false);
        }
        if(gameStartButton)
        {
            gameStartButton.SetActive(false);
        }
        foreach(GameObject stageClearStory in stageClearStories)
        {
            stageClearStory.SetActive(false);
        }
        stageClearStories[number].SetActive(true);
    }

    public void ShowGoToMainButton()
    {
        goToMainButton.SetActive(true);
    }

    public void DestroyItemUI()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach(GameObject obj in allObjects)
        {
            if(obj.name == "BoostSlider" || obj.name == "FlySlider" || obj.name == "DoubleSlider")
            {
                Destroy(obj);
            }
        }
    }

    public void OnPressPauseButton()
    {
        effectManager.PlayUIClickSound2();
        gameStateManager.EnterGamePauseState();
    }

    public void OnPressPlayButton()
    {
        effectManager.PlayMainSound();
        gameStateManager.EnterGamePlayState();
    }

    private void HideGameButtons()
    {
        backgroundColor.SetActive(true);
        foreach(GameObject button in gameButtons)
        {
            button.SetActive(false);
        }
        closeButton.SetActive(true);
    }

    private void ShowGameButtons()
    {
        backgroundColor.SetActive(false);
        foreach(GameObject button in gameButtons)
        {
            button.SetActive(true);
        }
        pauseButton.SetActive(false);
        closeButton.SetActive(false);

        questionDescription.SetActive(false);
        
        if(SceneManager.GetActiveScene().name != "MainScene")
        {
            settingsPlay.SetActive(false);
            stageRestartButton.SetActive(false);
            allStageRestartButton.SetActive(false);
        }

        keyArrowButton.SetActive(false);
        keyArrowButtonDeactivated.SetActive(false);
        keyWASDButton.SetActive(false);
        keyWASDButtonDeactivated.SetActive(false);

        settingsSound.SetActive(false);
        soundSlider.SetActive(false);

        settingsKeyArrow.SetActive(false);
        settingsKeyWASD.SetActive(false);
    }

    public void OnPressQuestionButton()
    {
        OnPressPauseButton();
        HideGameButtons();
        questionDescription.SetActive(true);
    }
    
    public void OnPressSettingsButton()
    {
        OnPressPauseButton();
        HideGameButtons();
        
        if(SceneManager.GetActiveScene().name != "MainScene")
        {
            settingsPlay.SetActive(true);
            stageRestartButton.SetActive(true);
            allStageRestartButton.SetActive(true);
        }
        
        settingsSound.SetActive(true);
        soundSlider.SetActive(true);

        if(PlayerPrefs.GetString("key") != "WASD")
        {
            OnPressKeyArrowDeactivatedButton();
            effectManager.StopUIClickSound1();
        } else {
            OnPressKeyWASDDeactivatedButton();
            effectManager.StopUIClickSound1();
        }
    }

    public void OnPressCloseButton()
    {
        effectManager.PlayUIClickSound1();
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            Time.timeScale = 1.0f;
            effectManager.PlayMainSound();
        }
        ShowGameButtons();
    }

    public void OnPressKeyArrowDeactivatedButton()
    {
        PlayerPrefs.SetString("key", "Arrow");
        keyArrowButton.SetActive(true);
        keyArrowButtonDeactivated.SetActive(false);
        keyWASDButton.SetActive(false);
        keyWASDButtonDeactivated.SetActive(true);
        settingsKeyArrow.SetActive(true);
        settingsKeyWASD.SetActive(false);
        effectManager.PlayUIClickSound1();
    }

    public void OnPressKeyWASDDeactivatedButton()
    {
        PlayerPrefs.SetString("key", "WASD");
        keyWASDButton.SetActive(true);
        keyWASDButtonDeactivated.SetActive(false);
        keyArrowButton.SetActive(false);
        keyArrowButtonDeactivated.SetActive(true);
        settingsKeyArrow.SetActive(false);
        settingsKeyWASD.SetActive(true);
        effectManager.PlayUIClickSound1();
    }


    private void SetScale()
    {
        // float widthRatio = Screen.width / 1920.0f;
        // float heightRatio = Screen.height / 1080.0f;
        // float ratio = (widthRatio < heightRatio) ? widthRatio : heightRatio;

        // foreach (Transform element in gameObject.transform)
        // {
        //     RectTransform rect = element.gameObject.GetComponent<RectTransform>();
        //     if (rect != null)
        //     {
        //         rect.localPosition = rect.localPosition * ratio;
        //         rect.sizeDelta = rect.sizeDelta * ratio;
        //     }
        // }
        if(Screen.width <= 1000 || Screen.height <= 300)
        {
            questionDescription.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if(Screen.width <= 1100 || Screen.height <= 450)
        {
            questionDescription.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
        if(Screen.width >= 1300 && Screen.height >= 750)
        {
            questionDescription.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
        if(Screen.width >= 1400 && Screen.height >= 900)
        {
            questionDescription.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        }
        if(Screen.width >= 1500 && Screen.height >= 1050)
        {
            questionDescription.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        }
    }
}