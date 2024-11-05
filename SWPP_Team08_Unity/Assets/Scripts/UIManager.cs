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
    private GameObject gameOverText;
    private GameObject gameClearText;
    private GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = "x" + PlayerPrefs.GetInt("Score").ToString();
        progressBar = GameObject.Find("ProgressBar").GetComponent<Slider>();
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);
        gameClearText = GameObject.Find("GameClearText");
        gameClearText.SetActive(false);
        restartButton = GameObject.Find("RestartButton");
        restartButton.SetActive(false);
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
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
    }

    public void ShowGameClearUI()
    {
        gameClearText.SetActive(true);
    }
}
