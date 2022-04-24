using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime; // defined in inspector
    public bool isEnemyBullet = false;
    private Vector2 lastPosition;
    private Vector2 currentPosition;
    private Vector2 playerPosition;


    private bool coolDownAttack = false;
    //public float coolDown = 2;
    // Start is called before the first frame update
    void Start()
    {
        // after instantiation, destory bullet after a certain time
        StartCoroutine(DestroyBullet());


        if (!isEnemyBullet) { 
        //set bullet size for player
        transform.localScale = new Vector2(Manager.BulletSize, Manager.BulletSize);
        }
    }

    public void setPlayerPosition(Vector3 p)
    {
        playerPosition = p;
    }

     void Update()
    {
        if (isEnemyBullet)
        {
            // if bullet reaches targer(player, destroy it
            currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, 5f * Time.deltaTime);
            if(currentPosition == lastPosition)
            {
                Destroy(gameObject);
            }
            lastPosition = currentPosition;
        
        
        }
       
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }




 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && !isEnemyBullet )
        {
            collision.gameObject.GetComponent<EnemyController>().Die();
            Destroy(this.gameObject);
        }

        if(collision.tag == "Player" && isEnemyBullet)
        {
             
                Manager.Damage(1);
                
                
                Destroy(this.gameObject);
            
            
        }
    }
}
