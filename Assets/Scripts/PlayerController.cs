
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;


    //bullet
    public GameObject bulletPrefab;
    public float bulletSpeed; //defined in inspector
    private float lastFire;
    public float fireDelay; //defined in inspector


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // set player values
        fireDelay = Manager.FireRate;
        speed = Manager.MovementSpeed;


        // get movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // get shoot input
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        // if player pressed shooting btn and delay time has passed
        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)  
        {
            Shoot(shootHor, shootVert); // instantiaate bullet from player position
            lastFire = Time.time; // record time for fire
        }

        // Set velocity of player movement thorough player input
        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        // Display score for collected items
        collectedText.text = "Items Collected: " + collectedAmount;
    }



    // function to shoot bullet from the player
    void Shoot(float x, float y)
    {
        // instatiate bullet prefab
      // float a = this.transform.position.y - 1.5f;
   
      //  Vector3 shootposition = new Vector3(this.transform.position.x, a, this.transform.position.z);

        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation) as GameObject;
        //set gravity to 0
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        //set velocity of bullet
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0);

    }


}
