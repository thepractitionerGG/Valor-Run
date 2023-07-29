using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectContainer : MonoBehaviour
{
    [SerializeField] GameObject[] moveableObjectContainerArray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit");
            foreach (GameObject g in moveableObjectContainerArray)
            {
                g.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject g in moveableObjectContainerArray)
            {
                g.SetActive(false);
            }
        }
    }
}
