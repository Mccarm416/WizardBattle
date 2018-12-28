using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTornado : MonoBehaviour
{
    public GameObject spell;

    private Vector2 origin;
    private double cooldown = 5;
    public double nextShot = 0;
    private float speed = 350f;


    public void castTornado()
    {
        if (nextShot <= Time.time)
        {
            nextShot = Time.time + cooldown;
            Aim aim = gameObject.GetComponent<Aim>();
            Vector2 playerPos = GetComponent<Transform>().position;
            Vector2 aimDirection = aim.aimDirection;
            origin = aimDirection + playerPos;
            GameObject torndao = Instantiate(spell, origin, Quaternion.identity) as GameObject;
            torndao.GetComponent<Torndao>().caster = gameObject.tag;
            Physics2D.IgnoreCollision(torndao.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            aimDirection.Normalize();
            torndao.GetComponent<Torndao>().aimDirection = aimDirection;
            torndao.GetComponent<Torndao>().speed = speed;
            //Destroy after animation
            Destroy(torndao, torndao.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
