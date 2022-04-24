using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;

    public int Height;

    public int X;

    public int Y;

    private bool updatedDoors = false;
    public int TotalEnemies;
    public List<GameObject> Enemies = new List<GameObject>();

    public List<Door> unconnectedDoors;
    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }


    public Door doorLeft;
    public Door doorRight;
    public Door doorTop;
    public Door doorBottom;
    public List<Door> doors = new List<Door>();

   // public List<GameObject> Enemies;

    void Start()
    {
        unconnectedDoors = new List<Door>();
        // pressed play in the wrong scene
        if(RoomController.instance == null)
        {
            //Debug.Log("pressed play in arong scene");
            return;
        }

        Door[] drs = GetComponentsInChildren<Door>();

        // collect all doors
        foreach(Door d in drs)
        {
            doors.Add(d);
            switch (d.type)
            {
                case Door.DoorType.right:
                    doorRight = d;
                    break;
                case Door.DoorType.left:
                    doorLeft = d;
                    break;
                case Door.DoorType.top:
                    doorTop = d;
                    break;
                case Door.DoorType.bottom:
                    doorBottom = d;
                    break;
            }
        }
        foreach(GameObject e in Enemies)
        {
           e.SetActive( false);
        }

        RoomController.instance.SetRoom(this);
    }




    private void Update()
    {
        if (name.Contains("Boss") && !updatedDoors)
        {
            RemoveConnectedDoors();
            updatedDoors = true;
        }
        

        /*
        if (!name.Contains("Boss")) { 
        Door[] dewrs = GetComponentsInChildren<Door>();
        //Debug.Log(TotalEnemies);
       
            
         if (TotalEnemies > 0)
        {
            foreach (Door d in dewrs)
            {
                d.GetComponent<BoxCollider2D>().isTrigger = false;
                if (!unconnectedDoors.Contains(d))
                {
                    d.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }

        }
        else if (TotalEnemies == 0)
        {
            foreach (Door d in dewrs)
            {
                d.GetComponent<BoxCollider2D>().isTrigger = true;
                if (!unconnectedDoors.Contains(d))
                {

                    d.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                }







            }


        }


    }*/
            
            
        }









    









    // remove unncecessary doors
    public void RemoveConnectedDoors()
    {
        foreach(Door d in doors)
        {
            switch (d.type)
            {
                //if no room exists to the right, remove door to the right
                case Door.DoorType.right:
                    if (GetRight() == null)
                    {
                        //d.gameObject.SetActive(false);
                        SpriteRenderer[] sprites = d.gameObject.GetComponentsInChildren<SpriteRenderer>();
                        foreach (SpriteRenderer s in sprites)
                        {
                            s.enabled = false;
                        }
                        d.GetComponent<BoxCollider2D>().isTrigger = false;
                        unconnectedDoors.Add(d);
                    }
                
                    break;

                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        // d.gameObject.SetActive(false);
                        SpriteRenderer[] sprites = d.gameObject.GetComponentsInChildren<SpriteRenderer>();
                        foreach (SpriteRenderer s in sprites)
                        {
                            s.enabled = false;
                        }
                        d.GetComponent<BoxCollider2D>().isTrigger = false;
                        unconnectedDoors.Add(d);
                    }
                    break;

                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        //d.gameObject.SetActive(false);
                        SpriteRenderer[] sprites = d.gameObject.GetComponentsInChildren<SpriteRenderer>();
                        foreach (SpriteRenderer s in sprites)
                        {
                            s.enabled = false;
                        }
                        d.GetComponent<BoxCollider2D>().isTrigger = false;
                        unconnectedDoors.Add(d);
                    }
                    break;

                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        {

                        // d.gameObject.SetActive(false);
                        SpriteRenderer[] sprites = d.gameObject.GetComponentsInChildren<SpriteRenderer>();
                        foreach (SpriteRenderer s in sprites)
                        {
                            s.enabled = false;
                        }
                        d.GetComponent<BoxCollider2D>().isTrigger = false;
                        unconnectedDoors.Add(d);
                    }
                    break;






            }







        }
    }




    // get room to the right of current room, if exists
    public Room GetRight()
    {
        if (RoomController.instance.RoomExists(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    // get room to the left of current room, if exists
    public Room GetLeft()
    {
        if (RoomController.instance.RoomExists(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }


    // get room to the top of current room, if exists
    public Room GetTop()
    {
        if (RoomController.instance.RoomExists(X , Y + 1))
        {
            return RoomController.instance.FindRoom(X , Y + 1);
        }
        return null;
    }


    // get room to the bottom of current room, if exists
    public Room GetBottom()
    {
        if (RoomController.instance.RoomExists(X , Y -1))
        {
            return RoomController.instance.FindRoom(X , Y -1);
        }
        return null;
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(Width, Height, 0));
    }



    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RoomController.instance.OnEnterRoom(this);
        }
    }


}
