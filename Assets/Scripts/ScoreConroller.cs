using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreConroller : MonoBehaviour
{
    
    public Text scoreText;
    public Text levelPassedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score " + GameController.Score;
        levelPassedText.text = "Level: " + GameController.LevelsPassed;
        
    }
}
