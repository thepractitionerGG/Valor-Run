using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // add swipe control where it works when swipe distance becomes more than 100

    Animator anim;

    public static GameObject player;
    public static GameObject curretPlatorm;
    bool canTurn = false;
    public static bool inAir = false;
    public static bool isDead = false;
    private Vector3 touchStartPosition;
    Vector3 startPosition;
    Rigidbody rb;
    private float minSwipeDistance = 100f;

    int maxPlatformCount = 3;
    float jumpY = 4f;

   

    private void OnCollisionEnter(Collision collision)
    {
        if (inAir) // add a tag array for this and check if any platfomr tag comes it should 
        {
            inAir = false;
            StopJummp(); 
        }
       

        if (collision.gameObject.tag == "Obstacle")
        {
            anim.SetTrigger("isDead");
            isDead = true;
        }

        else
        {
            curretPlatorm = collision.gameObject;
        }
            

    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = this.gameObject;
        GenrateWorld.RunDummy();
        GenrateWorld.RunDummy();

        startPosition = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Coin")
        {
            return;
        }

        if (other is BoxCollider && GenrateWorld.lastPlatForm.tag != "platformTSection") // will generate world if t section is not the lastplatform, for that move to update 
        {
            if (CounEnabledPlatforms() > maxPlatformCount)
                return;
           
            GenrateWorld.RunDummy(); // To Genrate world for more then one tile writ this line of code multiple times, it will only break if there is a t sectin formed after a tsection
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
    void LateUpdate()
    {
      if (isDead) return;

    #if UNITY_EDITOR
            // Code to execute when running in the Unity Editor
            MovementWindows();
    #endif

    #if UNITY_ANDROID
            MovementAndroid();
    #endif
    }

    private void MovementAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Store touch start position
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Calculate swipe distance
                float swipeDistanceSide = touch.position.x - touchStartPosition.x;
                float swipeUpDistance = touch.position.y - touchStartPosition.y;
                float swipeDownDistance = touchStartPosition.y - touch.position.y;

                if (Mathf.Abs(swipeDistanceSide) > Mathf.Abs(swipeUpDistance))
                {
                    if (Mathf.Abs(swipeDistanceSide) > minSwipeDistance)
                    {

                        // Check swipe direction
                        if (swipeDistanceSide < 0)
                        {
                            // Swipe left
                            MoveLeft();
                        }
                        else if (swipeDistanceSide > 0)
                        {
                            // Swipe right
                            MoveRight();
                        }
                    }
                }

                else
                {
                    if (Mathf.Abs(swipeUpDistance) > minSwipeDistance)
                    {
                        Jump();
                    }
                    else if (Mathf.Abs(swipeDownDistance) > minSwipeDistance)
                    {
                        RollDown(); 
                    }

                }

            }
        }
    }

    private void MovementWindows()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            Jump();
        }

        else if (Input.GetKeyDown(KeyCode.S)&& inAir)
        {
            RollDown();
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isMagic", true);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            transform.Rotate(Vector3.up * 90);
            GenrateWorld.dummyTraveller.transform.forward = -transform.forward;
            GenrateWorld.RunDummy();

            if (GenrateWorld.lastPlatForm.tag != "platformTSection") // to genrate further as we can call genreate.runDmmy multiple times to make unlimited world
                GenrateWorld.RunDummy();

            transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z); // for aliging the position of our player after a turn;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            transform.Rotate(-Vector3.up * 90);
            GenrateWorld.dummyTraveller.transform.forward = -transform.forward;
            GenrateWorld.RunDummy();

            if (GenrateWorld.lastPlatForm.tag != "platformTSection")
                GenrateWorld.RunDummy();

            transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
        }

        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x < GameManager.maxLeftSide)
        {
            transform.Translate(-2.5f, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x > GameManager.maxRightSide)
        {
            transform.Translate(2.5f, 0, 0);
        }
    }

    void MoveLeft()
    {
        if (transform.position.x < GameManager.maxLeftSide)
        {
            transform.Translate(-2.5f, 0, 0);
        }
    }

    void MoveRight()
    {
        if (transform.position.x > GameManager.maxRightSide)
        {
            transform.Translate(2.5f, 0, 0);
        }
    }

    void Jump()
    {
        StopAllCoroutines();
        StartCoroutine(JumpCoroutine());
        anim.SetBool("isJumping", true);

        /// old addofrce code
        //if (!inAir)
        //{
        //    inAir = true;
        //    anim.SetBool("isJumping", true);
        //    rb.AddForce(Vector3.up * 650f);
        //}
    }

    IEnumerator JumpCoroutine()
    {
        while (transform.position.y < jumpY)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3
                (transform.position.x, transform.position.y + 4, transform.position.z), Time.deltaTime * 2f);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(RollDownCoroutine());

    }

    void RollDown()
    {
        if (!inAir)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(RollDownCoroutine());

    }

    IEnumerator RollDownCoroutine()
    {
        while (transform.position.y > startPosition.y)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3
                (transform.position.x, startPosition.y, transform.position.z), Time.deltaTime * 4f);
            yield return null;
        }
    }


    private int CounEnabledPlatforms() // this function is for counting howmany platfomrs are active in gameview ain game time and the tag is helping us to get idea
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("platformStraight");

        int count = 0;

        foreach (GameObject platform in platforms)
        {
            if (platform.activeInHierarchy)
                count++;
        }

        return count;
    }
}
