﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Cast : Cast
{
    private double globalCooldown = 1;
    private double gcdTime = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Joy2 LB") && Time.time > gcdTime)
        {
            Debug.Log("p2 Fire Lion");
            gcdTime = Time.time + globalCooldown;
            gameObject.GetComponent<CastFireLion>().castFireLion();
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Joy2 RB") && Time.time > gcdTime)
        {
            gcdTime = Time.time + globalCooldown;
            gameObject.GetComponent<CastTornado>().castTornado();
        }
        else if (Input.GetButtonDown("Joy2 RightStickBtn"))
        {
            gameObject.GetComponent<CastBasicAttack>().castBasicAttack();
        }
    }
}