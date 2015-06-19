using UnityEngine;
using System.Collections;

public class PlayerController : Moveable {

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
	// Update is called once per frame
	void Update()
    {
        if (!GameManager.instance.playerMove)
        {
            return;
        }
        
        int horizontal = (int) Input.GetAxisRaw("Horizontal");
        int vertical = (int) Input.GetAxisRaw("Vertical");

        if (horizontal != 0 && vertical != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove(horizontal, vertical);
            triggerMoveAnimations(horizontal, vertical);
        }
	}

    private void triggerMoveAnimations(int horizontal, int vertical)
    {
        if (horizontal < 0)
        {
            animator.SetTrigger("walk_left");
        }
        else if (horizontal > 0)
        {
            animator.SetTrigger("walk_right");
        }
        else if (vertical < 0)
        {
            animator.SetTrigger("walk_down");
        }
        else if (vertical > 0)
        {
            animator.SetTrigger("walk_up");
        }
    }
}
