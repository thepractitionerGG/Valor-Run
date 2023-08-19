using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _anim;
    public static GameObject _player;
    public static GameObject _curretPlatorm;
    public static PlayerController _playerController;

    bool _canTurn = false;

    public static bool _inAir = false;
    public static bool _isDead = false;

    private Vector3 _touchStartPosition;
    Vector3 _playerStartPosition;

    int _maxPlatformCount = 3;

    [SerializeField] private float _minSwipeDistance = 10f;
    float _heightOnGround;
    float _capsulColliderHeightAtStart;
    [SerializeField] float _jumpDuration;
    [SerializeField] float _jumpSpeed;

    public Rigidbody _rb;

    private void Start()
    {
        _playerController = this;
        _player = this.gameObject;
        _playerStartPosition = _player.transform.position;
        _heightOnGround = _playerStartPosition.y;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _capsulColliderHeightAtStart = GetComponent<CapsuleCollider>().height;
       
        GenrateWorld.RunDummy();
    }

    public void StartRunning()
    {
        _anim.SetBool("isRunning", true);
        GetComponent<AudioSource>().enabled = true;
        GetComponent<AudioSource>().volume = AudioSettings.audioSettings.SoundVolume;
        GenrateWorld.RunDummy();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            DeathSequence(collision.gameObject.name);
            return;
        }

        if (_inAir) // add a tag array for this and check if any platfomr tag comes it should 
        {
            _rb.useGravity = false;
            _inAir = false;
            StopJummp(); 
        }

        else
        {
            _curretPlatorm = collision.gameObject;
            GetComponent<AudioSource>().enabled = true;
        }
    }

    public void DeathSequence(string collision)
    {
        // do the shlock tranissiton here #
        GetComponent<AudioSource>().enabled = false;
        VfxAndAudio(collision);

        _anim.SetTrigger("isDead");
        SaveScore();
        GameManager.gameManagerSingleton.UpdateRetryScreen();

        GameManager.gameManagerSingleton.SetGameState(GameManager.GameState.InMenu);
        GameManager.gameManagerSingleton._retryUI.SetActive(true);
        GameManager.gameManagerSingleton._inGameUi.SetActive(false);

        _rb.useGravity = true;
    }

    private void SaveScore()
    {
        GetComponent<PlayerDataManager>().SavePlayerData(GameManager.gameManagerSingleton.distance
                                                        , GameManager.gameManagerSingleton.coins,
                                                        GameManager.gameManagerSingleton.wings);
    }

    private void VfxAndAudio(string collision) // if a new obstalce is created its vfx and audio shall be addded here and a better way to distanguish ti should be there
    {
        AudioPlayer.audioPlayerSingle.PlayAudioOnce(GameManager.gameManagerSingleton.audioData.ObstacleHit, transform, AudioSettings.audioSettings.SoundVolume);


        if (collision == "Elephant")
        {
            VFXController._vFXControllerSingle.DoVfxEffect(GameManager.gameManagerSingleton.vfxData.ElephantHit, 
                                                    new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + .5f));
        }

        if (collision == "Arrow")
        {
            VFXController._vFXControllerSingle.DoVfxEffect(GameManager.gameManagerSingleton.vfxData.ArrowHit, 
                    new Vector3(transform.position.x, transform.position.y + 2, transform.position.z + .5f));
        }

        if (collision == "KillPlane")
        {
            // do here 
        }

        else if(collision!= "Elephant"|| collision != "Arrow")
        {
            VFXController._vFXControllerSingle.DoVfxEffect(GameManager.gameManagerSingleton.vfxData.FireMonsterHit, this.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Coin")
        {
            return;
        }

        if (other is BoxCollider && GenrateWorld.lastPlatForm.tag != "platformTSection") 
            // will generate world if t section is not the lastplatform, for that move to update 
        {
            if (CounEnabledPlatforms() > _maxPlatformCount)
                return;
           
            GenrateWorld.RunDummy(); // To Genrate world for more then one tile writ this line of code multiple times, it will only break if there is a t sectin formed after a tsection
            GenrateWorld.RunDummy();
        }

        if (other is SphereCollider)
        {
            _canTurn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other is SphereCollider)
        {
            _canTurn = false;
        }
    }

    public void StopJummp()
    {
        _anim.SetBool("isJumping", false);
    }

    public void StopMagic()
    {
        _anim.SetBool("isMagic", false);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = AudioSettings.audioSettings.SoundVolume;

        if (GameManager.gameManagerSingleton.GetGameState() != GameManager.GameState.Running) 
        { GetComponent<AudioSource>().enabled = false; return; }

        if (transform.position.y > 8)
        {
            _rb.useGravity=true;
        }
       
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
        if (GameManager.gameManagerSingleton.touchDisabled)
              return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Store touch start position
                _touchStartPosition = touch.position;
            }
            else if ( Mathf.Abs(touch.position.x-_touchStartPosition.x)> _minSwipeDistance || Mathf.Abs( touch.position.y- _touchStartPosition.y) > _minSwipeDistance)
            {
                // Calculate swipe distance
                float swipeDistanceSide = touch.position.x - _touchStartPosition.x;
                float swipeUpDistance = touch.position.y - _touchStartPosition.y;
                float swipeDownDistance = _touchStartPosition.y - touch.position.y;

                if (Mathf.Abs(swipeDistanceSide) > Mathf.Abs(swipeUpDistance))
                {
                    if (Mathf.Abs(swipeDistanceSide) > _minSwipeDistance)
                    {
                        AudioPlayer.audioPlayerSingle.PlayAudioOnce(GameManager.gameManagerSingleton.audioData.ArjunSlidingLeftRight, transform, AudioSettings.audioSettings.SoundVolume);
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
                   
                    if (swipeUpDistance > _minSwipeDistance)
                    {
                        Jump();
                    }
                    else if (swipeDownDistance > _minSwipeDistance) // between the following two function, only one will be called according to the position of the player
                    {
                        SlideDown();
                        SlideUnder();
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
            SlideDown();
            SlideUnder();
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            _anim.SetBool("isMagic", true);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && _canTurn)
        {
            RightTurn();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _canTurn)
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

        transform.position = new Vector3(_playerStartPosition.x, transform.position.y, _playerStartPosition.z); // for aliging the position of our player after a turn;
    }

    private void LeftTurn()
    {
        transform.Rotate(-Vector3.up * 90);
        GenrateWorld.dummyTraveller.transform.forward = -transform.forward;
        GenrateWorld.RunDummy();

        if (GenrateWorld.lastPlatForm.tag != "platformTSection")
            GenrateWorld.RunDummy();

        transform.position = new Vector3(_playerStartPosition.x, transform.position.y, _playerStartPosition.z);
    }

    void MoveLeft()
    {
        _anim.SetTrigger("leftTurn");
        if (transform.position.x < GameManager.maxLeftSide)
        {
            StartCoroutine(MoveLeftCoroutine(.15f));
        }
    }
    IEnumerator MoveLeftCoroutine(float slideDuration)
    {

        float initialPosition = transform.position.x;

        float targetPosition=0;

        if (transform.position.x>=-2.5f && transform.position.x < 0)
        {
            targetPosition = 0; 
        }

        if (transform.position.x >= 0f && transform.position.x < 2.5f)
        {
            targetPosition = 2.5f;
        }

        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            float normalizedTime = elapsedTime / slideDuration;
            float slideHeight = Mathf.Lerp(initialPosition, targetPosition, normalizedTime);

            transform.position = new Vector3(
                 slideHeight,
                transform.position.y,
                transform.position.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure to set the exact target position to avoid any inaccuracies
        transform.position = new Vector3(
            targetPosition,
            transform.position.y,
            transform.position.z
        );

    }
    void MoveRight()
    {
        _anim.SetTrigger("rightTurn");
        if (transform.position.x > GameManager.maxRightSide)
        {
            StartCoroutine(MoveRightCoroutine(.15f));
        }
    }
    IEnumerator MoveRightCoroutine(float slideDuration)
    {

        float initialPosition = transform.position.x;
        float targetPosition = 0;

        if (transform.position.x > -2.5f && transform.position.x <= 0)
        {
            targetPosition = -2.5f;
        }

        if (transform.position.x > 0f && transform.position.x <= 2.5f)
        {
            targetPosition = 0;
        }

        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            float normalizedTime = elapsedTime / slideDuration;
            float slideHeight = Mathf.Lerp(initialPosition, targetPosition, normalizedTime);

            transform.position = new Vector3(
                 slideHeight,
                transform.position.y,
                transform.position.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure to set the exact target position to avoid any inaccuracies
        transform.position = new Vector3(
            targetPosition,
            transform.position.y,
            transform.position.z
        );

    }

    void Jump()
    {  
        if (!_inAir)
        {
            GetComponent<AudioSource>().enabled = false;
            _inAir = true;
            StopAllCoroutines();
            StartCoroutine(JumpCoroutine());
            _anim.SetBool("isJumping", true);
            _anim.SetBool("isSliding", false); //  this is done because the animation after jumping doesnt go back to sliding
            GetComponent<CapsuleCollider>().height = _capsulColliderHeightAtStart;
            AudioPlayer.audioPlayerSingle.PlayAudioOnce(GameManager.gameManagerSingleton.audioData.Jump, transform, AudioSettings.audioSettings.SoundVolume);
        }
    }

    IEnumerator JumpCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _jumpDuration)
        {
            float normalizedTime = elapsedTime / _jumpDuration;
            float jumpHeight = Mathf.Lerp(_jumpSpeed, 0f, normalizedTime);

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + jumpHeight * Time.deltaTime,
                transform.position.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _rb.useGravity = true;

    }

    
    void SlideDown()
    {
        if (!_inAir)
        {
            return;
        }
        
        StopAllCoroutines();
        StartCoroutine(SlideDownCoroutine(.2f));
        _anim.SetBool("isJumping", false);
    }

    void SlideUnder()
    {
        if (_inAir)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(DecreaseColliderSizeCoroutine(.5f));
    }

    IEnumerator SlideDownCoroutine(float slideDuration)
    {
        float initialPosition = transform.position.y;
        float targetPosition = _heightOnGround; // The ground level or minimum height
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

        _rb.useGravity = false;
        _inAir = false;
        // Make sure to set the exact target position to avoid any inaccuracies
        transform.position = new Vector3(
            transform.position.x,
            targetPosition,
            transform.position.z
        );
    }

    IEnumerator DecreaseColliderSizeCoroutine(float slideDuration)
    {
        _anim.SetBool("isSliding", true);
        // Store the initial collider size
        float initialColliderSize = _capsulColliderHeightAtStart;

        float targetHeight = initialColliderSize/ 2.5f;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            float normalizedTime = elapsedTime / slideDuration;
            float newHeight = Mathf.Lerp(initialColliderSize, targetHeight, normalizedTime);

            // Update the collider size
            GetComponent<CapsuleCollider>().height = newHeight;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure to set the exact target size to avoid any inaccuracies
        GetComponent<CapsuleCollider>().height = targetHeight;

        // Start the coroutine to increase the collider size back to normal
        StartCoroutine(IncreaseColliderSizeCoroutine(slideDuration));
    }
    IEnumerator IncreaseColliderSizeCoroutine(float slideDuration)
    {
        yield return new WaitForSeconds(.6f);

        _anim.SetBool("isSliding", false);

        // Store the current and target collider sizes
        float targetColliderSize = GetComponent<CapsuleCollider>().height*2.5f; // You need to set this variable to the original collider size in the Start or Awake method.

        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            float newHeight = targetColliderSize;

            // Update the collider size
            GetComponent<CapsuleCollider>().height = newHeight;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure to set the exact target size to avoid any inaccuracies
        GetComponent<CapsuleCollider>().height = targetColliderSize;
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
