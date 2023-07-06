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
        
        //for(int i = 0; i < 20; i++)
        //{
        //    GameObject p = Pools.singleton.GetRandom();
        //    if (p == null)
        //        return;

        //    p.SetActive(true);
        //    p.transform.position = dummyTraveller.transform.position;
        //    p.transform.rotation = dummyTraveller.transform.rotation;
        //    if (p.tag == "stairsUp")
        //    {
        //        dummyTraveller.transform.Translate(0,5,0);
        //    }

        //    else if (p.tag == "stairsDown")
        //    {
        //        dummyTraveller.transform.Translate(0, -5, 0);
        //        p.transform.Rotate(new Vector3(0, 180, 0));
        //        p.transform.position = dummyTraveller.transform.position;
        //    }

        //    else if (p.tag == "platformTSection")
        //    {
        //        if(Random.Range(0,2)==0)
        //            dummyTraveller.transform.Rotate(new Vector3(0, 90, 0));
        //        else
        //            dummyTraveller.transform.Rotate(new Vector3(0, -90, 0));


        //        dummyTraveller.transform.Translate(Vector3.forward * -10);
        //    }

        //    dummyTraveller.transform.Translate(Vector3.forward * -10);
        //}
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
                dummyTraveller.transform.position = lastPlatForm.transform.position + PlayerController.player.transform.forward * 10;


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
