using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTornado : MonoBehaviour
{
    public GameObject spell;

    private Vector2 origin;
    private double cooldown = 7;
    public double nextShot = 0;
    private float speed = 600f;


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
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            torndao.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
            aimDirection.Normalize();
            torndao.GetComponent<Torndao>().aimDirection = aimDirection;
            torndao.GetComponent<Torndao>().speed = speed;
            torndao.GetComponent<Rigidbody2D>().velocity = aimDirection * speed;
            //Destroy after animation
            Destroy(torndao, torndao.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
