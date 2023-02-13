using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string HighScoreKey = "High Score";
    public int LoadHighScore() {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void SaveHighScore(int highScore) {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }
}
