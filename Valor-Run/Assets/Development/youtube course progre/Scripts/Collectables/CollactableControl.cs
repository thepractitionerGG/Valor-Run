using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollactableControl : MonoBehaviour
{
    public static int coinCount;
    public GameObject coinCountDispaly;
    // Update is called once per frame
    void Update()
    {
        coinCountDispaly.GetComponent<TMP_Text>().text = coinCount.ToString();
    }
}
