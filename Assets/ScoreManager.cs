using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region SINGLETON
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
                if (_instance == null) Debug.LogError("No scoremanager instance found");
            }
            return _instance;
        }
    }
    #endregion

    private static int highScore;
    private int currentScore;

    public int HighScore { get { return highScore; } }
    public int CurrentScore { get { return currentScore; } }

    public Text scoreText;
    public Text scoreTextOnGameOver;
    public Text highScoreText;

    private void Start()
    {
        ResetScore();
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highScore = PlayerPrefs.GetInt("Highscore");
        }
    }

    private void ResetScore()
    {
        currentScore = 0;
        scoreText.text = $"{currentScore}";
    }

    public void AddScore()
    {
        currentScore++;
        scoreText.text = $"{currentScore}";
    }

    public void SetHighScore()
    {
        if(currentScore >= highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("Highscore", highScore);
        }

        scoreTextOnGameOver.text = $"Score : {currentScore}";
        highScoreText.text = $"Highscore : {highScore}";
    }
}
