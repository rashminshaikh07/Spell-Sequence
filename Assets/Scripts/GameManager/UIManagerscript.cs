using UnityEngine;
using TMPro;

public class UIManagerscript : MonoBehaviour
{
    public static UIManagerscript instance;

    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text roundText;
    public TMP_Text timerText;

    public GameObject gameOverPanel;

    void Awake()
    {
        instance = this;
    }

    public void UpdateUI(int score, int lives, int round)
    {
        UpdateScore(score);
        UpdateLives(lives);
        UpdateRound(round);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void UpdateRound(int round)
    {
        roundText.text = "Round: " + round;
    }

    public void UpdateTimer(float time)
    {
        if (timerText != null)
            timerText.text = "Time: " + Mathf.Ceil(time).ToString();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}