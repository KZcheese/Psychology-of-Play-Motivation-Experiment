using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    private const string HighScoreKey = "High Score";
    public string filename = "log.csv";
    private TextWriter textWriter;

    // Start is called before the first frame update
    private void Awake() {
        filename = Application.dataPath + "/" + filename;
        // textWriter.WriteLine("Points");
    }

    public static int LoadHighScore() {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public static void SaveHighScore(int highScore) {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

    public void intializeLog(string name, string id) {
        textWriter = new StreamWriter(filename, false);
        textWriter.WriteLine("Points,Time,Name,ID");
        textWriter.WriteLine($",,{name},{id}");
        textWriter.Close();
    }
    
    public void logAttempt(string[] properties) {
        textWriter = new StreamWriter(filename, true);
        textWriter.WriteLine(string.Join(",", properties.Select(item => "'" + item + "'")));
        textWriter.Close();    
    }
}