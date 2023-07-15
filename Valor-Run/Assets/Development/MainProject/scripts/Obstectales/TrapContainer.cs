using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapContainer : MonoBehaviour
{
    List<GameObject> obstacles = new List<GameObject>();

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            obstacles.Add(transform.GetChild(0).gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(StartTraps());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.transform.position.z > 25) 
        {
            ResetTraps();
        }
    }

    IEnumerator StartTraps()
    {
        foreach (GameObject g in obstacles)
        {
            yield return new WaitForSeconds(.2f);
            g.SetActive(true);
            g.GetComponent<MovableObject>().SetSpeedRequired();
        }
    }

    private void ResetTraps()
    {
        foreach (GameObject g in obstacles)
        {
            g.GetComponent<MovableObject>().SetSpeedZero();
            g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, -8);
            g.SetActive(false);
        }
    }
}
