using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {


    public int columns = 20;
    public int rows = 20;

    public GameObject outerWallTiles;
    public GameObject floorTiles;
    public GameObject cornerWallTile;


    Transform boardHolder;

    public void Awake()
    {
        CreateBoard();
    }

    public void CreateBoard()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns+1; x++)
        {
            for (int y = -1; y < rows+1; y++)
            {
                GameObject toInstantiate = floorTiles;

                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    if ( (y == -1 && x == -1) || ( y == -1 && x == rows) || (y == columns && x == -1) || (y== columns && x == rows))
                    {
                        toInstantiate = cornerWallTile;
                    }
                    else
                    {
                        toInstantiate = outerWallTiles;
                    }
                }

                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

                
                if (x == -1)
                    instance.transform.Rotate(new Vector3(0, 0, 270));
                else if (x == columns)
                    instance.transform.Rotate(new Vector3(0, 0, -270));
                

                if (y == rows && x<columns && x > -1)
                    instance.transform.Rotate(new Vector3(180,0,0));
            }
        }

    }
	
}
