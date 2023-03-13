using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour {
    public int score;
    public int highScore;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI highScoreBoard;
    public GameObject gameOverScreen;
    public bool isAlive;
    public SaveManager saveManager;
    private float startTime;

    public void Start() {
        score = 0;
        isAlive = true;
        highScore = SaveManager.LoadHighScore();
        startTime = Time.time;
    }

    [ContextMenu("Add Score")]
    public void AddScore(int points) {
        score += points;
        scoreDisplay.text = score.ToString();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver() {
        if (!isAlive) return;
        saveManager.logAttempt(new[] { score.ToString(), (Time.time - startTime).ToString() });
        if (score > highScore) {
            highScore = score;
            SaveManager.SaveHighScore(highScore);
        }

        scoreBoard.text = score.ToString();
        highScoreBoard.text = highScore.ToString();
        
        gameOverScreen.SetActive(true);
        isAlive = false;
    }
}