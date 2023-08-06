using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour
{
    public Text coinCountText;
    public Text wingsCountText;
    public Text highScoreText;
    public void SavePlayerData(int scoreCountLastGame, int coinCount, int wingsCount)
    {
        int wingsCountAfterAddition = PlayerPrefs.GetInt("WingsCount", 0) + wingsCount;
        int CoinsCountAfterAddition = PlayerPrefs.GetInt("WingsCount", 0) + wingsCount;

        PlayerPrefs.SetInt("CoinCount", CoinsCountAfterAddition);
        PlayerPrefs.SetInt("WingsCount", wingsCountAfterAddition);

        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (scoreCountLastGame > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", scoreCountLastGame);
        }

        PlayerPrefs.Save();
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CoinCount"))
        {
            PlayerPrefs.SetInt("CoinCount", 0);
            PlayerPrefs.SetInt("WingsCount", 0);
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.Save();
        }
    }
    private void Start()
    {
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        int wingsCount = PlayerPrefs.GetInt("WingsCount", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        coinCountText.text =  coinCount.ToString();
        wingsCountText.text = wingsCount.ToString();
        highScoreText.text =  highScore.ToString();
    }
}