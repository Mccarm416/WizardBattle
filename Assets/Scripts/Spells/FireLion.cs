using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLion : Spell
{
    private AudioSource audioSrc;
    private Vector2 origin;
    public GameObject spell;
    private double cooldown = 3;
    private double nextShot = 0;
    private int damage = 30;
    private float speed = 500f;
    private string caster;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "p1" || other.gameObject.tag == "p2" && !other.gameObject.tag.Equals(caster));
        {
            Debug.Log("Hit");
            other.collider.SendMessage("onTakeDamage", damage);
        }
    }

    void castFireLion()
    {
        if (nextShot <= Time.time)
        {
            nextShot = Time.time + cooldown;
            caster = gameObject.tag;
            Aim aim = gameObject.GetComponent<Aim>();
            Vector2 playerPos = GetComponent<Transform>().position;
            Vector2 aimDirection = aim.aimDirection;
            origin = aimDirection + playerPos;
            GameObject fireLion = Instantiate(spell, origin, Quaternion.identity) as GameObject;
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
