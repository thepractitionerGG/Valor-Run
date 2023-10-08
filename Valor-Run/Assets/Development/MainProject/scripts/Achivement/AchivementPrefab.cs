using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AchivementHandler;

public class AchivementPrefab : MonoBehaviour
{
    public int target;
    public string info;
    public bool isCoinAchivement;

    public Texture2D IconImage;// or sprit

    public AchivementState achivementState;
    /// <summary>
    /// SettingupPrefab
    /// </summary>
    /// 

    [SerializeField] GameObject UI;
    
    // Start is called before the first frame update
    void OnEnable() // on enable
    {
      GameObject uiTempObject =  Instantiate(UI, Vector3.zero, Quaternion.identity,this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
