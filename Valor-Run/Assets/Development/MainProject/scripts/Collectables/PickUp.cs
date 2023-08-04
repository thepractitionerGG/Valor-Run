using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int rotateSpeed = 100;
    MeshRenderer mrs;
    private void Start()
    {
        mrs = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (GameManager.gameManagerSingleton.GetGameState() != GameManager.GameState.Running)
            return;

       // if (PlayerController.isDead) return;
        transform.Rotate(0, rotateSpeed*Time.deltaTime, 0, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.name == "Wings")
            {
                GameManager.gameManagerSingleton.UpdateWings(1);
            }

            else
            {
                GameManager.gameManagerSingleton.UpdateCoins(1);
               
            }

            mrs.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (mrs != null)
        {
            mrs.enabled = true;
        }
    }

}
