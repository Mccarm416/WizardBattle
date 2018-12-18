using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Spell
{
    public AudioClip fizzle;
    public AudioClip hit;
    public GameObject spell;

    private float speed = 500f;
    private double cooldown = 0.5;
    private double nextShot = 0;
    private AudioSource audioSrc;
    private Vector2 origin;
    private string caster;

    void castBasicAttack()
    {
        if (nextShot < Time.time)
        {
            caster = gameObject.tag;
            Aim aim = gameObject.GetComponent<Aim>();
            Vector2 aimDirection = aim.aimDirection;
            aimDirection.Normalize();
            if (aimDirection != new Vector2(0, 0))
            {
                nextShot = Time.time + cooldown;
                Vector2 playerPos = GetComponent<Transform>().position;
                origin = aimDirection + playerPos;
                GameObject basicAttack = Instantiate(spell, origin, Quaternion.identity) as GameObject;
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                basicAttack.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                basicAttack.GetComponent<Rigidbody2D>().velocity = aimDirection * speed;
            }
        }

    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("border"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator waitDestroy()
    {
        //Destroys the object after 1 second to let the audio clip play on destruction
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
