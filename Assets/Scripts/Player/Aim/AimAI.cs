using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAI : Aim
{
    private GameObject enemy;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("player1");
    }
    void Update()
    {
        //Get position
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;
        //Get player location
        Vector2 enemyPos = enemy.transform.position;
        //Determine the direction 
        base.aimDirection = enemyPos - currentPos;
        base.aimDirection.Normalize();
    }
}
