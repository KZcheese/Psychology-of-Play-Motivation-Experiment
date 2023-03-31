using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Transform))]
public class HighScoreTableManager : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public int numEntries = 15;
    public bool showAllEntries = false;

    private List<HighScoreEntry> _highScoreEntryList;
    private List<Transform> _highScoreEntryTransformList;

    private const int Rank = 0;
    private const int Score = 1;
    private const int Name = 2;
    private const int ID = 3;

    private void Awake()
    {
        _highScoreEntryList = new List<HighScoreEntry>
        {
            new HighScoreEntry {score = 458, name = "Elio", id = 1},
            new HighScoreEntry {score = 303, name = "AK", id = 11},
            new HighScoreEntry {score = 287, name = "William Maier", id = 16},
            new HighScoreEntry {score = 247, name = "Ziad", id = 12},
            new HighScoreEntry {score = 182, name = "Jhoowon", id = 10},
            new HighScoreEntry {score = 130, name = "Kito", id = 15},
            new HighScoreEntry {score = 100, name = "Will Hu", id = 101},
            new HighScoreEntry {score = 99, name = "Samer", id = 13},
            new HighScoreEntry {score = 78, name = "Rami", id = 6},
            new HighScoreEntry {score = 64, name = "Paramveer", id = 4},
            new HighScoreEntry {score = 52, name = "Jake", id = 5},
            new HighScoreEntry {score = 42, name = "Ajith", id = 9},
            new HighScoreEntry {score = 22, name = "Tong Liu", id = 100},
            new HighScoreEntry {score = 10, name = "yinan", id = 3},
            new HighScoreEntry {score = 6, name = "Zhen Mou", id = 114514}
        };
    }

    public void GenerateScoreBoard()
    {
        // _entryContainer = transform.Find("highScoreEntryContainer");
        // _entryContainer = transform.Find("body");
        // Potential fix for this: Due to creation of "body" empty object, it could not find it's direct child anymore, so we have to find the child within the ody which is now the new child. So we would have to find the container once we have found body.
        // _entryContainer = transform.Find("body").Find("highScoreEntryContainer");
        // _entryTemplate = _entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        _highScoreEntryTransformList = new List<Transform>();

        _highScoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

        if(showAllEntries) numEntries = _highScoreEntryList.Count;
        for (int i = 0; i < numEntries; i++)
        {
            CreateHighScoreEntryTransform(_highScoreEntryList[i], entryContainer);
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container)
    {
        const float templateHeight = 40f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * _highScoreEntryTransformList.Count);
        entryTransform.gameObject.SetActive(true);

        string rankString = GenerateRankString(_highScoreEntryTransformList.Count + 1);

        //This GetChild() seems to work compared to find, so I will be creating transforms for each of the text boxes
        Transform posText = entryTransform.GetChild(Rank);
        Transform scoreText = entryTransform.GetChild(Score);
        Transform nameText = entryTransform.GetChild(Name);
        Transform idText = entryTransform.GetChild(ID);

        posText.GetComponent<Text>().text = rankString;

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
        foreach (HighScoreEntry highScoreEntry in _highScoreEntryList.Where(highScoreEntry => highScoreEntry.id.Equals(id)))
        {
            highScoreEntry.score = score;
            highScoreEntry.name = name;
            existing = true;
        }

        if(!existing)
            _highScoreEntryList.Add(new HighScoreEntry {score = score, name = name, id = id});
    }

    public int GetHighScore(int id)
    {
        return _highScoreEntryList.FirstOrDefault(h => h.id.Equals(id))?.score ?? 0;
    }

    private static string GenerateRankString(int rank)
    {
        string rankString = rank switch
        {
            1 => "1ST",
            2 => "2ND",
            3 => "3RD",
            _ => rank + "TH"
        };

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