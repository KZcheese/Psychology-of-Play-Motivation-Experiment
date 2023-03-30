using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    private const int RANK = 0;
    private const int SCORE = 1;
    private const int NAME = 2;
    private const int ID = 3;

    private void Awake()
    {
        //entryContainer = transform.Find("highScoreEntryContainer");
        //entryContainer = transform.Find("body");
        //Potential fix for this: Due to creation of "body" emty object, it could not find it's direct child anymore, so we have to find the child within the ody which is now the new child. So we would have to find the container once we have found body.
        entryContainer = transform.Find("body").Find("highScoreEntryContainer");
        Debug.Log("entrycontainer " + transform.Find("body").Find("highScoreEntryContainer"));
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        highscoreEntryList = new List<HighScoreEntry>
        {
            new HighScoreEntry {score = 458, name = "Elio", id = 1},
            new HighScoreEntry {score = 303, name = "AK", id = 11},
            new HighScoreEntry {score = 287, name = "William Maier", id = 16},
            new HighScoreEntry {score = 247, name = "Ziad", id = 12},
            new HighScoreEntry {score = 130, name = "Kito", id = 15},
            new HighScoreEntry {score = 99, name = "Samer", id = 13},
            new HighScoreEntry {score = 100, name = "Will Hu", id = 101},
            new HighScoreEntry {score = 78, name = "Rami", id = 6},
            new HighScoreEntry {score = 64, name = "Paramveer", id = 4},
            new HighScoreEntry {score = 182, name = "jhoon", id = 10}
        };

        //Sort entry list by score
        for (int i = 0; i < highscoreEntryList.Count - 1; i++)
        {
            for (int j = i + 1; j < highscoreEntryList.Count; j++)
            {
                if(highscoreEntryList[j].score > highscoreEntryList[i].score)
                {
                    //Swap
                    (highscoreEntryList[i], highscoreEntryList[j]) = (highscoreEntryList[j], highscoreEntryList[i]);
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();

        foreach (HighScoreEntry highScoreEntry in highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer);
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container)
    {
        const float templateHeight = 40f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * highscoreEntryTransformList.Count);
        entryTransform.gameObject.SetActive(true);

        string rankString = GenerateRankString(highscoreEntryTransformList.Count + 1);

        //Debug.Log(entryTransform.Find("posText"));
        //Debug.Log(entryTransform.GetChild(0));

        //This GetChild() seems to work compared to find, so I will be creating transforms for each of the text boxes
        Transform posText = entryTransform.GetChild(RANK);
        Transform scoreText = entryTransform.GetChild(SCORE);
        Transform nameText = entryTransform.GetChild(NAME);
        Transform idText = entryTransform.GetChild(ID);

        posText.GetComponent<Text>().text = rankString;

        //entryTransform.Get = rankString;
        //Debug.Log(entryTransform.Find("posText"));
        //Debug.Log(entryTransform.Find("scoreText").GetComponent<Text>().text);

        //for testing we using a random number
        int score = highScoreEntry.score;

        scoreText.GetComponent<Text>().text = score.ToString();

        //for testing
        string name = highScoreEntry.name;
        int id = highScoreEntry.id;
        nameText.GetComponent<Text>().text = name;
        idText.GetComponent<Text>().text = id.ToString();

        highscoreEntryTransformList.Add(entryTransform);
    }

    public void AddHighScore(int score, string name, int id)
    {
        bool existing = false;
        foreach (Transform entryTransform in from entryTransform in highscoreEntryTransformList
                 let transformID = int.Parse(entryTransform.GetChild(ID).GetComponent<Text>().text)
                 where id.Equals(transformID)
                 select entryTransform)
        {
            // update score
            entryTransform.GetChild(SCORE).GetComponent<Text>().text = score.ToString();

            // update name (just in case it changed)
            entryTransform.GetChild(NAME).GetComponent<Text>().text = name;
            existing = true;
        }

        if(!existing)
        {
            HighScoreEntry newHighScore = new HighScoreEntry {score = score, name = name, id = id};
            CreateHighScoreEntryTransform(newHighScore, entryContainer);
        }

        // fix rankings to reflect changed score
        SortScoreBoard();
        // No longer used now that sorting swaps rankings
        // RegenerateRankings();
    }

    public int GetHighScore(int id)
    {
        return (from entryTransform in highscoreEntryTransformList
            where int.Parse(entryTransform.GetChild(ID).GetComponent<Text>().text).Equals(id)
            select int.Parse(entryTransform.GetChild(SCORE).GetComponent<Text>().text)).FirstOrDefault();
    }

    private void SortScoreBoard()
    {
        //Sort entry list by score
        for (int i = 0; i < highscoreEntryTransformList.Count - 1; i++)
        {
            for (int j = i + 1; j < highscoreEntryTransformList.Count; j++)
            {
                int iScore = int.Parse(highscoreEntryTransformList[i].GetChild(SCORE).GetComponent<Text>().text);
                int jScore = int.Parse(highscoreEntryTransformList[j].GetChild(SCORE).GetComponent<Text>().text);

                if(jScore <= iScore) continue;
                //Swap
                (highscoreEntryList[i], highscoreEntryList[j]) = (highscoreEntryList[j], highscoreEntryList[i]);

                //Swap Ranking
                Text iRank = highscoreEntryTransformList[i].GetChild(RANK).GetComponent<Text>();
                Text jRank = highscoreEntryTransformList[j].GetChild(RANK).GetComponent<Text>();
                (iRank, jRank) = (jRank, iRank);
            }
        }
    }

    private void RegenerateRankings()
    {
        for (int i = 0; i < highscoreEntryTransformList.Count; i++)
        {
            highscoreEntryTransformList[i].GetChild(RANK).GetComponent<Text>().text = GenerateRankString(i + 1);
        }
    }

    private static string GenerateRankString(int rank)
    {
        string rankString;
        Debug.Log("entering rank system");
        switch (rank)
        {
            default:
                rankString = rank + "TH";
                Debug.Log("entering switch system");
                break;

            case 1:
                rankString = "1ST";
                Debug.Log("entering switch system");
                break;

            case 2:
                rankString = "2ND";
                break;

            case 3:
                rankString = "3RD";
                break;
        }

        return rankString;
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