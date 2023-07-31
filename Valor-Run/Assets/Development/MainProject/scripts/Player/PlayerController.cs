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
            else if ( Mathf.Abs(touch.position.x-touchStartPosition.x)> 100 || Mathf.Abs( touch.position.y- touchStartPosition.y) > 100)
            {
                // Calculate swipe distance
                float swipeDistanceSide = touch.position.x - touchStartPosition.x;
                float swipeUpDistance = touch.position.y - touchStartPosition.y;
                float swipeDownDistance = touchStartPosition.y - touch.position.y;

                if (Mathf.Abs(swipeDistanceSide) > Mathf.Abs(swipeUpDistance))
                {
                    if (Mathf.Abs(swipeDistanceSide) > minSwipeDistance && touch.phase==TouchPhase.Ended)
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
                    Debug.Log(swipeDownDistance+" "+swipeUpDistance);
                    if (swipeUpDistance > minSwipeDistance)
                    {
                        Jump();
                    }
                    else if (swipeDownDistance > minSwipeDistance)
                    {
                        GoDown(); 
                    }

                }

            }
        }
    }

    private void MovementWindows()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            GoDown();
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isMagic", true);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            RightTurn();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            LeftTurn();
        }

        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x < GameManager.maxLeftSide)
        {
            MoveLeft();
        }

        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x > GameManager.maxRightSide)
        {
            MoveRight();
        }
    }

    private void RightTurn()
    {
        transform.Rotate(Vector3.up * 90);
        GenrateWorld.dummyTraveller.transform.forward = -transform.forward;
        GenrateWorld.RunDummy();

        if (GenrateWorld.lastPlatForm.tag != "platformTSection") // to genrate further as we can call genreate.runDmmy multiple times to make unlimited world
            GenrateWorld.RunDummy();

        transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z); // for aliging the position of our player after a turn;
    }

    private void LeftTurn()
    {
        transform.Rotate(-Vector3.up * 90);
        GenrateWorld.dummyTraveller.transform.forward = -transform.forward;
        GenrateWorld.RunDummy();

        if (GenrateWorld.lastPlatForm.tag != "platformTSection")
            GenrateWorld.RunDummy();

        transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z);
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
        if (!inAir)
        {
            inAir = true;
            StopAllCoroutines();
            StartCoroutine(JumpCoroutine());
        }
        //anim.SetBool("isJumping", true);

    }

    IEnumerator JumpCoroutine()
    {
       
        float initialJumpSpeed = 6f; // Initial jump speed
        float jumpDuration = 1f;   // Total jump duration in seconds
        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            float normalizedTime = elapsedTime / jumpDuration;
            float jumpHeight = Mathf.Lerp(initialJumpSpeed, 0f, normalizedTime);

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + jumpHeight * Time.deltaTime,
                transform.position.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(GoDownCoroutine());

    }

    
    void GoDown()
    {
        if (!inAir)
        {
            return;
        }
        inAir = false;
        StopAllCoroutines();
        StartCoroutine(SlideDownCoroutine(.2f));
    }

    IEnumerator GoDownCoroutine()
    {
        float rollSpeed = 6f; // Speed at which the character rolls down
        float minHeight = 1.8f; // The lowest height you want the character to reach

        while (transform.position.y > minHeight)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - rollSpeed * Time.deltaTime,
                transform.position.z
            );

            // Ensure the character doesn't go below the minimum height
            if (transform.position.y < minHeight)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    minHeight,
                    transform.position.z
                );
            }

            yield return null;
        }
    }

    IEnumerator SlideDownCoroutine(float slideDuration)
    {
        float initialPosition = transform.position.y;
        float targetPosition = 1.8f; // The ground level or minimum height
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            float normalizedTime = elapsedTime / slideDuration;
            float slideHeight = Mathf.Lerp(initialPosition, targetPosition, normalizedTime);

            transform.position = new Vector3(
                transform.position.x,
                slideHeight,
                transform.position.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure to set the exact target position to avoid any inaccuracies
        transform.position = new Vector3(
            transform.position.x,
            targetPosition,
            transform.position.z
        );
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
