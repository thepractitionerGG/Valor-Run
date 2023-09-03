using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementList : MonoBehaviour
{
   public  AchivementItem[] achivementItemDetailsCoin;
   public AchivementItem[] achivementItemDetailsSprint;

    private const string CoinAchievementKey = "CoinAchievementIndex";
    private const string SprintAchievementKey = "SprintAchievementIndex";

    private void Start()
    {
        // Check and create PlayerPrefs values if they don't exist
        CheckAndCreatePlayerPrefsValues();
    }

    // Function to check and create PlayerPrefs values with default of zero
    private void CheckAndCreatePlayerPrefsValues()
    {
        if (!PlayerPrefs.HasKey(CoinAchievementKey))
        {
            PlayerPrefs.SetInt(CoinAchievementKey, 0);
        }

        if (!PlayerPrefs.HasKey(SprintAchievementKey))
        {
            PlayerPrefs.SetInt(SprintAchievementKey, 0);
        }
    }

    // Function to update CoinAchievementIndex
    public void UpdateCoinAchievementIndex(int newValue)
    {
        PlayerPrefs.SetInt(CoinAchievementKey, newValue);
        PlayerPrefs.Save(); // Save the PlayerPrefs to persist the changes
    }

    // Function to update SprintAchievementIndex
    public void UpdateSprintAchievementIndex(int newValue)
    {
        PlayerPrefs.SetInt(SprintAchievementKey, newValue);
        PlayerPrefs.Save(); // Save the PlayerPrefs to persist the changes
    }

    // Function to get the value of CoinAchievementIndex
    public int GetCoinAchievementIndex()
    {
        return PlayerPrefs.GetInt(CoinAchievementKey);
    }

    // Function to get the value of SprintAchievementIndex
    public int GetSprintAchievementIndex()
    {
        return PlayerPrefs.GetInt(SprintAchievementKey);
    }
    public enum AchivementState
    {
        Active,
        Locked,
        Completed
    }
}

