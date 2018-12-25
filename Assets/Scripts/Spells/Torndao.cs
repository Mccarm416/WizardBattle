
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torndao : Spell
{
    int damage = 0;
    public Vector2 aimDirection;
    public float speed;
    private double changeDirectionEvery;
    private bool fireWithOffset = true;
    private Vector2 aimOffset;
    private void Start()
    {
        base.priority = 5;
        //Get the lifespan of the object then divide by 4 to very the tornado's path
        float lifespan = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        changeDirectionEvery = lifespan / 4;
    }

    private void Update()
    {
    }

    private void instantiateAimOffset()
    {
        if (aimDirection == null)
            return;
        else if (aimOffset != null)
            aimOffset = aimDirection + new Vector2(1, 1);//Find a way to offset the original shot
    }
    protected override void OnCollisionEnter2D(Collision2D other)
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
