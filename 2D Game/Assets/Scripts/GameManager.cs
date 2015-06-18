using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameManager instance;
    public BoardManager boardManager;
    private int level = 1;

	// Use this for initialization
	void Awake () {


        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardManager = GetComponent<BoardManager>();
        
        boardManager.LoadLevel(level);
	}
	
}
