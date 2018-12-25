using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages input on the main menu and title screen
public class TitleScreenControls : MonoBehaviour
{
    private double buttonBuffer = 0.2;
    private double nextButton = 0;
    private double movementBuffer = 0.3;
    private double nextMovement = 0;

    public int getMoveInput()
    {
        int move = 0;
        if (nextMovement < Time.time)
        {
            if (Input.GetAxis("Joy1 LeftStickVertical") > 0 || Input.GetAxis("Joy2 LeftStickVertical") > 0)
            {
                //Move the cursor up
                move = -1;
                nextMovement = Time.time + movementBuffer;
            }
            if (Input.GetAxis("Joy1 LeftStickVertical") < 0 || Input.GetAxis("Joy2 LeftStickVertical") < 0)
            {
                //Move the cursor down
                move = 1;
                nextMovement = Time.time + movementBuffer;
            }
        }
        return move;
    }

    public bool getEnterButton()
    {
        if (nextButton < Time.time)
        {
            if (Input.GetButtonDown("Joy1 A") || Input.GetButtonDown("Joy2 A"))
            {
                nextButton = buttonBuffer + Time.time;
                return true;
            }
        }
        return false;
    }

    public bool getStartButton()
    {
        if (Input.GetButtonDown("Joy1 Start") || Input.GetButtonDown("Joy2 Start"))
        {
            nextButton = buttonBuffer + Time.time;
            return true;
        }
        return false;
    }

    public bool getCancelButton()
    {
        if (nextButton < Time.time)
        {
            if (Input.GetButtonDown("Joy1 B") || Input.GetButtonDown("Joy2 B"))
            {
                nextButton = buttonBuffer + Time.time;
                return true;
            }
        }
        return false;
    }
}
