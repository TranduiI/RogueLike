using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");


        entryTemplate.gameObject.SetActive(false);

        

        //AddHighScoreEntry(228, 546456, "QWE");
        

        string jsonString = File.ReadAllText(Application.persistentDataPath + "/HighScoreTable.json");

        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        for (int i = 0; i < highScores.highScoreEntryList.Count;i++)
        {
            for(int j = i+1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
                {
                    HighScoreEntry tmp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i]= highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j]=tmp;
                }
            }
        }

        highScoreEntryTransformList = new List<Transform>();

        for(int i = 0; i < 10; i++)
        {
            CreateHighScoreEntryTransform(highScores.highScoreEntryList[i], entryContainer, highScoreEntryTransformList);
        }

    }
    

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH";
                break;
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
        }

        entryTransform.Find("PositionText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = highScoreEntry.score.ToString();
        entryTransform.Find("LevelText").GetComponent<Text>().text = highScoreEntry.level.ToString();
        entryTransform.Find("NameText").GetComponent<Text>().text = highScoreEntry.name;

        transformList.Add(entryTransform);
    }

    public static void AddHighScoreEntry(int level, int score, string name)
    {
        
        HighScoreEntry highScoreEntry = new HighScoreEntry { level = level, score = score, name = name};


        if (!File.Exists(Application.persistentDataPath + "/HighScoreTable.json"))
        {
            HighScores highScoresEmpty = new HighScores();
            string jsonEmpty = JsonUtility.ToJson(highScoresEmpty);
            //File.Create(Application.persistentDataPath + "/HighScoreTable.json");
            File.WriteAllText(Application.persistentDataPath + "/HighScoreTable.json", jsonEmpty); // WriteAllText должен сам создать файл если его нет
        }

        string jsonString = File.ReadAllText(Application.persistentDataPath + "/HighScoreTable.json");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        highScores.highScoreEntryList.Add(highScoreEntry);
        
        string json = JsonUtility.ToJson(highScores);
        File.WriteAllText(Application.persistentDataPath + "/HighScoreTable.json", json);       
    }
    public void ClearHighScores()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "/HighScoreTable.json");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            Destroy(entryContainer.gameObject);
        }

        highScores.highScoreEntryList.Clear();
        string json = JsonUtility.ToJson(highScores);
        File.WriteAllText(Application.persistentDataPath + "/HighScoreTable.json", json);
        Awake();
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int level;
        public int score;
        public string name;
    }
}
