using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool dScheduled = false;
    private void OnCollisionExit(Collision player)
    {
        if (PlayerController.isDead) return;
        if (player.gameObject.tag == "Player"&&!dScheduled)
        {
            Invoke("SetInactive", 4.0f);
            dScheduled = true;

        }
           
    }

    void SetInactive()
    {
        gameObject.SetActive(false);
        dScheduled = false;
    }
}
