using System;
using System.IO;
using UnityEngine;

public class LogFile : MonoBehaviour {
    string filename = "";

    [Serializable]
    public class Player {
        public string name;
        public int LoadHighScore;
    }

    [Serializable]
    public class PlayerList {
        public Player[] player;
    }

    public PlayerList myPlayerList = new PlayerList();

    // Start is called before the first frame update
    private void Start() {
        filename = Application.dataPath + "/test.csv";
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            WriteCSV();
    }

    private void WriteCSV() {
        if (myPlayerList.player.Length <= 0) return;
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Name, point");
        tw.Close();

        tw = new StreamWriter(filename, true);

        foreach (var player in myPlayerList.player) {
            tw.WriteLine(player.name + "," + player.LoadHighScore);
        }

        tw.Close();
    }
}