using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Movement : MovementController
{
    private Vector2 _currentPos;
    private Vector2 newPos;
    private float moveHorizontal;
    private float moveVertical;
    private Animator animator;
    private Rigidbody2D rBody;
    private int speed = 200;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    public override void Move()
    {

        //Get player input
        float moveHorizontal = Input.GetAxis("Joy2 LeftStickHorizontal");
        float moveVertical = Input.GetAxis("Joy2 LeftStickVertical");
        //Calculating the new point to move to
        Vector2 velocity = new Vector2(moveHorizontal, moveVertical);
        //rBody.velocity = velocity * speed;
        //Check to see if movement animation should play (this should be snappier)
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            animator.SetBool("playerMove", true);
        }
        else
        {
            animator.SetBool("playerMove", false);
        }
        //Move to the new position
        transform.Translate(newPos);
    }
}
