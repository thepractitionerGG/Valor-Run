using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool dScheduled = false;
   
    private void OnTriggerExit(Collider other)
    {
        if (PlayerController.isDead) return;

        if (other.gameObject.tag == "Player" && !dScheduled)
        {
            Debug.Log("Jump");
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
