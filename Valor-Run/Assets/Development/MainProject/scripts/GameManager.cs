using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public Text scoreTextbox = null;
    public int score;

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
        score = 0;
        DontDestroyOnLoad(gameObject);
        singleton = this;
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

        score += s;
        if (scoreTextbox != null)
        {
            scoreTextbox.text = "Score: " + score;
        }
    }

    public void ResetScene()
    {
        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().ToString());
    }

    public void QuitGame()
    {
        /// add some code here #
        Application.Quit();
    }
}
