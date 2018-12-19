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
    int speed;

    private float rightX = 685f;
    private float leftX = -685f;
    private float topY = 372f;
    private float botY = -372f;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Move(int speed)
    {
        this.speed = speed;
        //Controls player movement
        //Get player input
        float moveHorizontal = Input.GetAxis("Joy2 LeftStickHorizontal");
        float moveVertical = Input.GetAxis("Joy2 LeftStickVertical");
        CheckBoundary();
        //Calculating the new point to move to
        newPos.x = moveHorizontal * speed * Time.deltaTime;
        newPos.y = moveVertical * speed * Time.deltaTime;
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

    private void CheckBoundary()
    {
        //Checks the players position against the camera boundary to prevent them from moving off of it
        if (_currentPos.x + (moveHorizontal * speed * Time.deltaTime) < leftX)
        {
            Debug.Log("curPos < leftX");
            moveHorizontal = 0;
        }
        if (_currentPos.x + (moveHorizontal * speed * Time.deltaTime) > rightX)
        {
            Debug.Log("curPos > rightX");
            moveHorizontal = 0;
        }
        if (_currentPos.y + (moveVertical * speed * Time.deltaTime) > topY)
        {
            Debug.Log("curPos > topY");
            moveVertical = 0;
        }
        if (_currentPos.y + (moveVertical * speed * Time.deltaTime) < botY)
        {
            Debug.Log("curPos < botY");
            moveVertical = 0;
        }
    }
}
