using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenrateWorld : MonoBehaviour
{
  
     static public GameObject dummyTraveller;
     static public GameObject lastPlatForm;
    // Start is called before the first frame update
    void Awake()
    {
        dummyTraveller = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pools.singleton.GetRandom();
        if (p == null) return;

        if (lastPlatForm != null)
        {
            if(lastPlatForm.tag == "platformTSection")
                dummyTraveller.transform.position = lastPlatForm.transform.position + PlayerController.player.transform.forward * 20;

            else
                dummyTraveller.transform.position = lastPlatForm.transform.position + PlayerController.player.transform.forward * 60; // common value for z change in other places also if you add new sections 


            if (lastPlatForm.tag == "stairsUp")
            {
                dummyTraveller.transform.Translate(0, 5, 0);
            }
        }

        lastPlatForm = p;
        p.SetActive(true);
        p.transform.position = dummyTraveller.transform.position;
        p.transform.rotation = dummyTraveller.transform.rotation;

        if (p.tag == "stairsDown")
        {
            dummyTraveller.transform.Translate(0, -5, 0);
            p.transform.Rotate(0, 180, 0);
            p.transform.position = dummyTraveller.transform.position;
        }
    }

    
}
