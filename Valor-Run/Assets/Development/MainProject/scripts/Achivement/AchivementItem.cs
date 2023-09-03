using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static AchivementList;

[System.Serializable]
public class AchivementItem:MonoBehaviour
{
    public int _target;
    public string _achivementText;
    public AchivementState achivementState;
    public RawImage IconImage;
}
