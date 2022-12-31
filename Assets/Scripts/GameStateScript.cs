using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateScript : MonoBehaviour {

    public int score;
    public TextMeshProUGUI scoreBoard;
    public GameObject gameOverScreen;    
    public bool isAlive = true;

    [ContextMenu("Add Score")]
    public void addScore(int points) {
        score += points;
        scoreBoard.text = score.ToString();
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver() {
        gameOverScreen.SetActive(true);
        isAlive = false;
    }
}
