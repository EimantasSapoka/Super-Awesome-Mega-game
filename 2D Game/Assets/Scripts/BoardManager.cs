using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public int columns = 12;
    public int rows = 12;
    public int obstacleCount;

    public GameObject outerWallTiles;
    public GameObject floorTiles;
    public GameObject exit;
    public GameObject[] obstacles;
    public GameObject lever;


    private List<Vector2> obstaclePositions = new List<Vector2>();
    private int leverRangeX;
    private int leverRangeY;
    
    


    Transform boardHolder;

    public void Awake()
    {
        InitList();
        CreateBoard();
        LayoutAtRandomPosition(1, lever);
        LayoutAtRandomPosition(1, exit);
        LayoutAtRandomPosition(obstacleCount, obstacles);
        
    }

    void InitList()
    {
        obstaclePositions.Clear();
        for (int i = 0; i < columns -1; i++)
        {
            for (int j = 0; j < rows -1; j++)
            {
                obstaclePositions.Add(new Vector2(i, j));
            }
            
        }
    }

    void CreateBoard()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns+1; x++)
        {
            for (int y = -1; y < rows+1; y++)
            {
                GameObject toInstantiate = floorTiles;

                if (x == -1 || x == columns || y == -1 || y == rows)                   
                    toInstantiate = outerWallTiles;

                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

            }
        }

    }

    Vector2 GetRandomPosition()
    {
        int randomPos = Random.Range(0, obstaclePositions.Count);
        Vector2 randomVector = obstaclePositions[randomPos];
        obstaclePositions.RemoveAt(randomPos);
        return randomVector;
    }

    void LayoutAtRandomPosition(int count, params GameObject[] items)
    {
        for (int i = 0; i < count; i++)
		{
            Instantiate(items[Random.Range(0, items.Length)], GetRandomPosition(), Quaternion.identity);
		}
    }
   
	
}
