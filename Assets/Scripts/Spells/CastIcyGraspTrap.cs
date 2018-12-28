using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastIcyGraspTrap : MonoBehaviour
{
    private Vector2 origin;
    public GameObject spell;
    private double cooldown = 5;
    public double nextShot = 0;

    public void castIcyGraspTrap()
    {
        Debug.Log("Casting icy grasp trap. caster=" + gameObject.tag);
        if (nextShot <= Time.time)
        {
            nextShot = Time.time + cooldown;
            Aim aim = gameObject.GetComponent<Aim>();
            Vector2 playerPos = GetComponent<Transform>().position;
            Vector2 aimDirection = aim.aimDirection;
            origin = aimDirection + playerPos;
            GameObject icyGraspTrap = Instantiate(spell, origin, Quaternion.identity) as GameObject;
            icyGraspTrap.GetComponent<IcyGraspTrap>().caster = gameObject.tag;
            icyGraspTrap.GetComponent<IcyGraspTrap>().casterCollider = GetComponent<Collider2D>();
            Debug.Log("Caster assigned");
            Physics2D.IgnoreCollision(icyGraspTrap.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            icyGraspTrap.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
    }
}
