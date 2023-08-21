using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerSingleton;

    public GameObject StartingPlatform;
    public Text coinCollected;
    public Text distanceScore;
    public Text WingsCollected;

    [SerializeField] Text coinsCollectedRT;
    [SerializeField] Text distanceScoreRT;
    [SerializeField] Text WingsCollectedRT;

    public int coins;
    public int distance;
    public int wings;
    private float distanceInFloat;

    public static float maxLeftSide = 2f;
    public static float maxRightSide = -2f;

    public GameObject _inGameUi;
    public GameObject _menuUI;
    public GameObject _retryUI;

    public Button StartGameButton;
    public Button Retry;

    public bool touchDisabled;

    public AudioData audioData;
    public VFXData vfxData;

    public float timeScaleValue;

    public TMP_Text _timerText;
    public GameObject _pauseScreen;
    // cache for better performance
    AudioSource _mainMenuSource;
    AudioSource _gameMenuSorce;

    public enum GameState
    {
        InMenu,
        Running,
        Dead// try not use it
    }

    private GameState gameState;

    private void Start()
    {
        timeScaleValue = 1f;

        _mainMenuSource = _menuUI.GetComponent<AudioSource>();
        _gameMenuSorce = _inGameUi.GetComponent<AudioSource>();

        SetGameState(GameManager.GameState.InMenu);

        StartGameButton.onClick.AddListener(StartGame);
        Retry.onClick.AddListener(ResetScene);
    }

    public void SetGameState(GameState newState)
    {
        gameState = newState;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    private void Awake()
    {
        GameObject[] gameManager = GameObject.FindGameObjectsWithTag("GameManager");

        if (gameManager.Length > 1)
        {
            Destroy(gameObject);
        }
        coins = 0;
       
        gameManagerSingleton = this;
    }

    public void StartGame()
    {
        DisableTouch();
        _menuUI.SetActive(false);
        _inGameUi.SetActive(true);
        PlayerController._playerController.StartRunning();
        SetGameState(GameManager.GameState.Running);
    }

    public void UpdateCoins(int s)
    {
        if (gameState != GameManager.GameState.Running)
            return;

        coins += s;
        if (coinCollected != null)
        {
            coinCollected.text = coins.ToString();
        }
    }

    public void UpdateWings(int s)
    {
        if (gameState != GameManager.GameState.Running)
            return;

        wings += s;

        if (WingsCollected != null)
        {
            WingsCollected.text = wings.ToString();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    
    void DisableTouch()
    {
        touchDisabled = true;
        StartCoroutine(TouchEnable());
    }

    IEnumerator TouchEnable()
    {
        yield return new WaitForSeconds(.7f);
        touchDisabled = false;
    }
    private void Update()
    {
        if (_menuUI.activeInHierarchy)
        {
            _mainMenuSource.volume = AudioSettings.audioSettings.LobbyMusic;
        }

        if (_inGameUi.activeInHierarchy)
        {
            _gameMenuSorce.volume = AudioSettings.audioSettings.InGameMusic;
        }

        if (gameState == GameState.Running)
        {
            distanceInFloat += 4*Time.deltaTime;
        }
    }

   public void PauseGame()
   {
        Time.timeScale = 0f;
        touchDisabled = true;
        PlayerController._playerController._runningAudio.enabled = false;
   }

    public void ResumeGame()
    {
      //StartCoroutine(ResumeGameCorutine());
        _timerText.gameObject.SetActive(true);
        PlayerController._playerController._runningAudio.enabled = true; /// might create a r=proble as when pause din mid air
        _pauseScreen.SetActive(false);
        Time.timeScale = timeScaleValue;
        touchDisabled = false;
    }

    // use animations here 

    //IEnumerator  ResumeGameCorutine()
    //{
    //    _timerText.gameObject.SetActive(true);
            
    //    _pauseScreen.SetActive(false);
    //    Time.timeScale = timeScaleValue;
    //    touchDisabled = false;
    //}

    public void UpdateRetryScreen()
    {
        coinsCollectedRT.text = coins.ToString();
        distanceScoreRT.text = distance.ToString();
        WingsCollectedRT.text = wings.ToString();
    }
    private void  LateUpdate()
    {
        if (gameState == GameState.Running)
        { 
            distance = (int)distanceInFloat;
            distanceScore.text = Mathf.Abs(distance).ToString();
        }
    }

    public void QuitGame()
    {
        /// add some code here #
        Application.Quit();
    }
}
