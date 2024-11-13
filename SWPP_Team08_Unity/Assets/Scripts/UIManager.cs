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
    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject questionButton;
    public GameObject settingsButton;
    public GameObject closeButton;
    public GameObject gameOverImage;
    public GameObject stageClearImage;
    public GameObject gameRestartButton;

    private List<GameObject> gameButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Canvas Size
        float fixedAspectRatio = 9f / 16f;
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        if (currentAspectRatio > fixedAspectRatio) gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        else if (currentAspectRatio < fixedAspectRatio) gameObject.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;

        // Game Object Initialization
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = "x" + PlayerPrefs.GetInt("Score").ToString();
        progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();

        // If not setted (need activation)
        // backgroundColor = GameObject.Find("BackgroundColor");
        // backgroundColor.SetActive(false);

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

        // gameOverImage = GameObject.Find("GameOverImage");
        // gameOverImage.SetActive(false);
        // stageClearImage = GameObject.Find("StageClearImage");
        // stageClearImage.SetActive(false);
        // gameRestartButton = GameObject.Find("GameRestartButton");
        // gameRestartButton.SetActive(false);

        gameButtons = new List<GameObject>();
        gameButtons.Add(playButton);
        gameButtons.Add(pauseButton);
        gameButtons.Add(questionButton);
        gameButtons.Add(settingsButton);
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

    public void ShowGameOverUI()
    {
        backgroundColor.SetActive(true);
        gameOverImage.SetActive(true);
        gameRestartButton.SetActive(true);
    }

    public void ShowGameClearUI()
    {
        backgroundColor.SetActive(true);
        stageClearImage.SetActive(true);
    }

    public void OnPressPauseButton()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);
        // Code for pausing game
    }

    public void OnPressPlayButton()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        // Code for playing game
    }

    private void HideGameButtons()
    {
        backgroundColor.SetActive(true);
        foreach (GameObject button in gameButtons)
        {
            button.SetActive(false);
        }
        closeButton.SetActive(true);
    }

    private void ShowGameButtons()
    {
        backgroundColor.SetActive(false);
        foreach (GameObject button in gameButtons)
        {
            button.SetActive(true);
        }
        pauseButton.SetActive(false);
        closeButton.SetActive(false);
    }

    public void OnPressQuestionButton()
    {
        OnPressPauseButton();
        HideGameButtons();
        // TODO
    }
    
    public void OnPressSettingsButton()
    {
        OnPressPauseButton();
        HideGameButtons();
        // TODO
    }

    public void OnPressCloseButton()
    {
        ShowGameButtons();
    }
}
