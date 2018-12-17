using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLion : Spell
{
	private AudioSource audioSrc;
    private Vector2 origin;
    public GameObject spell;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        throw new System.NotImplementedException();
    }

    void castFireLion()
    {
        Debug.Log("FireLion - castFireLion");
        Aim aim = gameObject.GetComponent<Aim>();
        origin = aim.aimDirection;
        GameObject fireLion = Instantiate(spell, origin, Quaternion.identity) as GameObject;
        float angle = Mathf.Atan2(origin.y, origin.x) * Mathf.Rad2Deg;
        fireLion.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Destroy(fireLion, fireLion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
