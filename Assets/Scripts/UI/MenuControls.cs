using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages input on the main menu and title screen
public class MenuControls : MonoBehaviour
{
    private double buttonBuffer = 0.5;
    private double nextButton = 0;
    private double movementBuffer = 0.3;
    private double nextMovement = 0;

    public int getMoveInput()
    {
        int move = 0;
        if (nextMovement <= Time.unscaledTime)
        {
            if (Input.GetAxis("Joy1 LeftStickVertical") > 0 || Input.GetAxis("Joy2 LeftStickVertical") > 0)
            {
                //Move the cursor up
                move = -1;
                nextMovement = Time.unscaledTime + movementBuffer;
            }
            else if (Input.GetAxis("Joy1 LeftStickVertical") < 0 || Input.GetAxis("Joy2 LeftStickVertical") < 0)
            {
                //Move the cursor down
                move = 1;
                nextMovement = Time.unscaledTime + movementBuffer;
            }
        }
        return move;
    }

    public bool getEnterButton()
    {
        if (nextButton <= Time.unscaledTime)
        {
            if (Input.GetButtonDown("Joy1 A") || Input.GetButtonDown("Joy2 A"))
            {
                nextButton = buttonBuffer + Time.unscaledTime;
                return true;
            }
        }
        return false;
    }

    public bool getStartButton()
    {
        if (Input.GetButtonDown("Joy1 Start") || Input.GetButtonDown("Joy2 Start"))
        {
            Debug.Log("Start hit");

            nextButton = buttonBuffer + Time.unscaledTime;
            return true;
        }
        return false;
    }

    public bool getCancelButton()
    {
        if (nextButton < Time.unscaledTime)
        {
            if (Input.GetButtonDown("Joy1 B") || Input.GetButtonDown("Joy2 B"))
            {
                nextButton = buttonBuffer + Time.unscaledTime;
                return true;
            }
        }
        return false;
    }
}
