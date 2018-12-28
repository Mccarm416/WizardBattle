
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torndao : Spell
{
    int damage = 0;
    public Vector2 aimDirection;
    public float speed;
    private Rigidbody2D rBody;

    //Sine wave values
    private float frequency = 6f;
    private float amplitude = 100f;
    private float sinLifetime;



    private void Start()
    {
        base.priority = 9;
        sinLifetime = 0;
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        sinLifetime += Time.fixedDeltaTime;
        rBody.velocity = sinVelocity();
    }

    //Move the tornado along a sine wave
    private Vector2 sinVelocity()
    {
        Vector2 sinUp = new Vector2(-aimDirection.y, aimDirection.x);
        float upSpeed = Mathf.Cos(sinLifetime * frequency) * amplitude * frequency;
        return sinUp * upSpeed + aimDirection * speed;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player1" || other.gameObject.tag == "player2")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            other.collider.SendMessage("onTakeDamage", damage);
        }
        else if (other.gameObject.tag.Equals("spell"))
        {
            Spell hitSpell = other.gameObject.GetComponent<Spell>();
            //Hit own spell
            if (hitSpell.caster.Equals(base.caster))
            {
                Physics2D.IgnoreCollision(hitSpell.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                return;
            }
            else if (hitSpell.priority >= priority)
            {
                Destroy(gameObject);
            }
        }
    }
}
