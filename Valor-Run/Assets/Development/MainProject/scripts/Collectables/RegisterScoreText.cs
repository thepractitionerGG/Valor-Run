using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScoreText : MonoBehaviour
{
    private void Start()
    {
        GameManager.gameManagerSingleton.coinCollected = gameObject.GetComponent<Text>();
    }
}
