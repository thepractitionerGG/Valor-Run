using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    public static GameObject player;
    public static GameObject curretPlatorm;
    bool canTurn = false;

    Vector3 startPosition;
    private void OnCollisionEnter(Collision collision)
    {
        curretPlatorm = collision.gameObject;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        player = this.gameObject;
        GenrateWorld.RunDummy();

        startPosition = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other is BoxCollider && GenrateWorld.lastPlatForm.tag!="platformTSection")
              GenrateWorld.RunDummy();

        if(other is SphereCollider)
        {
            canTurn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other is SphereCollider)
        {
            canTurn = false;
        }
    }

    public void StopJummp()
    {
        anim.SetBool("isJumping", false);
    }

    public void StopMagic()
    {
        anim.SetBool("isMagic", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isMagic", true);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)&&canTurn)
        {
            transform.Rotate(Vector3.up * 90);
            GenrateWorld.dummyTraveller.transform.forward = -transform.forward;
            GenrateWorld.RunDummy();

            if (GenrateWorld.lastPlatForm.tag != "platformTSection")
                GenrateWorld.RunDummy();

            transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow)&&canTurn)
        {
            transform.Rotate(-Vector3.up * 90);
            GenrateWorld.dummyTraveller.transform.forward = -transform.forward;
            GenrateWorld.RunDummy();

            if (GenrateWorld.lastPlatForm.tag != "platformTSection")
                GenrateWorld.RunDummy();
            transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(-0.3f,0,0);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(0.3f, 0, 0);
        }
    }


}
