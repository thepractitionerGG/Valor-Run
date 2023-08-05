using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObstacle : MonoBehaviour
{
    private float speedArrow = 0;
    private float speedElephant = 0;
    [SerializeField] Vector3 startingPos;

    private void Awake()
    {
        startingPos = gameObject.transform.position;
    }
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
        if (GameManager.gameManagerSingleton.GetGameState() != GameManager.GameState.Running)
        {
            StopAnimation();
            return;
        }

        //if (PlayerController.isDead) shifting from is dead to inMenu or running Enum
     

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
        // stop animation here
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
     
