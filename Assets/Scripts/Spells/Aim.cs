using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    private Spell spell;
    public Vector2 aimDirection;

    // Update is called once per frame
    void Update()
    {
        float rsHorPos = Input.GetAxis("Right Stick Horizontal");
        float rsVerPos = Input.GetAxis("Right Stick Vertical");
        aimDirection = new Vector2(rsHorPos, rsVerPos);
    }
}
