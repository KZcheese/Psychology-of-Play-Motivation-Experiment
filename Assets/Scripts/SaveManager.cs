using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    private const string HighScoreKey = "High Score";
    string filename = "";
    TextWriter textWriter;

    // Start is called before the first frame update
    private void Awake() {
        filename = Application.dataPath + "/test.csv";
        textWriter = new StreamWriter(filename, true);
        // textWriter.WriteLine("Points");
    }

    public int LoadHighScore() {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void SaveHighScore(int highScore) {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

    public void logAttempt(int score) {
        textWriter.WriteLine(score);
        textWriter.Close();
    }

    void OnApplicationQuit() {
        textWriter.Close();
    }
}