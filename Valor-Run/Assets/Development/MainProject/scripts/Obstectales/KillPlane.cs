using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController._playerController.DeathSequence("KillPlane");
            Camera.main.gameObject.transform.parent=null;
        }
    }
}
