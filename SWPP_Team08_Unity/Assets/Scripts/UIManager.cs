using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject scoreText;
    private GameObject progressBar;
    private GameObject gameOverText;
    private GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText");
        progressBar = GameObject.Find("ProgressBar");
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);
        restartButton = GameObject.Find("RestartButton");
        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowGameOverUI();
    }

    void UpdateScoreText(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "x" + score.ToString();
    }

    void UpdateProgressBar(float distance)
    {
        // distance / totalDistance
    }

    void ShowGameOverUI()
    {
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
    }
}
