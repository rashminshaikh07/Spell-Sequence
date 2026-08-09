using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public int score = 0;
    public int lives = 3;
    public int round = 1;

    public float roundTime = 10f;
    private float timer;

    public bool gameRunning = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!gameRunning) return;

        timer -= Time.deltaTime;
        UIManagerscript.instance.UpdateTimer(timer);

        if (timer <= 0)
        {
            LoseLife();
        }
    }

    public void StartGame()
    {
        score = 0;
        lives = 3;
        round = 1;

        StartRound();
        UIManagerscript.instance.UpdateUI(score, lives, round);
    }

    public void StartRound()
    {
        timer = roundTime;
        gameRunning = true;

        UIManagerscript.instance.UpdateRound(round);
    }

    public void AddScore(int points)
    {
        score += points;
        UIManagerscript.instance.UpdateScore(score);
    }

    public void NextRound()
    {
        round++;
        StartRound();
    }

    public void LoseLife()
    {
        lives--;
        UIManagerscript.instance.UpdateLives(lives);

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            StartRound();
        }
    }

    void GameOver()
    {
        gameRunning = false;
        UIManagerscript.instance.ShowGameOver();
    }
}