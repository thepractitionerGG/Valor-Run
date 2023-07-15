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
            if (!obstacles.Contains(transform.GetChild(i).gameObject))
            {
                obstacles.Add(transform.GetChild(i).gameObject);
            }
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(StartTraps());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.transform.position.z > 25)
        {
            ResetTraps();
        }
    }

    IEnumerator StartTraps()
    {
        foreach (GameObject g in obstacles)
        {
            Debug.Log(g.name);
            yield return new WaitForSeconds(.01f);
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
