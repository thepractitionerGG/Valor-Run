using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int rotateSpeed = 100;

    private void Update()
    {
        if (PlayerController.isDead) return;
        transform.Rotate(0, rotateSpeed*Time.deltaTime, 0, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.singleton.UpdateScore(1);
            Destroy(gameObject, .1f);
        }
    }

    
}
