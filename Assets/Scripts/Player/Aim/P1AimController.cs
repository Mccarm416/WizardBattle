using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1AimController : Aim
{
    public GameObject crosshair;
    private void Start()
    {
       crosshair = Instantiate(crosshair, gameObject.transform.position, Quaternion.identity);
       crosshair.SetActive(false);
    }
    void Update()
    {
        float rsHorPos = Input.GetAxis("Joy1 RightStickHorizontal");
        float rsVerPos = Input.GetAxis("Joy1 RightStickVertical");
        base.aimDirection = new Vector2(rsHorPos, rsVerPos);

        //Crosshair placement
        if(base.aimDirection != Vector2.zero)
        {
            crosshair.SetActive(true);
            Vector2 playerPos = GetComponent<Transform>().position;
            crosshair.transform.position = (aimDirection * 100) + playerPos;
        }
        else
        {
            crosshair.SetActive(false);
        }
    }
}
