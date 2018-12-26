using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastDodge : MonoBehaviour
{
    public AudioClip dodgeSound;
    private double dodgeCd = 2;
    private double dodgeLength = 0.08;
    private double dodgeFixTime = 0;
    private double lastDodge = 0;
    private float reduceDrag = 1.7f;
    private int increaseSpeed = 500;
    private bool fixDragAndSpeed = false;
    private AudioSource audioSrc;
    private Rigidbody2D rBody;
    private  MovementController movementController;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        movementController = GetComponent<P1Movement>();
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        fixDodge();
    }

    public void castDodge()
    {
        if (lastDodge <= Time.time)
        {
            audioSrc.clip = dodgeSound;
            audioSrc.enabled = true;
            audioSrc.Play();
            audioSrc.volume = 0.2f;
            Debug.Log("Dodging!");
            lastDodge = Time.time + dodgeCd;
            dodgeFixTime = Time.time + dodgeLength;
            rBody.drag = rBody.drag - reduceDrag;
            rBody.angularDrag = rBody.angularDrag - reduceDrag;
            GetComponent<MovementController>().speed = GetComponent<MovementController>().speed + increaseSpeed;
            fixDragAndSpeed = true;
        }
    }
    void fixDodge()
    {
        if (fixDragAndSpeed && Time.time >= dodgeFixTime)
        {
            Debug.Log("Fixing dodge");
            rBody.drag = rBody.drag + reduceDrag;
            rBody.angularDrag = rBody.angularDrag + reduceDrag;
            movementController.speed = GetComponent<P1Movement>().speed - increaseSpeed;
            fixDragAndSpeed = false;
        }
    }
}
