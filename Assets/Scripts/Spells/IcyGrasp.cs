using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyGrasp : Spell
{
    private int damage = 15;
    private int speedReduction = 35;
    private int coldLength = 3;

    private void Start()
    {
        base.priority = 4;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "player1" || other.gameObject.tag == "player2")
        {
            Debug.Log("Icy grasp hit! caster=" + caster);
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            int[] coldDamage = new int[] { damage, speedReduction, coldLength };
            other.collider.SendMessage("onTakeColdDamage", coldDamage);
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
