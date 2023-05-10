using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instanse;

    public GameObject player;
    

    private static float health = 100;
    private static int  maxHealth = 100;
    private static float moveSpeed = 7;
    private static float fireRate = 0.5f;

    private static float alphaStrike = 5f;

    private static int score = 0;

    private static int levelsPassed = 1;

    public Text healthText;

    public static bool isKilled = false;

    public static bool IsKilled { get => isKilled; set => isKilled = value; }
    public static int LevelsPassed { get => levelsPassed; set => levelsPassed = value; }

    public static int Score { get => score; set => score = value; }

    public static float AlphaStrike { get => alphaStrike; set => alphaStrike = value; }

    public static float Health{get => health; set => health = value;}

    public static int MaxHealth{get => maxHealth; set => maxHealth = value;}

    public static float MoveSpeed{get => moveSpeed; set => moveSpeed = value;}

    public static float FireRate{get => fireRate; set => fireRate = value;}

   
    
    void Awake()
    {
        if(instanse==null)
        {
            instanse=this;
        }
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        

        healthText.text = "Health: " + health;
        
    }
    public static void SetToDefault()
    {
        health = 100;
        maxHealth = 100;
        moveSpeed = 7;
        fireRate = 0.5f;
        alphaStrike = 5f;
        score = 0;
        levelsPassed = 1;
        isKilled = false;
    }
    public static void DamagePlayer(int damage, GameObject player)
    {
        health -= damage;
        Debug.Log("HP игрока: " + health);
        if (Health <= 0)
        {
            Kill(player);

        }
        
    }
    

    

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        if(moveSpeed + speed < 14)
        {
            moveSpeed += speed;
        } 
        
    }

    public static void AttackSpeedChange(float rate)
    {
        if(fireRate - rate > 0.2f)
        {
            fireRate -= rate;
        } 
    }
    public static void AlphaStrikeChange(float alpha)
    {
        if (alphaStrike + alpha < 10)
        {
            alphaStrike += alpha;
        }
    }



    public static void Kill(GameObject person)
    {
        
        person.SetActive(false);
        isKilled = true;
        BetweenScenesController.nextLevel = 9;
        BetweenScenesController.sceneEnd = true;

    }

    public static void ScoreUpdated(int scoreUp)
    {
        score += scoreUp;
    }

    public static void LevelPassed()
    {
        levelsPassed = levelsPassed+1;
    }

}
