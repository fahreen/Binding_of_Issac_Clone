using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class itemController : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public string info;
        public Sprite itemSprite;
    }


    public Item item;
    public float healthChange;
    public float playerspeedChange;
    public float bulletSpeedChange;
    public float bulletSizeChange;
    public float win;





    private void Start()
    {
        // set sprite for the item
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }




    private void OnTriggerEnter2D(Collider2D collission )
    {
        if(collission.tag == "Player")
        {
            PlayerController.collectedAmount++;
            Manager.Heal(healthChange);
            Manager.ChangePlayerSpeed(playerspeedChange);
            Manager.ChangeBulletRate(bulletSpeedChange);
            Manager.ChangeBulletsize(bulletSizeChange);
            if(win == 1)
            {
                //Debug.Log("Win");
                SceneManager.LoadScene("Scenes/Win");
            }



            Destroy(gameObject);
        }
    }
}
