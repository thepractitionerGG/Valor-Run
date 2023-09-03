using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementList : MonoBehaviour
{
   public AchivementItem[] achivementItemDetailsCoin;
   public AchivementItem[] achivementItemDetailsSprint;

   [SerializeField] AchivementItem _itemPrefabForCoinAchievement;
   [SerializeField] AchivementItem _itemPrefabForSprintAchivement;

    public Transform _listContentParentForCoin;
    public Transform _listContentParentForSprint;
   public void ListPopulatorCoin()
   {
       for(int i = 0; i <= achivementItemDetailsCoin.Length; i++)
       {
            GameObject achievementItemObjecty= GameObject.Instantiate(new GameObject(), _listContentParentForCoin);
            achievementItemObjecty.name = "CoinAchivmentItem " + i;
            achievementItemObjecty.AddComponent<AchivementItem>();

            achievementItemObjecty.GetComponent<AchivementItem>()._achivementText = achivementItemDetailsCoin[i]._achivementText;
            achievementItemObjecty.GetComponent<AchivementItem>()._target = achivementItemDetailsCoin[i]._target;
            achievementItemObjecty.GetComponent<AchivementItem>().IconImage = achivementItemDetailsCoin[i].IconImage;
            achievementItemObjecty.GetComponent<AchivementItem>().achivementState = achivementItemDetailsCoin[i].achivementState;

       }
   }
    public enum AchivementState
    {
        Active,
        Locked,
        Completed
    }
}
