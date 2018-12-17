using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Fire Lion
            Debug.Log("Cast - Fire Lion");
            SendMessage("castFireLion");
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            //Magic missile
        }
    }
}
