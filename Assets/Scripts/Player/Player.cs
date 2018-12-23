using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public int Health;
    public int hitsTaken;
    public int hits;

    public abstract void enable();
    public abstract void disable();
}
