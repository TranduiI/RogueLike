using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public EnemyData enemyData;

    private static float enHealth;
    public static float EnHealth { get => enHealth; set => enHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        enHealth = enemyData.enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void DamageEnemy(float damage, GameObject enemy)
    {
        enHealth -= damage;
        Debug.Log("EnemyHealth" + enHealth);
        if (EnHealth <= 0)
        {
            Debug.Log("EnemyDied");
            if (enemy.GetComponent<EnemyType>() == EnemyType.Boss)
            {
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
                RoomController.instance.MakeNewScene();
                Debug.Log("Загрузка из Enemy");

            }
            Destroy(enemy);
        }
    }
}
