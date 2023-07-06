using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinFX; // this will be an auido file for coin collect
    private void OnTriggerEnter(Collider other)
    {
        //coinFX.Play();
        CollactableControl.coinCount += 1;
        gameObject.SetActive(false);
       
    }
}
