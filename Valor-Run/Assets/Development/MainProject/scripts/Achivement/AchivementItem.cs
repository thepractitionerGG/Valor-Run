using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AchivementItem : MonoBehaviour
{
    [SerializeField] RawImage _firstImage;
    [SerializeField] RawImage _scecondImage;
    [SerializeField] TMP_Text _achivementText;
    [SerializeField] Slider _progressbar;

    [SerializeField] int _target;

    public enum AchivementSatate
    {
       Active,
       Locked,
       Completed
    }


}
