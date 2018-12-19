using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public int priority;
    public string caster;

    abstract protected void OnCollisionEnter2D(Collision2D other);
}
