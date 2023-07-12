using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public Text scoreTextbox = null;
    public int score;

    public static float maxLeftSide = -2.5f;
    public static float maxRightSide = 2.5f;

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



    public void UpdateScore(int s)
    {
        score += s;
        if (scoreTextbox != null)
        {
            scoreTextbox.text = "Score: " + score;
        }
    }
}
