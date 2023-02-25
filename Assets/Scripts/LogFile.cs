using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogFile : MonoBehaviour
{
    string filename = "";

    [System.Serializable]

    public class Player
    {
        public string name;
        public int LoadHighScore;
    }
    [System.Serializable]

    public class PlayerList
    {
        public Player[] player;
    }
    public PlayerList myPlayerList = new PlayerList();

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            WriteCSV();
    }

    public void WriteCSV()
    {
        if(myPlayerList.player.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Name, point");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for(int i = 0; i < myPlayerList.player.Length; i++)
            {
                tw.WriteLine(myPlayerList.player[i].name + "," + myPlayerList.player[i].LoadHighScore);
            }
            tw.Close();
        }
    }
}
