using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveScoreController : MonoBehaviour
{   
    public InputField field; // —сылка на InputField - указываем в инспекторе
    string playerNameText;
    int score;
    int level;

    // Start is called before the first frame update
    public void SaveRecord()
    {
        playerNameText = field.text;
        score = GameController.Score;
        level = GameController.LevelsPassed;

        HighScoreTable.AddHighScoreEntry(level, score, playerNameText);

        BetweenScenesController.nextLevel = 0;
        BetweenScenesController.sceneEnd = true;
        
    }
    
}
