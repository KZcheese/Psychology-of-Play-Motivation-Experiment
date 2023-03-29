using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour
{
    public int score;
    public int highScore;
    public string playerName;
    public int id;
    public TextMeshProUGUI scoreHud;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highScoreDisplay;
    public GameObject gameOverScreen;
    public GameObject highScoreScreen;
    public bool isAlive;
    public SaveManager saveManager;
    public HighScoreTable highScoreTable;
    private float startTime;

    public void Start()
    {
        score = 0;
        isAlive = true;
        (playerName, id) = SaveManager.LoadPlayer();
        highScore = highScoreTable.GetHighScore(id);
        startTime = Time.time;
    }

    [ContextMenu("Add Score")]
    public void AddScore(int points)
    {
        score += points;
        scoreHud.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        if(!isAlive) return;
        saveManager.logAttempt(new[] {score.ToString(), (Time.time - startTime).ToString()});
        if(score > highScore)
        {
            highScore = score;
            highScoreTable.AddHighScore(score, playerName, id);
        }

        scoreDisplay.text = score.ToString();
        highScoreDisplay.text = highScore.ToString();

        gameOverScreen.SetActive(true);
        isAlive = false;
    }
}