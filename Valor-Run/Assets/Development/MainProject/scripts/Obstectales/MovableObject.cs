using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private float speedArrow;
    private float speedElephant;

    public void SetSpeedZero()
    {
        speedArrow = 0;
        speedElephant = 0;
    }

    public void SetSpeedRequired()
    {
        speedArrow = 1f;
        speedElephant = .05f;
    }
    private void FixedUpdate()
    {
        if(gameObject.name=="Arrow")
            transform.position += PlayerController.player.transform.forward * -speedArrow;

        if(gameObject.name=="Elephant")
            transform.position += PlayerController.player.transform.forward * -speedElephant;
    }
}
     
