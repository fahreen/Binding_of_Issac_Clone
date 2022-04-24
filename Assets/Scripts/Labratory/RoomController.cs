using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;




public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}




public class RoomController : MonoBehaviour
{

    // use singeton for room controller
    public static RoomController instance;

    public GameObject BossPanel;
    string currentWorldName = "Basement";
    

    // hold info about room location
    RoomInfo currentLoadRoomData;

    Room currentRoom;

    //queue of rooms to be loaded in current world
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }



    void Start()
    {
        
        /*
        LoadRoom("Start", 0, 0);
        
        LoadRoom("Empty", 1, 0);
        
        LoadRoom("Empty", -1, 0);
        
        LoadRoom("Empty", 0, 1);
       
        LoadRoom("Empty", 0, -1);
        */
    }

    void Update()
    {
       // Debug.Log("hello");
        UpdateRoomQueue();
    }


    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom  && !updatedRooms)
            {
                foreach(Room room in loadedRooms)
                {
                    room.RemoveConnectedDoors();
                }

                //UpdateRooms();
                updatedRooms = true;
            }





            return;
        }
        // remove room at the top of the queue, set it to current room
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        // start co routine to load the room
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));


    }




IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0)
        {
            // Locate last room generated
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            // delete the empty room
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Boss", tempRoom.X, tempRoom.Y);
        }
    }





    //Method to load scene
    public void LoadRoom(string name, int x, int y)
    {   
        // if room exist in a certain position, do not draw another room to overlap it
        if(RoomExists(x,y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;
       
        //add room to the queue
        loadRoomQueue.Enqueue(newRoomData);

    }


    // avoid lag when loading new scenes
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }

    }


    //set room settings
    public void SetRoom(Room r)
    {

        if (!RoomExists(currentLoadRoomData.X, currentLoadRoomData.Y))
        {


            // set position
            r.transform.position = new Vector3(currentLoadRoomData.X * r.Width, currentLoadRoomData.Y * r.Height, 0);

            // 
            r.X = currentLoadRoomData.X;
            r.Y = currentLoadRoomData.Y;
            r.name = currentWorldName + "-" + currentLoadRoomData.name + " " + r.X + ", " + r.Y;
            r.transform.parent = this.transform;
            isLoadingRoom = false;


            if (loadedRooms.Count == 0)
            {
                CameraControl.instance.currentRoom = r;
            }


            //add room to queue
            loadedRooms.Add(r);
            //r.RemoveConnectedDoors();
        }

        else
        {
            Destroy(r.gameObject);
            isLoadingRoom = false;

        }
    }



    public bool RoomExists(int x, int y)
    {
        bool v = loadedRooms.Find(room => room.X == x && room.Y == y) != null;
        return v;
    }




    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public void OnEnterRoom(Room r)
    {
        // ask player if they want to start boss fight
        if (r.name.Contains("Boss"))
        {
            //Debug.Log("Are u sure?");
            Manager.inBossRoom = true;
           // BossPanel.SetActive(true);
            //  BossPanel.SetActive(true);

            //Debug.Log("Are u sure?");
        }
        CameraControl.instance.currentRoom = r;
        currentRoom = r;
    }


}
