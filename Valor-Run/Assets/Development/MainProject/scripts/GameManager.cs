using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerSingleton;
    public Text coinCollected = null;
    public Text distanceScore = null;
    public int coins;
    public int distance;
    private float distanceInFloat;

    public static float maxLeftSide = 2.4f;
    public static float maxRightSide = -2.4f;

    public GameObject _inGameUi;
    public GameObject _menuUI;
    public GameObject _retryUI;
    public enum GameState
    {
        InMenu,
        Running,
        Dead// try not use it
    }

    private GameState gameState;


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
        DontDestroyOnLoad(gameObject);
        gameManagerSingleton = this;
    }
    private void Start()
    {
        SetGameState(GameManager.GameState.InMenu);
    }
    public void StartGame()
    {
        _menuUI.SetActive(false);
        _inGameUi.SetActive(true);
        SetGameState(GameManager.GameState.Running);
    }

    public void UpdateScore(int s)
    {
        if (gameState != GameManager.GameState.Running)
            return;

        coins += s;
        if (coinCollected != null)
        {
            coinCollected.text = "Coins " + coins;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadSceneAsync("SccrollingWorld");
    }

    private void Update()
    {
        if (gameState == GameState.Running)
        {
            distanceInFloat += 1 * Time.deltaTime;
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
