using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;

    public bool isEnemyBullet = false;

    private Vector2 lastPos;
    private Vector2 currPos;
    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        
    }


    // Update is called once per frame
    void Update()
    {
        if (isEnemyBullet)
        {   
            currPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f*Time.deltaTime);
            if(currPos == lastPos)
            {
                Destroy(gameObject);   
            }
            lastPos = currPos;
        }
    }

    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }

    void OnTriggerEnter2D(Collider2D col)
    {   
        if(col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (col.tag == "Enemy" && !isEnemyBullet)
        {

            col.gameObject.GetComponent<EnemyController>().DamageEnemy(GameController.AlphaStrike, col.gameObject);
            Destroy(gameObject);
        }
        if(col.tag == "Player" && isEnemyBullet)
        {
            GameController.DamagePlayer(6,col.gameObject);
            Destroy(gameObject);
        }

    }

}
