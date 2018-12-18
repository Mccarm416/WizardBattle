using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Ability 1"))
        {
            //Fire Lion
            SendMessage("castFireLion");
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            SendMessage("castBasicAttack");
        }
    }
}
