using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public static Manager instance;


    public static float health = 6;
    public static int healthMax = 6;

    public static float movementSpeed = 5f;
    public static float speedMax = 12f;

    public static float fireRate = 0.5f;
    public static float fireRateMax = 0.1f;

    public static float bulletSize = 0.3f;
    public static float bulletSizeMax = 1;


    public static bool inBossRoom = false;
    public static bool readyToFight = false;

    public static bool coolDownAttack = false;
    //text 
    public  Text healthText;
    
    //singleton 
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }







    private void Update()
    {
        healthText.text = "Health: " + health;
   
    }



    public static float HealthStatus
    {
        get => health;
        set => health = value;
    }

    public static int HealthMax
    {
        get => healthMax;
        set => healthMax = value;
    }


    public static float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }
    public static float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    public static float BulletSize
    {
        get => bulletSize;
        set => bulletSize = value;
    }


    public static void Kill()
    {
        // game over
    }




    private static IEnumerator CD()
    {
        coolDownAttack = true;
        
        yield return new WaitForSeconds(2);

        coolDownAttack = false;
    }




    public static void Damage(float damage)
    {
        if (!coolDownAttack)
        {
            health = health - damage;
            if (health <= 0)
            {
                Kill();
            }
            instance.StartCoroutine(CD());
        }

    }






    //function for items/powerups
    public static void Heal(float x)
    {
        if ((health + x) < healthMax)
        {
            health = health + x;
        }
        else
        {
            health = healthMax;
        }
    }


  
    public static void ChangePlayerSpeed(float speed)
    {
        movementSpeed = Mathf.Min(movementSpeed+ speed, speedMax);

      //  movementSpeed += speed;
    }


    public static void ChangeBulletRate(float rate)
    {
        fireRate = Mathf.Max(fireRate - rate, fireRateMax);
        //fireRate -= rate;
    }




    public static void ChangeBulletsize(float size)
    {
        bulletSize = Mathf.Min(bulletSize + size, bulletSizeMax);
        //bulletSize += size;
    }


}
