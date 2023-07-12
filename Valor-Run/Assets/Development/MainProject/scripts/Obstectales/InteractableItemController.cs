using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemController : MonoBehaviour
{
   public List<GameObject> obstacles = new List<GameObject>();

   GameObject itemOne, ItemTwo;

   int itemsToBeGenratedCount;

   public void SpawnItem()
   {
     itemsToBeGenratedCount = Random.Range(0, 3);
     SpawnItemSwitch(itemsToBeGenratedCount);
   }

    void SpawnItemSwitch(int i)
    {
        switch (i) 
        {
            case 0:
                break;
                
            case 1:
                break;

            case 2:
                break;
        }
    }

    private void OnDisable()
    {
        
    }
}
