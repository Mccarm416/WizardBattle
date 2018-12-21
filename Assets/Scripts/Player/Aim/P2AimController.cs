using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2AimController : Aim
{

    void Update()
    {
        float rsHorPos = Input.GetAxis("Joy2 RightStickHorizontal");
        float rsVerPos = Input.GetAxis("Joy2 RightStickVertical");
        base.aimDirection = new Vector2(rsHorPos, rsVerPos);
    }
}
