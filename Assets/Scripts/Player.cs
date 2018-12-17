using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class  Player : MonoBehaviour
{
    Rigidbody2D rBody;
    int health;
    abstract protected void Move();
}
