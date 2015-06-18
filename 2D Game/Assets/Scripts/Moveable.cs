using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

    private float movetime = .1f;
    private float inverseMoveTime;
    private Rigidbody2D rb2d;
    

	// Use this for initialization
	protected virtual void Start () {
        inverseMoveTime = 1f / movetime;
        rb2d = GetComponent<Rigidbody2D>();
	}

    protected bool Move(int xDir, int yDir)
    {
        Vector2 end = (Vector2) transform.position + new Vector2(xDir, yDir);
        StartCoroutine(SmoothMovement(end));
        return true;
    }

    protected IEnumerator SmoothMovement(Vector2 end)
    {
        float sqrRemainingDistance = ((Vector2)transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPosition);
            sqrRemainingDistance = ((Vector2) transform.position - end).sqrMagnitude;
            yield return null;
        }
        GameManager.instance.playerMove = true;
    }
}
