using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementList : MonoBehaviour
{
   public AchivementItem[] achivementItemDetailsCoin;
   public AchivementItem[] achivementItemDetailsSprint;
   [SerializeField] GameObject _itemPrefab;
   public void ListPopulator()
   {

   }
    public enum AchivementState
    {
        Active,
        Locked,
        Completed
    }
}
