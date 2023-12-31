using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // coin genration problem is occuring here fix here only 
    GameObject[] obstacles;
    [SerializeField] GameObject[] coins;

    GameObject currentCoinSet;

    float[] xSpawnValues = new float[]{ -2.5f, 0, 2.5f};
    private void OnEnable()
    {
        if (currentCoinSet == null)
        {
            int i = Random.Range(0, coins.Length);
            currentCoinSet = coins[i];
        }
       
        int randomvalueForX = Random.Range(0, 3);
        float xValue = xSpawnValues[randomvalueForX];

        float zValue = Random.Range(-30f,26f);

        GameObject coinSet = Instantiate(currentCoinSet, new Vector3(xValue, 1.1f, zValue), Quaternion.identity, gameObject.transform);

        Debug.Log(coinSet.transform.position);
        
    }

    private void OnDisable()
    {
        foreach (Transform childTransform in transform)
        {
            if (gameObject.tag == "CoinRow")
            {
                Destroy(childTransform.gameObject);
            }
            
        }
    }
}
