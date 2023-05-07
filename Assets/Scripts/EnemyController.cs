using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Attack,
    Die
};

public enum EnemyType
{
    Melee, 
    Ranged,
    Boss
};


public class EnemyController : MonoBehaviour
{
    GameObject player;

    public EnemyData enemyData;


    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;



    //public static float EnemyHealth {get => enemyHealth; set => enemyHealth = value; }


    public float enemyHealth;



    public float range;
    public float attackingRange;
    public float coolDown;
    public float speed;
    private bool chooseDir = false;
    //private bool dead = false;
    private bool coolDownAttack = false;
    public bool notInRoom = false;
    private Vector3 randomDir;


    public float bulletSpeed;
    public GameObject bulletPrefab;
    




    // Start is called before the first frame update
    void Start()
    {   

        player = GameObject.FindGameObjectWithTag("Player");
        

    }

    // Update is called once per frame
    

    void Update()
    {
        switch (currState)
        {
            case (EnemyState.Idle):
                Idle();
                break;
            case(EnemyState.Wander):
                Wander();
                break;
            case(EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
                //Die();
                break;
        }
        if (!notInRoom)
        {
            if(!GameController.IsKilled)
            {
                if (IsPlayerInRange(range) && currState != EnemyState.Die)
                {
                    currState = EnemyState.Follow;

                }
                else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
                {
                    currState = EnemyState.Wander;
                }

                if (Vector3.Distance(transform.position, player.transform.position) <= attackingRange)
                {
                    currState = EnemyState.Attack;
                }
            }
            else
            {
                currState = EnemyState.Idle;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Idle()
    {

    }


    void Wander()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
            
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if (IsPlayerInRange(range))
        {
            currState = EnemyState.Follow;

        }

    }
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
    }

    void Attack()
    {
        if(!coolDownAttack)
        {
            switch (enemyType)
            {
                case(EnemyType.Melee):
                    
                    GameController.DamagePlayer(5, player);
                    StartCoroutine(CoolDown());
                    break;
                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Boss):
                    bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }
        
    }
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void DamageEnemy(float damage, GameObject enemy)
    {   
        enemyHealth -= damage;
        //Debug.Log("EnemyHealth" + enemyHealth);
        if (enemyHealth <= 0)
        {
            //Debug.Log("EnemyDied");
            //Debug.Log("Неправильный уровень вызван отсюда");
            Death();
        }
    }

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        switch (enemyType)
        {
            case (EnemyType.Melee):
                GameController.ScoreUpdated(10);
                break;
            case (EnemyType.Ranged):
                GameController.ScoreUpdated(5);
                break;
            case (EnemyType.Boss):
                GameController.ScoreUpdated(250);
                break;
        }

        if (enemyType == EnemyType.Boss)
        {
            
            RoomController.instance.MakeNewScene();
            Debug.Log("Загрузка из EnemyController");
            

        }
        Destroy(gameObject);
    }

}
