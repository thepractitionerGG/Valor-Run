using UnityEngine;
using UnityEditor;
using static AchivementList;

[CreateAssetMenu(fileName = "AchivementItem", menuName = "SOs/AchivementItemSO")]
public class AchivementItem : ScriptableObject
{
    public int target;
    public string achivementText;
    public bool coinAchievement;
    public Texture2D iconImage;
    public AchivementState achivementState;
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(AchivementItem))]
//public class AchivementItemSOEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        AchivementItem achivementItemSO = (AchivementItem)target;

//        // Check if the name of the SO ends with a number (e.g., "_1", "_2")
//        if (!achivementItemSO.name.EndsWith("_"))
//        {
//            string originalAssetPath = AssetDatabase.GetAssetPath(achivementItemSO.GetInstanceID());
//            string directory = System.IO.Path.GetDirectoryName(originalAssetPath);
//            string baseName = achivementItemSO.name;

//            // Find the next available suffix
//            int suffix = 1;
//            while (System.IO.File.Exists(System.IO.Path.Combine(directory, $"{baseName}_{suffix}.asset")))
//            {
//                suffix++;
//            }

//            // Rename the SO with the new suffix
//            string newAssetPath = $"{directory}/{baseName}_{suffix}.asset";
//            System.IO.File.Move(originalAssetPath, newAssetPath);
//            AssetDatabase.Refresh();
//        }

//        // Draw the default inspector GUI
//        DrawDefaultInspector();
//    }
//}
//#endif