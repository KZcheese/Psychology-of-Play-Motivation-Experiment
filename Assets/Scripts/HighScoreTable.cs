using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighScoreEntry>()
        {
            new HighScoreEntry{score= 458, name = "Elio", id = 1},
            new HighScoreEntry{score= 303, name = "AK", id = 11},
            new HighScoreEntry{score= 287, name = "William Maier", id = 16},
            new HighScoreEntry{score= 247, name = "Ziad", id = 12},
            new HighScoreEntry{score= 130, name = "Kito", id = 15},
            new HighScoreEntry{score= 100, name = "Will Hu", id = 101},
            new HighScoreEntry{score= 99, name = "Samer", id = 13},
            new HighScoreEntry{score= 78, name = "Rami", id = 6},
            new HighScoreEntry{score= 64, name = "Paramveer", id = 4},
            new HighScoreEntry{score= 52, name = "Jake", id = 5}
        };

        highscoreEntryTransformList = new List<Transform>();

        
        foreach (HighScoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString;
        Debug.Log("entering rank system");
        switch (rank)
        {
            default:
                rankString = rank + "TH"; Debug.Log("entering switch system"); break;

            case 1: rankString = "1ST"; Debug.Log("entering switch system"); break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        //Debug.Log(entryTransform.Find("posText"));
        //Debug.Log(entryTransform.GetChild(0));

        //This GetChild() seems to work compared to find, so I will be creating transofrms for each of the text boxes
        Transform posText = entryTransform.GetChild(0);
        Transform scoreText = entryTransform.GetChild(1);
        Transform nameText = entryTransform.GetChild(2);
        Transform idText = entryTransform.GetChild(3);

        posText.GetComponent<Text>().text = rankString;

        //entryTransform.Get = rankString;
        //Debug.Log(entryTransform.Find("posText"));
        //Debug.Log(entryTransform.Find("scoreText").GetComponent<Text>().text);

        //for testing we using a random number
        int score = highscoreEntry.score;

        scoreText.GetComponent<Text>().text = score.ToString();

        //for testing
        string name = highscoreEntry.name;
        int id = highscoreEntry.id;
        nameText.GetComponent<Text>().text = name;
        idText.GetComponent<Text>().text = id.ToString();

        transformList.Add(entryTransform);
    }

    /*
     * Represents a single High Score Entry
     **/
    private class HighScoreEntry
    {
        public int score;
        public string name;
        public int id;
    }
}
