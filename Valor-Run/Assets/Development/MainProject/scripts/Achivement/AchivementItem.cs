using UnityEngine;
using UnityEditor;
using static AchivementHandler;

[CreateAssetMenu(fileName = "AchivementItem", menuName = "SOs/AchivementItemSO")]
public class AchivementItem : ScriptableObject
{
    public int AchivementTarget;
    public string achivementText;
    public bool coinAchievement;
    public Texture2D achivementIconImage;
    public AchivementState achivementState;
}

