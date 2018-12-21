using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : Aim
{
    void Update()
    {
        float rsHorPos = Input.GetAxis("Joy1 RightStickHorizontal");
        float rsVerPos = Input.GetAxis("Joy1 RightStickVertical");
        base.aimDirection = new Vector2(rsHorPos, rsVerPos);
    }
}
