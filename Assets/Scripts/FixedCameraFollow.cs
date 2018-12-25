using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class FixedCameraFollow : MonoBehaviour
{
    private Camera camera;
    private Transform player1;
    private Transform player2;
    private float cameraMinSize = 400;
    private float cameraMaxSize = 720;
    private float followTimeDelta = 0.15f;

    void Start()
    {
        camera = Camera.main;
        player1 = GameObject.FindGameObjectWithTag("player1").GetComponent<Transform>();
        player2 = GameObject.FindGameObjectWithTag("player2").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {


        Vector3 midpoint = (player1.position + player2.position) / 2f;

        //Distance between two players
        float distance = (player1.position - player2.position).magnitude;
        distance = Mathf.Round(distance);

        Vector3 cameraDestination = midpoint - camera.transform.forward;
        float x = Mathf.Round(cameraDestination.x);
        float y = Mathf.Round(cameraDestination.y);
        cameraDestination = new Vector3(x, y, -1);


        camera.transform.position = Vector3.Slerp(camera.transform.position, cameraDestination, followTimeDelta);
        //Snap to the destination when close enough
        if ((cameraDestination - camera.transform.position).magnitude <= 0.05f)
        {
            camera.transform.position = cameraDestination;
        }


        if(distance < cameraMinSize)
        {
            camera.orthographicSize = cameraMinSize;
        }
        else if( distance> cameraMaxSize)
        {
            camera.orthographicSize = cameraMaxSize;
        }
        else
        {
            camera.orthographicSize = distance;

        }
    }
}
