using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
}


public class LabCrawlerController : MonoBehaviour
{
    //Representation of 2D vectors and points using integers
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    // create dictionary [key = enum Direction, value = Vector2Int]
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up}, // 0, (0,1)
        {Direction.left, Vector2Int.left}, //1, (-1,0)
        {Direction.down, Vector2Int.down},//2, (0,-1)
        {Direction.right, Vector2Int.right}//3, (1,0)
    };


    // return a list of positions after iterating through dungeon data
    public static List<Vector2Int> GenerateDungeon(LabratoryGenerationData labData)
    {
        //  fill labCrawlers with all crawlers
        List<LabCrawler> labCrawlers = new List<LabCrawler>();
        for(int i = 0; i <labData.numberOfCrawlers; i++)
        {
            labCrawlers.Add(new LabCrawler(Vector2Int.zero)); // set position of the crawler to 0
        }

        int iterations = Random.Range(labData.iterationMin, labData.iterationMax);

        for(int i=0; i < iterations; i++)
        {
            foreach(LabCrawler lc in labCrawlers)
            {
                Vector2Int newPos = lc.Move(directionMovementMap);
                positionsVisited.Add(newPos);

            }
        }
        return positionsVisited;
    }


}
