using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// create a enum to represent the states of the enemy
public enum EnemyState
{
    Wander, Follow, Attack, Die
};


public enum EnemyType
{
    Melee, Ranged, Boss
};


public class EnemyController : MonoBehaviour
{

    
    GameObject player; // to represent the player/user
    public int EnemyHealth;
    public EnemyType type;
    public EnemyState currState = EnemyState.Wander; // instantiate Enemy with wander state

    // enemy variables(set in inspector)
    public float range;
    public float speed;

    // variables for random movement
    private bool chooseDir = false;
    private Vector3 randomDir;

    private bool dead = false;
    public float attackRange; // set in inspector


    
    private bool coolDownAttack = false;
    public float coolDown;

    public GameObject bulletPrefab;


    public Room r;

    public GameObject itemPrefab;
    public GameObject itemPrefab2;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");  //set player variable of each enemy to player
    }



    void Update()
    {
        // call bahaviour function based on current state
        switch (currState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
                Die();
                break;

        }

        // if player is clode by, change set to follow
        if (IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }

        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currState = EnemyState.Attack;
        }

    }




    //  return true if distance between enemy and player is less than range
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }








    // CO-ROUTINE for direction change for enemy movement
    //after 2-8 seconds, change direction of enemy wander movement
    private IEnumerator ChooseDirection()
    {
        chooseDir = true; //if choose dir is true, keep walking in current and a new coroutine will not be called
        yield return new WaitForSeconds(Random.Range(2f, 8f)); // wait an x amount of time to wander in current roatation

        // after coroutine is complete, randomly choose direction
        randomDir = new Vector3(0, 0, Random.Range(0, 360)); 

        // set enemy rotation
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f)); // change back and forth  between this.rotation and next rotation 
        chooseDir = false;  // set choose dir false, so that this function is called again
    }



    // as long as current state = wonder, keep calling coroutine repeatedly to change enemy direction
    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        // walk in current direction
        transform.position += transform.right * speed * Time.deltaTime;

        // if player gets close to enemy, follow player
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
        // damage player by 1 health
        if (!coolDownAttack)// if enough time has passed since the last attack
        {
            switch (type)
            {
                case (EnemyType.Melee):
                    Manager.Damage(0.5f);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    //instantiate a bullet
                    GameObject bullet = Instantiate(bulletPrefab,transform.position, Quaternion.identity );
                    //allow bullet controller to get access to playyer position
                    bullet.GetComponent<BulletController>().setPlayerPosition(player.transform.position);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;


                case (EnemyType.Boss):
                    //instantiate a bullet
                   

                    GameObject bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    GameObject bullet3 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    GameObject bullet4 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    GameObject bullet5 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    GameObject bullet6 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                   
                    //allow bullet controller to get access to playyer position
                    float x = player.transform.position.x;
                    float y = player.transform.position.y;
                    bullet2.GetComponent<BulletController>().setPlayerPosition(player.transform.position);
                    bullet3.GetComponent<BulletController>().setPlayerPosition(new Vector3(x + 1, y + 1, player.transform.position.z));
                    bullet4.GetComponent<BulletController>().setPlayerPosition(new Vector3(x - 1, y - 1, player.transform.position.z));
                    bullet4.GetComponent<BulletController>().setPlayerPosition(new Vector3(x + 1, y - 1, player.transform.position.z));
                    bullet6.GetComponent<BulletController>().setPlayerPosition(new Vector3(x - 1, y + 1, player.transform.position.z));

                    bullet2.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet3.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet4.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet5.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet6.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet2.GetComponent<BulletController>().isEnemyBullet = true;
                    bullet3.GetComponent<BulletController>().isEnemyBullet = true;
                    bullet4.GetComponent<BulletController>().isEnemyBullet = true;
                    bullet5.GetComponent<BulletController>().isEnemyBullet = true;
                    bullet6.GetComponent<BulletController>().isEnemyBullet = true;
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



    public void Die()
    {
        EnemyHealth--;
        if(itemPrefab2!= null)
        {
            if(EnemyHealth % 5 == 0) { 
            GameObject item = Instantiate(itemPrefab2, transform.position, Quaternion.identity);
            }
        }



        if(EnemyHealth <= 0) { 
            if(itemPrefab != null)
            {
                GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }

            r.TotalEnemies--;
            Destroy((this.gameObject));
        }

    }


}
