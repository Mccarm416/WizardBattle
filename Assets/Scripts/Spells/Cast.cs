using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    private double globalCooldown = 1;
    private double gcdTime = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Joy1 LB") && Time.time > gcdTime)
        {
            gcdTime = Time.time + globalCooldown;
            gameObject.GetComponent<CastFireLion>().castFireLion();
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Joy1 RB") && Time.time > gcdTime)
        {
            gcdTime = Time.time + globalCooldown;
            gameObject.GetComponent<CastTornado>().castTornado();
        }
        else if (Input.GetButtonDown("Joy1 RightStickBtn"))
        {
            gameObject.GetComponent<CastBasicAttack>().castBasicAttack();
        }
    }
}
