using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Spell
{
    public AudioClip fizzle;
    public AudioClip hit;

    private int damage = 10;

    void Start()
    {
        base.priority = 1;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource audioSrc = GetComponent<AudioSource>();
        //Enemy player hit
        if (other.gameObject.tag == "player1" || other.gameObject.tag == "player2")
        {
            other.collider.SendMessage("onTakeDamage", damage);
            audioSrc.enabled = true;
            audioSrc.clip = hit;
            audioSrc.volume = 1f;
            audioSrc.Play();
            StartCoroutine(waitDestroy());
        }
        //Hit spell
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
                audioSrc.enabled = true;
                audioSrc.clip = fizzle;
                audioSrc.volume = 1f;
                audioSrc.Play();
                StartCoroutine(waitDestroy());
            }
        }

        else if (other.gameObject.tag.Equals("border"))
        {
            audioSrc.enabled = true;
            audioSrc.clip = fizzle;
            audioSrc.volume = 1f;
            audioSrc.Play();
            StartCoroutine(waitDestroy());
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
