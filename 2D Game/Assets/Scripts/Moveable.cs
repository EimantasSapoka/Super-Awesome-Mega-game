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

    protected bool Move(float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector3 end = new Vector3(xDir, yDir, 0)*Time.deltaTime*3;
        hit = Physics2D.Linecast(transform.position,transform.position + end, blockingLayer);

        if (hit.transform != null)
        {
            Debug.Log(string.Format("object hit {0}", hit.transform.name));
            
            return false;
        }
        else
        {
            transform.position += end;
            return true;
        }

        
    }

    protected virtual bool AttemptMove(float xDir, float yDir)
    {
        RaycastHit2D hit;
        bool moved = Move(xDir, yDir, out hit);
        GameManager.instance.playerMove = false;
        return moved;
    }

    void SmoothMovement(Vector2 end)
    {
        float sqrRemainingDistance = ((Vector2)transform.position - end).sqrMagnitude;
        if (sqrRemainingDistance > float.Epsilon)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPosition);
            
        }
    }
}
