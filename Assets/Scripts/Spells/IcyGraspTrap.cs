using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyGraspTrap : Spell
{
    public int damage = 0;
    private int trapLength = 15;
    public Collider2D casterCollider;
    private double destroyTime;
    public GameObject icyGrasp;
    private Vector2 castOffset = new Vector2(4, 24);//USed to centre the spell over the trap
    void Start()
    {
        base.priority = 7;
        destroyTime = Time.time + trapLength;
    }
    private void Update()
    {
        if (destroyTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player1" || other.gameObject.tag == "player2")
        {

            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            //Cast icy grasp
            Debug.Log("trap hit, caster="+ base.caster +", casting...");
            Debug.Log("non base caster=" + caster);
            Vector2 origin = transform.position;
            origin = origin + castOffset;
            icyGrasp = Instantiate(icyGrasp, origin, Quaternion.identity) as GameObject;
            icyGrasp.GetComponent<IcyGrasp>().caster = base.caster;
            Physics2D.IgnoreCollision(icyGrasp.GetComponent<Collider2D>(), casterCollider);
            //Destroy after animation
            Destroy(gameObject);
            Debug.Log("icyGrasp length=" + icyGrasp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(icyGrasp, icyGrasp.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            //Destroy the trap
            Destroy(gameObject);


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
