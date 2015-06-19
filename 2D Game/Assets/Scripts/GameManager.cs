using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public BoardManager boardManager;
    private int level = 1;

    public float turnTime = .05f;
    public List<Enemy> enemies = new List<Enemy>();

    [HideInInspector] public bool playerMove;
    bool enemiesMove;

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

    void Update()
    {
        if (playerMove || enemiesMove)
        {
            return;
        }
        StartCoroutine(PerformTurn());
    }

    IEnumerator PerformTurn()
    {
        enemiesMove = true;

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnTime);
        }

        playerMove = true;
        enemiesMove = false;
    }
	
}
