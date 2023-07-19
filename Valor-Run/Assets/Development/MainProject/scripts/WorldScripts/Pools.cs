using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject prfab;
    public int amount;
    public bool expandble;
}

public class Pools : MonoBehaviour
{
    public static Pools singleton;
    public List<PoolItem> items;
    public List<GameObject> pooldItems;
    private void Awake()
    {
        singleton = this;

        pooldItems = new List<GameObject>();

        foreach (PoolItem item in items)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prfab);
                obj.SetActive(false);
                pooldItems.Add(obj);
            }
        }
    }

    public GameObject GetRandom()
    {
        Utils.Shuffle(pooldItems);
        for(int i =0; i < pooldItems.Count; i++)
        {
            if (!pooldItems[i].activeInHierarchy)
            {
                return pooldItems[i];
            }
        }

        foreach(PoolItem item in items)
        {
            if (item.expandble)
            {
                GameObject obj = Instantiate(item.prfab);
                obj.SetActive(false);
                pooldItems.Add(obj);
                return obj;
            }
        }
        return null;
    }
}

public static class Utils
{
    public static System.Random r = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = r.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
