using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool dScheduled = false;
    [SerializeField] GameObject[] moveableObjects;
    private void OnTriggerEnter(Collider other)
    {
        foreach(GameObject g in moveableObjects)
        {
            g.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (PlayerController.isDead) return;
        foreach (GameObject g in moveableObjects)// add this condition 
        {
            g.SetActive(false);
        }
        if (other.gameObject.tag == "Player" && !dScheduled)
        {
            Invoke("SetInactive", 2.0f);
            dScheduled = true;
        }
    }
    void SetInactive()
    {
        gameObject.SetActive(false);
        dScheduled = false;
    }
}
