using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastFireLion : MonoBehaviour
{
    private Vector2 origin;
    public GameObject spell;
    private double cooldown = 3;
    public double nextShot = 0;
    private float speed = 525f;


    public void castFireLion()
    {

        if (nextShot <= Time.time)
        {
            nextShot = Time.time + cooldown;
            Aim aim = gameObject.GetComponent<Aim>();
            Vector2 playerPos = GetComponent<Transform>().position;
            Vector2 aimDirection = aim.aimDirection;
            origin = aimDirection + playerPos;
            GameObject fireLion = Instantiate(spell, origin, Quaternion.identity) as GameObject;
            fireLion.GetComponent<FireLion>().caster = gameObject.tag;
            Physics2D.IgnoreCollision(fireLion.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            fireLion.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            aimDirection.Normalize();
            fireLion.GetComponent<Rigidbody2D>().velocity = aimDirection * speed;
            //Destroy after animation
            Destroy(fireLion, fireLion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
