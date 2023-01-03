using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour {
    public int score;
    public TextMeshProUGUI scoreBoard;
    public GameObject gameOverScreen;
    public bool isAlive = true;

    [ContextMenu("Add Score")]
    public void AddScore(int points) {
        score += points;
        scoreBoard.text = score.ToString();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver() {
        gameOverScreen.SetActive(true);
        isAlive = false;
    }
}