
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed;
    Rigidbody2D rigidBody;

    public Text collectedText;
    public static int collectedAmount = 0;


    public GameObject bulletPrefab;
    public float bulletSpeed; 
    private float lastFire;
    public float fireDelay;

    
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        

        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");

        if((shootHor != 0 || shootVer != 0) &&Time.time > lastFire +fireDelay)
        {   
            
            Shoot(shootHor, shootVer);
            lastFire = Time.time;
        }


        rigidBody.velocity = new Vector3(horizontal*speed, vertical*speed, 0);
        //collectedText.text = "Собрал кружков: " + collectedAmount;
    }
    void Shoot(float x, float y)
    {
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0);

        Vector3 rotZ = new Vector3(0, 0, 1);

        if (y == 0)
        {
            bullet.GetComponent<Transform>().rotation = Quaternion.AngleAxis((x<0)? 0 : 180, rotZ);
        }
        else if (y < 0)
        {
            if (x == 0)
            {
                bullet.GetComponent<Transform>().rotation = Quaternion.AngleAxis(90, rotZ);
            }
            else
            {
                bullet.GetComponent<Transform>().rotation = Quaternion.AngleAxis((x < 0) ? 45 : 135, rotZ);
            }
            
        }
        else if(y > 0)
        {
            if(x == 0)
            {
                bullet.GetComponent<Transform>().rotation = Quaternion.AngleAxis(270, rotZ);
            }
            else
            {
                bullet.GetComponent<Transform>().rotation = Quaternion.AngleAxis((x < 0) ? 315 : 225, rotZ);
            }
            
        }
        
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
