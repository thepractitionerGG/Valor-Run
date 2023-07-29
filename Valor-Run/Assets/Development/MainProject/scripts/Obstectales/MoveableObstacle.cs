using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObstacle : MonoBehaviour
{
    private float speedArrow = 0;
    private float speedElephant = 0;
    private MeshRenderer mrs;
    [SerializeField] Vector3 startingPos;

    private void Start()
    {
        SetSpeedRequired();
    }
    public void SetSpeedZero()
    {
        speedArrow = 0;
        speedElephant = 0;
    }

    public void SetSpeedRequired()
    {
        speedArrow = .30f;
        speedElephant = .1f;
    }
    private void FixedUpdate()
    {
        if (PlayerController.isDead)
        {
            StopAnimation();
            return;
        }

        if(gameObject.name=="Arrow")
            transform.position += PlayerController.player.transform.forward * -speedArrow;

        if(gameObject.name=="Elephant")
            transform.position += PlayerController.player.transform.forward * -speedElephant;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            speedArrow = 0;
            speedElephant = 0f;
            if (gameObject.GetComponent<Animator>() != null)
            {
                StopAnimation();
            }
        }
    }

    private void StopAnimation()
    {
        
    }

    private void OnEnable()
    {
        transform.position = startingPos;
        SetSpeedRequired();
    }

    private void OnDisable()
    {
        SetSpeedZero();
    }
}
     
