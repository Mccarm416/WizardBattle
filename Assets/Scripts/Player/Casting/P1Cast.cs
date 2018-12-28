using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Cast : Cast
{
    private double globalCooldown = 1;
    private double gcdTime = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Joy1 LB") && Time.time > gcdTime)
        {
            Debug.Log("p1 Fire Lion");

            gcdTime = Time.time + globalCooldown;
            gameObject.GetComponent<CastFireLion>().castFireLion();
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Joy1 RB") && Time.time > gcdTime)
        {
            gcdTime = Time.time + globalCooldown;
            gameObject.GetComponent<CastTornado>().castTornado();
        }
        else if (Input.GetAxis("Joy1 RT") > 0.3)
        {
            gameObject.GetComponent<CastBasicAttack>().castBasicAttack();
        }
        else if (Input.GetAxis("Joy1 LT") > 0.3 && Time.time > gcdTime)
        {
            gcdTime = Time.time + globalCooldown;
            gameObject.GetComponent<CastIcyGraspTrap>().castIcyGraspTrap();
        }
        else if (Input.GetButtonDown("Joy1 A"))
        {
            gameObject.GetComponent<CastDodge>().castDodge();
        }

    }
}
