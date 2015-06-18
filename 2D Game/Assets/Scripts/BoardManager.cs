using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public int columns = 20;
    public int rows = 20;

    public GameObject outerWallTiles;
    public GameObject floorTiles;
    public GameObject doors;
    public GameObject exit_stairs;
    public GameObject[] obstacles;

    private List<Vector2> obstaclePositions = new List<Vector2>();
    private int leverRangeX;
    private int leverRangeY;
    


    Transform boardHolder;

    public void Awake()
    {
        InitList();
        CreateBoard();
        LayoutAtRandomPosition(obstacles, 6);
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

                if (x == columns - 1 && y == rows - 1)
                {
                    toInstantiate = doors;
                    GameObject stairsInstance = Instantiate(exit_stairs, new Vector2(x, y), Quaternion.identity) as GameObject;
                    stairsInstance.transform.SetParent(boardHolder);
                }
                    

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

    void LayoutAtRandomPosition(GameObject[] items, int count)
    {
        for (int i = 0; i < count; i++)
		{
            Instantiate(items[Random.Range(0, items.Length)], GetRandomPosition(), Quaternion.identity);
		}
    }

    
	
}
