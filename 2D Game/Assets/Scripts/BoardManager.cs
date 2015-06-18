using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public int columns;
    public int rows;

    public GameObject outerWallTiles;
    public GameObject floorTiles;
    public GameObject exit;
    public GameObject[] obstacles;
    public GameObject chest;
    public GameObject lever;


    private List<Vector2> obstaclePositions = new List<Vector2>();
    private int leverRangeX;
    private int leverRangeY;
    
    


    Transform boardHolder;

    public void LoadLevel(int level)
    {
        
        InitList();
        CreateBoard();
        LayoutAtRandomPosition(1, lever);
        LayoutAtRandomPosition(1, exit);
        LayoutAtRandomPosition((int) Mathf.Sqrt(level), chest);
        LayoutObstacles((int)Mathf.Sqrt(rows*columns)*2, obstacles);        
    }

    private void LayoutObstacles(int obstacleCount, GameObject[] obstacles)
    {
        while (obstacleCount > 0)
        {
            int closeItems = Random.Range(0, (int) Mathf.Sqrt(obstacleCount));
            closeItems = closeItems == 0 ? 1 : closeItems;
            Vector2[] itemPositions = GetCloseRandomPositions(closeItems);
            Debug.Log(obstacles.Length);
            GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
            foreach (Vector2 pos in itemPositions)
            {
                GameObject instance = Instantiate(obstacle, pos, Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }

            obstacleCount -= closeItems;
        }
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

    Vector2[] GetCloseRandomPositions(int numPositions)
    {
        Vector2[] positions = new Vector2[numPositions];
        int seedPosition = Random.Range(0, obstaclePositions.Count);

        for (int i = 0; i < numPositions; i++)
        {
            int nextPosition = 0;
            if (obstaclePositions.Count > seedPosition + i)
                nextPosition = seedPosition + 1;
            else
                nextPosition = seedPosition - 1;
            positions[i] = obstaclePositions[nextPosition];
            obstaclePositions.RemoveAt(nextPosition);
        }

        return positions;
    }

    void LayoutAtRandomPosition(int count, params GameObject[] items)
    {
        for (int i = 0; i < count; i++)
		{
            GameObject instance = Instantiate(items[Random.Range(0, items.Length)], GetRandomPosition(), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
            
		}
    }
   
	
}
