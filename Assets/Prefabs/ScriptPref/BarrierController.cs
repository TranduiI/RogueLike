using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            
        }
        if(collision.tag == "Player")
        {
            GameController.DamagePlayer(1, collision.gameObject);
        }
    }
}
