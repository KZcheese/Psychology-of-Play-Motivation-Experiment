using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Transform))]
public class HighScoreTableManager : MonoBehaviour
{
    public Transform _entryContainer;
    public Transform _entryTemplate;

    private List<HighScoreEntry> _highScoreEntryList;
    private List<Transform> _highScoreEntryTransformList;

    private const int Rank = 0;
    private const int Score = 1;
    private const int Name = 2;
    private const int ID = 3;

    private void Awake()
    {
        // Debug.Log("awake!");
        //entryContainer = transform.Find("highScoreEntryContainer");
        //entryContainer = transform.Find("body");
        //Potential fix for this: Due to creation of "body" empty object, it could not find it's direct child anymore, so we have to find the child within the ody which is now the new child. So we would have to find the container once we have found body.
        // _entryContainer = transform.Find("body").Find("highScoreEntryContainer");
        // Debug.Log("entrycontainer " + transform.Find("body").Find("highScoreEntryContainer"));
        // _entryTemplate = _entryContainer.Find("highScoreEntryTemplate");

        _entryTemplate.gameObject.SetActive(false);

        _highScoreEntryList = new List<HighScoreEntry>
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
        for (int i = 0; i < _highScoreEntryList.Count - 1; i++)
        {
            for (int j = i + 1; j < _highScoreEntryList.Count; j++)
            {
                if(_highScoreEntryList[j].score > _highScoreEntryList[i].score)
                {
                    //Swap
                    (_highScoreEntryList[i], _highScoreEntryList[j]) = (_highScoreEntryList[j], _highScoreEntryList[i]);
                }
            }
        }

        _highScoreEntryTransformList = new List<Transform>();

        foreach (HighScoreEntry highScoreEntry in _highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, _entryContainer);
        }
        Debug.Log(_highScoreEntryList);
        Debug.Log(_highScoreEntryTransformList);
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container)
    {
        const float templateHeight = 40f;

        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * _highScoreEntryTransformList.Count);
        entryTransform.gameObject.SetActive(true);

        string rankString = GenerateRankString(_highScoreEntryTransformList.Count + 1);

        //Debug.Log(entryTransform.Find("posText"));
        //Debug.Log(entryTransform.GetChild(0));

        //This GetChild() seems to work compared to find, so I will be creating transforms for each of the text boxes
        Transform posText = entryTransform.GetChild(Rank);
        Transform scoreText = entryTransform.GetChild(Score);
        Transform nameText = entryTransform.GetChild(Name);
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

        _highScoreEntryTransformList.Add(entryTransform);
    }

    public void AddHighScore(int score, string name, int id)
    {
        bool existing = false;
        foreach (Transform entryTransform in from entryTransform in _highScoreEntryTransformList
                 let transformID = int.Parse(entryTransform.GetChild(ID).GetComponent<Text>().text)
                 where id.Equals(transformID)
                 select entryTransform)
        {
            // update score
            entryTransform.GetChild(Score).GetComponent<Text>().text = score.ToString();

            // update name (just in case it changed)
            entryTransform.GetChild(Name).GetComponent<Text>().text = name;
            existing = true;
        }

        if(!existing)
        {
            HighScoreEntry newHighScore = new HighScoreEntry {score = score, name = name, id = id};
            CreateHighScoreEntryTransform(newHighScore, _entryContainer);
        }

        // fix rankings to reflect changed score
        SortScoreBoard();
        // No longer used now that sorting swaps rankings
        // RegenerateRankings();
    }

    public int GetHighScore(int id)
    {
        foreach (Transform entryTransform in _highScoreEntryTransformList)
        {
            if(int.Parse(entryTransform.GetChild(ID).GetComponent<Text>().text).Equals(id))
            {
                return int.Parse(entryTransform.GetChild(Score).GetComponent<Text>().text);
            }
        }

        return 0;
        // return (from entryTransform in _highscoreEntryTransformList
        //     where int.Parse(entryTransform.GetChild(ID).GetComponent<Text>().text).Equals(id)
        //     select int.Parse(entryTransform.GetChild(Score).GetComponent<Text>().text)).FirstOrDefault();
    }

    private void SortScoreBoard() 
    {
        //Sort entry list by score
        for (int i = 0; i < _highScoreEntryTransformList.Count - 1; i++)
        {
            for (int j = i + 1; j < _highScoreEntryTransformList.Count; j++)
            {
                int iScore = int.Parse(_highScoreEntryTransformList[i].GetChild(Score).GetComponent<Text>().text);
                int jScore = int.Parse(_highScoreEntryTransformList[j].GetChild(Score).GetComponent<Text>().text);

                if(jScore <= iScore) continue;
                //Swap
                (_highScoreEntryList[i], _highScoreEntryList[j]) = (_highScoreEntryList[j], _highScoreEntryList[i]);

                //Swap Ranking
                Text iRank = _highScoreEntryTransformList[i].GetChild(Rank).GetComponent<Text>();
                Text jRank = _highScoreEntryTransformList[j].GetChild(Rank).GetComponent<Text>();
                (iRank, jRank) = (jRank, iRank);
            }
        }
    }

    private void RegenerateRankings()
    {
        for (int i = 0; i < _highScoreEntryTransformList.Count; i++)
        {
            _highScoreEntryTransformList[i].GetChild(Rank).GetComponent<Text>().text = GenerateRankString(i + 1);
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