using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameStateScript : MonoBehaviour
{
    public int score;
    public int highScore;
    public int tableScore;
    public string playerName;
    public int id;
    public TextMeshProUGUI scoreHud;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI highScoreDisplay;
    public GameObject gameOverScreen;
    public GameObject highScoreScreen;
    public bool isAlive;
    public SaveManager saveManager;
    public HighScoreTableManager highScoreTableManager;
    private float _startTime;

    public void Start()
    {
        score = 0;
        isAlive = true;
        (playerName, id) = SaveManager.LoadPlayer();

        highScore = SaveManager.LoadHighScore();
        tableScore = highScoreTableManager.GetHighScore(id);
        if(tableScore > highScore) highScore = tableScore;
        _startTime = Time.time;
    }

    [ContextMenu("Add Score")]
    public void AddScore(int points)
    {
        score += points;
        scoreHud.text = score.ToString();
    }

    public void ShowHighScores()
    {
        gameOverScreen.SetActive(false);
        highScoreScreen.SetActive(true);
        highScoreTableManager.GenerateScoreBoard();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        if(!isAlive) return;
        saveManager.logAttempt(new[] {score.ToString(), (Time.time - _startTime).ToString()});
        
        if(score > highScore)
            highScore = score;

        if(tableScore < highScore)
        {
            SaveManager.SaveHighScore(highScore);
            highScoreTableManager.AddHighScore(highScore, playerName, id);
        }

        scoreDisplay.text = score.ToString();
        highScoreDisplay.text = highScore.ToString();

        gameOverScreen.SetActive(true);
        isAlive = false;
    }
}