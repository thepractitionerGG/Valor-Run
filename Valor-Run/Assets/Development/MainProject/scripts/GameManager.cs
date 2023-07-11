using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public Text scoreTextbox = null;
    public int score;
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
