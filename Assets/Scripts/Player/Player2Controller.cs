﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Controller : Player {
    //Used by first player
    MovementController movementController;
    private Rigidbody2D rBody;
    private Transform _transform;
    private Vector2 _currentPos;
    private AudioSource playerAudio;
    private Camera camera;
    private Animator animator;
    private Player1Controller player1;

    private bool isEnabled { get; set; }

    public float fireRate { get; set; }


    //Death variables
    bool dying;
    public AudioClip deathSound;
    public AudioClip death01;
    public AudioClip death02;

    void Start()
    {
        enabled = true;
        movementController = GetComponent<MovementController>();
        Debug.Log("p2 movement controller tag=" + movementController.gameObject.tag);
        Health = 100;
        animator = GetComponent<Animator>();
        camera = Camera.main;
        dying = false;
        rBody = GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
        _currentPos = _transform.position;
        hits = 0;
        hitsTaken = 0;
        player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Player1Controller>();

    }

    void Update()
    {

        _transform = gameObject.GetComponent<Transform>();
        _currentPos = _transform.position;

        if (!dying && enabled)
        {
            Move();
        }
        else if (dying)
        {
            //Check to see if the player is dying and if the camera should follow them
            camera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, -10);
        }
    }

    void Move()
    {
        movementController.Move();
    }

    //Collision methods
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("player2"))
        {
            rBody.isKinematic = true;
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = 0f;
        }

        else if (other.gameObject.tag.Equals("border"))
        {
            rBody.isKinematic = true;
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = 0f;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("player2"))
        {
            rBody.isKinematic = false;
        }
        else if (other.gameObject.tag.Equals("border"))
        {
            rBody.isKinematic = false;
        }
    }


    public void Death()
    {
        //Pass player controllers to the next scenes controller
        Player2Controller player2 = GetComponent<Player2Controller>();
        Destroy(Camera.main.GetComponent<FixedCameraFollow>());
        Destroy(player1);
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(GetComponent<P1Cast>());
        Destroy(GetComponent<P1Movement>());
        Destroy(GetComponent<PolygonCollider2D>());
        EndScreenController.player1 = player1;
        EndScreenController.player2 = player2;
        Destroy(player1.gameObject);
        //Stop all sound then start the cinematic death
        StopSound();
        MoveCamera();
        StartCoroutine(Scream());
    }

    void MoveCamera()
    {
        //Method used to move the camera and zoom it up to the player when they die
        dying = true;
        AudioSource cameraAudio = camera.GetComponent<AudioSource>();
        //Move the camera
        camera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, -10);
        //Increases the cameras resolution to zoom in on the dying player
        camera.orthographicSize = Mathf.Lerp(520, 60, 20);
        Time.timeScale = 1f;
        cameraAudio.clip = deathSound;
        cameraAudio.enabled = true;
        cameraAudio.Play();
    }

    IEnumerator Scream()
    {
        Debug.Log("deciding scream");
        //Decide the death scream to use

        //Generate a random number between 1-2 to select a death scream
        int randomScream = Random.Range(1, 3);
        playerAudio = GetComponent<AudioSource>();
        if (randomScream == 1)
        {
            playerAudio.clip = death01;
        }
        else if (randomScream == 2)
        {
            playerAudio.clip = death02;
        }
        //Play the death animation
        animator.SetTrigger("playerDeath");
        playerAudio.enabled = true;
        //Wait 1 second then play the death scream
        yield return new WaitForSeconds(1.9f);
        Debug.Log("screaming");
        playerAudio.Play();
        //Wait 5 seconds then load the end screen
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }


    void StopSound()
    {
        AudioSource[] audioSrcs = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource aSrc in audioSrcs)
        {
            aSrc.Stop();
        }
    }

    public override void disable()
    {
        if (enabled)
        {
            enabled = false;
        }

    }
    public override void enable()
    {
        if (!enabled)
        {
            gameObject.AddComponent<P2Cast>();
            enabled = true;
        }

    }

    void onTakeDamage(int damage)
    {
        Health = Health - damage;
        hitsTaken++;
        if (Health <= 0)
        {
            Death();
        }
    }

    void onTakeColdDamage(int[] coldDamage)
    {
        Debug.Log( "cold dmg taken");
        Health = Health - coldDamage[0];
        hitsTaken++;
        if (Health <= 0)
        {
            Death();
        }
        StartCoroutine(coldEffect(coldDamage[1], coldDamage[2]));
    }

    IEnumerator coldEffect(int speedReduction, int coldLength)
    {
        Debug.Log( "cold effect applying");

        CastDodge dodge = GetComponent<CastDodge>();
        dodge.increaseSpeed = dodge.increaseSpeed - (speedReduction * 8);
        movementController.speed = movementController.speed - speedReduction;
        GetComponent<SpriteRenderer>().color = new Color(0, 160 , 255, 1);
        yield return new WaitForSeconds(coldLength);
        GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("cold effect removing");


        movementController.speed = movementController.speed + speedReduction;
        dodge.increaseSpeed = dodge.increaseSpeed + (speedReduction * 8);
    }
}
