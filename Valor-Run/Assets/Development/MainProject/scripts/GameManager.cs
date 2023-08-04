using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerSingleton;

    public GameObject StartingPlatform;
    public Text coinCollected;
    public Text distanceScore;
    public Text WingsCollected;
    public int coins;
    public int distance;
    public int wings;
    private float distanceInFloat;

    public static float maxLeftSide = 2.4f;
    public static float maxRightSide = -2.4f;

    public GameObject _inGameUi;
    public GameObject _menuUI;
    public GameObject _retryUI;

    public Button StartGameButton;
    public Button Retry;

    public bool touchDisabled;
    public enum GameState
    {
        InMenu,
        Running,
        Dead// try not use it
    }

    private GameState gameState;

    private void Start()
    {
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
        PlayerController.playerController.StartRunning();
        SetGameState(GameManager.GameState.Running);
    }

    public void UpdateCoins(int s)
    {
        if (gameState != GameManager.GameState.Running)
            return;

        coins += s;
        if (coinCollected != null)
        {
            coinCollected.text = "Coins " + coins;
        }
    }

    public void UpdateWings(int s)
    {
        if (gameState != GameManager.GameState.Running)
            return;

        wings += s;
        if (WingsCollected != null)
        {
            WingsCollected.text = "Wings " + wings;
        }
    }

    public void ResetScene()
    {
        //_menuUI.SetActive(true);
        //_retryUI.SetActive(false);
        //PlayerController.playerController.ResetAnimatorToIdleState();
        //Pools.singleton.ResetPool();
        //Pools.singleton.AddPlatformsToPooledItems();
        //GenrateWorld.RunDummy();
        //StartingPlatform.SetActive(true);
        //StartingPlatform.transform.position = startingPlatformInitPos;
         
        // remove all the extra functions above; they were created for reset'

        SceneManager.LoadSceneAsync("SccrollingWorld");
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
        if (gameState == GameState.Running)
        {
            distanceInFloat += 2 * Time.deltaTime;
        }
    }

    private void  LateUpdate()
    {
        if (gameState == GameState.Running)
        { 
            distance = (int)distanceInFloat;
            distanceScore.text = "Score: "+ Mathf.Abs(distance).ToString();
        }
    }

    public void QuitGame()
    {
        /// add some code here #
        Application.Quit();
    }
}
