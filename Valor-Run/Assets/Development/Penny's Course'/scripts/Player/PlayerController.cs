using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    public static GameObject player;
    public static GameObject curretPlatorm;
    bool canTurn = false;
    public static bool isDead = false;
    Vector3 startPosition;
    Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            anim.SetTrigger("isDead");
            isDead = true;
        }

        else
            curretPlatorm = collision.gameObject;
       
       
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = this.gameObject;
        GenrateWorld.RunDummy();
        
        startPosition = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && GenrateWorld.lastPlatForm.tag != "platformTSection") // will generate world if t section is not the lastplatform, for that move to update 
        {
            GenrateWorld.RunDummy(); // To Genrate world for more then one tile writ this line of code multiple times, it will only break if there is a t sectin formed after a tsection

            GenrateWorld.RunDummy();
            GenrateWorld.RunDummy();
            GenrateWorld.RunDummy();
            GenrateWorld.RunDummy();
        }

        if (other is SphereCollider)
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
            rb.AddForce(Vector3.up * 200);
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

            if (GenrateWorld.lastPlatForm.tag != "platformTSection") // to genrate further as we can call genreate.runDmmy multiple times to make unlimited world
                GenrateWorld.RunDummy();

            transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z); // for aliging the position of our player after a turn;
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
            transform.Translate(-0.5f,0,0);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(0.5f, 0, 0);
        }
    }


}
