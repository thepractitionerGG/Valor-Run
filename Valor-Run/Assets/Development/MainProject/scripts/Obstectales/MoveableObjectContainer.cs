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
            foreach (GameObject g in moveableObjectContainerArray)
            {
                if (g != null)
                {
                    g.SetActive(true);
                }
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject g in moveableObjectContainerArray)
            {
                if (g != null)
                {
                    g.SetActive(false);
                }
               
            }
        }
    }
}
