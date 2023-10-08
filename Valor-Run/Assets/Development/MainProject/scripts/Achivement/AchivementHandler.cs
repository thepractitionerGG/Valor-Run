using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementHandler : MonoBehaviour
{
    // make UI and Test
    AchivementList achivementList;

    private List<AchivementItem> _achivementItmes=new List<AchivementItem>();
    private List<string> _achivementItemUniqueKey = new List<string>(); // for retriving;

    [SerializeField] Transform _coinAchivementListParent;
    [SerializeField] Transform _achivementListParent;

   
    private void Awake()
    {
        achivementList = AchivementList.achivementListSingleton;
        CreateUniqueKeyList();

        if (PlayerPrefs.GetInt("Files Created",0) != 1) 
        {
            CreateAchivementSaveFilesUsingPlayerPreffs();
        }

        else
        {
            RetriveAchivementSaveFiles();
        }
    }

    private void CreateUniqueKeyList()
    {
        for(int i = 0; i < achivementList.achivementItemDetailsCoin.Length; i++)
        {
            _achivementItemUniqueKey.Add(achivementList.achivementItemDetailsCoin[i].achivementText);
        }

        for (int i = 0; i < achivementList.achivementItemDetailsSprint.Length; i++)
        {
            _achivementItemUniqueKey.Add(achivementList.achivementItemDetailsSprint[i].achivementText);
        }

    }

    private void RetriveAchivementSaveFiles()
    {
        Debug.Log("RetriveAchivementSaveFiles");
        int isCoinAchivementThisItem;
        AchivementState achivementState;

        foreach (string itemKey in _achivementItemUniqueKey)
        {
            AchivementPrefab achivementPrefab = new AchivementPrefab();
           
            GameObject gameObject = new GameObject();
            Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            achivementPrefab = gameObject.AddComponent<AchivementPrefab>();

            achivementPrefab.target = PlayerPrefs.GetInt(itemKey + "Target", 0);
            achivementPrefab.info = PlayerPrefs.GetString(itemKey + "Info");

            isCoinAchivementThisItem = PlayerPrefs.GetInt(itemKey + "IsCoinAchivement");
            achivementPrefab.isCoinAchivement = isCoinAchivementThisItem == 1;

            achivementState = (AchivementState) PlayerPrefs.GetInt(itemKey + "AchivementSatate", 0);
            achivementPrefab.achivementState = achivementState;


            string encodedTexture = PlayerPrefs.GetString(itemKey+"TextureData");
            byte[] textureData = Convert.FromBase64String(encodedTexture);
            Texture2D texture = new Texture2D(2, 2); // You might want to set the correct width and height
            texture.LoadImage(textureData); // Load the texture data
            achivementPrefab.IconImage =texture;

            CheckIsCoinAchivement(achivementPrefab);

        }
    }

    private void CreateAchivementSaveFilesUsingPlayerPreffs()
    {
       
        Debug.Log("CreateAchivementSaveFilesUsingPlayerPreffs");
        string prefixForThisItem;
        int isCoinAchivementThisItem; // to store bool in player prefs
        Texture2D textureThisItem;
        int i = 0;
        AchivementState achivementStateThisItem;

        foreach (AchivementItem achivementItem in achivementList.achivementItemDetailsCoin)
        {
            if (i > achivementList.achivementItemDetailsCoin.Length) { break; }

            achivementItem.achivementState = achivementList.achivementItemDetailsCoin[i].achivementState;
            achivementItem.achivementText = achivementList.achivementItemDetailsCoin[i].achivementText;
            achivementItem.coinAchievement = achivementList.achivementItemDetailsCoin[i].coinAchievement;
            achivementItem.achivementIconImage = achivementList.achivementItemDetailsCoin[i].achivementIconImage;
            achivementItem.AchivementTarget = achivementList.achivementItemDetailsCoin[i].AchivementTarget;

            Debug.Log(achivementList.achivementItemDetailsCoin[i].achivementText.ToString());
            _achivementItmes.Add(achivementItem);
            i++;
        }

        int j = 0;

        foreach (AchivementItem achivementItem in achivementList.achivementItemDetailsSprint)
        {


            if (j> achivementList.achivementItemDetailsSprint.Length) { break; }

            achivementItem.achivementState = achivementList.achivementItemDetailsSprint[i].achivementState;
            achivementItem.achivementText = achivementList.achivementItemDetailsSprint[i].achivementText;
            achivementItem.coinAchievement = achivementList.achivementItemDetailsSprint[i].coinAchievement;
            achivementItem.achivementIconImage = achivementList.achivementItemDetailsSprint[i].achivementIconImage;
            achivementItem.AchivementTarget = achivementList.achivementItemDetailsSprint[i].AchivementTarget;

            Debug.Log(achivementList.achivementItemDetailsSprint[i].achivementText.ToString());
            _achivementItmes.Add(achivementItem);
            j++;
        }

        foreach (AchivementItem achivementItem in _achivementItmes)
        {

            AchivementPrefab achivementPrefab = new AchivementPrefab();

            GameObject gameObject = new GameObject();
            Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            achivementPrefab = gameObject.AddComponent<AchivementPrefab>();
            achivementPrefab.achivementState = achivementItem.achivementState;
            achivementPrefab.IconImage = achivementItem.achivementIconImage;
            achivementPrefab.info = achivementItem.achivementText;
            achivementPrefab.target = achivementItem.AchivementTarget;
            achivementPrefab.isCoinAchivement = achivementItem.coinAchievement;

            prefixForThisItem = achivementPrefab.info;
            CheckIsCoinAchivement(achivementPrefab);

            PlayerPrefs.SetInt(prefixForThisItem + "Target", achivementPrefab.target);
            PlayerPrefs.SetString(prefixForThisItem + "Info", achivementPrefab.info);

            isCoinAchivementThisItem = achivementPrefab.isCoinAchivement ? 1 : 0;
            PlayerPrefs.SetInt(prefixForThisItem + "IsCoinAchivement", isCoinAchivementThisItem);

            //textureThisItem = achivementPrefab.IconImage;
            //byte[] textureData = textureThisItem.EncodeToPNG();
            //string encodedTexture = Convert.ToBase64String(textureData);
            //PlayerPrefs.SetString(prefixForThisItem + "TextureData", encodedTexture);

            achivementStateThisItem = achivementPrefab.achivementState;
            int stateValue = (int)achivementStateThisItem;
            PlayerPrefs.SetInt(prefixForThisItem + "AchievementState", stateValue);

        }
        PlayerPrefs.SetInt("Files Created", 1);
        Debug.Log("CreateAchivementSaveFilesUsingPlayerPreffs Finished");
    }

    private void CheckIsCoinAchivement(AchivementPrefab achivementPrefab)
    {
        if (achivementPrefab.isCoinAchivement)
        {
            achivementPrefab.transform.SetParent(_coinAchivementListParent);
        }

        else
        {
            achivementPrefab.transform.SetParent(_achivementListParent);
        }
    }

    public enum AchivementState
    {
        Active,
        Locked,
        Completed
    }
}
