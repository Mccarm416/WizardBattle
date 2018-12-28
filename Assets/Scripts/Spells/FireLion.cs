using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLion : Spell
{
    private int damage = 30;

    void Start()
    {
        base.priority = 5;
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
