using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBasicAttack : MonoBehaviour
{

    public GameObject spell;

    private float speed = 530f;
    private double cooldown = 0.7;
    public double nextShot = 0;
    private Vector2 origin;

    public void castBasicAttack()
    {
        if (nextShot < Time.time)
        {
            Aim aim = gameObject.GetComponent<Aim>();
            Vector2 aimDirection = aim.aimDirection;
            aimDirection.Normalize();
            if (aimDirection != new Vector2(0, 0))
            {
                nextShot = Time.time + cooldown;
                Vector2 playerPos = GetComponent<Transform>().position;
                origin = aimDirection + playerPos;
                GameObject basicAttack = Instantiate(spell, origin, Quaternion.identity) as GameObject;
                basicAttack.GetComponent<BasicAttack>().caster = gameObject.tag;
                Physics2D.IgnoreCollision(basicAttack.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                basicAttack.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                GetComponent<Rigidbody2D>().freezeRotation = true;
                basicAttack.GetComponent<Rigidbody2D>().velocity = aimDirection * speed;
            }
        }

    }




}
