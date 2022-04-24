using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabratoryGenerator : MonoBehaviour
{

    public LabratoryGenerationData labData;
    private List<Vector2Int> labRooms;


    private void Start()
    {
        labRooms = LabCrawlerController.GenerateDungeon(labData);
        SpawnRooms(labRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> r)
    {
        RoomController.instance.LoadRoom("Start", 0,0);
        foreach(Vector2Int location in r)
        {   //CREATE A BOSS ROOM AT THE VERY END
            /* if (location == labRooms[labRooms.Count - 1] && !(location == Vector2Int.zero))
             {
                 RoomController.instance.LoadRoom("Boss", location.x, location.y);
             }
             else
             {*/
            int a = Random.Range(1, 10);
            string b = a.ToString();
             RoomController.instance.LoadRoom(b, location.x, location.y);
           // }
        }
    }

}
