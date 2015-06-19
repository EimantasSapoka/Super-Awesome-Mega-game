using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

    private float movetime = .1f;
    private float inverseMoveTime;
    private Rigidbody2D rb2d;

    public LayerMask blockingLayer;
    

	// Use this for initialization
	protected virtual void Start () {
        inverseMoveTime = 1f / movetime;
        rb2d = GetComponent<Rigidbody2D>();
        blockingLayer = LayerMask.GetMask("BlockingLayer");
	}

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 end = (Vector2) transform.position + new Vector2(xDir, yDir);   

        hit = Physics2D.Linecast(transform.position, end, blockingLayer);

        if (hit.transform != null)
        {
            Debug.Log(string.Format("object hit {0}", hit.transform.name));
            
            return false;
        }

        StartCoroutine(SmoothMovement(end));
        return true;
    }

    protected virtual bool AttemptMove(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool moved = Move(xDir, yDir, out hit);
        GameManager.instance.playerMove = false;
        return moved;
    }

    IEnumerator SmoothMovement(Vector2 end)
    {
        float sqrRemainingDistance = ((Vector2)transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPosition);
            sqrRemainingDistance = ((Vector2) transform.position - end).sqrMagnitude;
            yield return null;
        }
    }
}
