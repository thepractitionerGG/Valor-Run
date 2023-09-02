using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static AchivementList;

[System.Serializable]
public class AchivementItem
{
    //[SerializeField] Slider _progressbar;
    //[SerializeField] RawImage _locked;
    //[SerializeField] RawImage _completed;
    //[SerializeField] RawImage _firstImage;
    //[SerializeField] RawImage _scecondImage;

    public int _target;
    public string _achivementText;
    public AchivementState achivementState;
    public RawImage IconImage;
}
