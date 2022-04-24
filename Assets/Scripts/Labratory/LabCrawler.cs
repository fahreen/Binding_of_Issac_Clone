using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabCrawler : MonoBehaviour
{

    public Vector2Int Position { get; set; }


    public LabCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }

    //Crawler movement
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directMovementMap)
    {
        //pick a random direction from enum
        Direction toMove = (Direction)Random.Range(0, directMovementMap.Count);
        Position += directMovementMap[toMove];
        return Position;

    }


}
