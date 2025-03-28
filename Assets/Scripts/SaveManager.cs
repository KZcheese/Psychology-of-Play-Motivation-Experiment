using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string HighScoreKey = "High Score";
    private const string NameKey = "Name";
    private const string IDKey = "ID";
    public string filename = "log.csv";
    private TextWriter textWriter;

    // Start is called before the first frame update
    private void Awake()
    {
        filename = Application.dataPath + "/" + filename;
        // textWriter.WriteLine("Points");
    }

    public void intializeLog(string name, string id)
    {
        textWriter = new StreamWriter(filename, false);
        textWriter.WriteLine("Points,Time,Name,ID");
        textWriter.WriteLine($",,{name},{id}");
        textWriter.Close();
    }

    public void logAttempt(IEnumerable<string> properties)
    {
        textWriter = new StreamWriter(filename, true);
        textWriter.WriteLine(string.Join(",", properties.Select(item => "'" + item + "'")));
        textWriter.Close();
    }

    public static (string, int) LoadPlayer()
    {
        return (
            PlayerPrefs.GetString(NameKey, ""),
            PlayerPrefs.GetInt(IDKey, 0)
        );
    }
    
    public static void SavePlayer(string name, int id)
    {
        PlayerPrefs.SetString(NameKey, name);
        PlayerPrefs.SetInt(IDKey, id);
        PlayerPrefs.Save();
    }

    public static int LoadHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey);
    }

    public static void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

    
}