using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{


    //create singleton
    public static CameraControl instance;

    public Room currentRoom;
    public float roomChangeSpped;
    public GameObject BossPanel;


    void Awake()
    {
        instance = this;
    }



    void Start()
    {

    }



    IEnumerator doorWait()
    {
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        if (currentRoom != null)
        {

            if (currentRoom.name.Contains("Boss") && Manager.inBossRoom == true )
            {
                if (!Manager.readyToFight)
                {
                    BossPanel.SetActive(true);
                }
                // Debug.Log("Are u sure?");
                
                // Manager.inBossRoom = f;

                else if (Manager.readyToFight)
                {

                    List<GameObject> Enem = currentRoom.Enemies;
                    if (Enem != null)
                    {
                        foreach (GameObject e in Enem)

                        {
                            if (e != null)
                            {
                                e.SetActive(true);
                            }
                        }

                    }

                    Door[] dowrs = currentRoom.GetComponentsInChildren<Door>();
                    if (currentRoom.TotalEnemies > 0)
                    {
                        foreach (Door d in dowrs)
                        {
                            d.GetComponent<BoxCollider2D>().isTrigger = false;
                            if (!currentRoom.unconnectedDoors.Contains(d))
                            {
                                d.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                            }
                        }

                    }


                    else if (currentRoom.TotalEnemies <= 0)
                    {
                        foreach (Door d in dowrs)
                        {
                            d.GetComponent<BoxCollider2D>().isTrigger = true;
                            if (!currentRoom.unconnectedDoors.Contains(d))
                            {

                                d.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                            }



                            //WON GAME SCREEN



                        }


                    }




                }



            }

            else if(!currentRoom.name.Contains("Boss"))
            {

                List<GameObject> Enemies = currentRoom.Enemies;
                if (Enemies != null)
                {
                    foreach (GameObject e in Enemies)

                    {
                        if (e != null)
                        {
                            e.SetActive(true);
                        }
                    }

                }


                Door[] dewrs = currentRoom.GetComponentsInChildren<Door>();
                if (currentRoom.TotalEnemies > 0)
                {
                    foreach (Door d in dewrs)
                    {
                        d.GetComponent<BoxCollider2D>().isTrigger = false;
                        if (!currentRoom.unconnectedDoors.Contains(d))
                        {
                            d.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                        }
                    }

                }


                else if (currentRoom.TotalEnemies <= 0)
                {
                    foreach (Door d in dewrs)
                    {
                        d.GetComponent<BoxCollider2D>().isTrigger = true;
                        if (!currentRoom.unconnectedDoors.Contains(d))
                        {

                            d.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                        }







                    }


                }

            }
            }
        }


        void UpdatePosition()
        {
            if (currentRoom == null)
            {
                return;
            }

            // get 
            Vector3 targerPos = GetCameraPosition();

            //update camera position to target
            this.transform.position = Vector3.MoveTowards(this.transform.position, targerPos, Time.deltaTime * roomChangeSpped);

        }




        private Vector3 GetCameraPosition()
        {
            if (currentRoom == null)
            {
                return Vector3.zero;
            }

            // get centre of current room 
            Vector3 targetPos = currentRoom.GetRoomCentre();
            // set the camera z to what it was before
            targetPos.z = this.transform.position.z;

            return targetPos;
        }



        public bool isChangingScene()
        {
            return this.transform.position.Equals(GetCameraPosition()) == false;
        }



    }
