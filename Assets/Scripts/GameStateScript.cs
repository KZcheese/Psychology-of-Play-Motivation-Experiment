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

    public void Start() {
        score = 0;
        isAlive = true;
        highScore = saveManager.LoadHighScore();
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
        saveManager.logAttempt(score);
        if (score > highScore) {
            highScore = score;
            saveManager.SaveHighScore(highScore);
        }

        scoreBoard.text = score.ToString();
        highScoreBoard.text = highScore.ToString();
        
        gameOverScreen.SetActive(true);
        isAlive = false;
    }
}