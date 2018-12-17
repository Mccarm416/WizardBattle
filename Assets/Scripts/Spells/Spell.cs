using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    abstract protected void OnCollisionEnter2D(Collision2D other);
}
