using UnityEngine;
using UnityEngine.UI;

public class RankSystem : MonoBehaviour
{
    public RawImage rankImage;
    public Texture defaultRankImage;
    public Texture[] rankImages; // Copper, Bronze, Silver, Gold, Diamond, Champ

    public int[] rankThresholds;

    private int _playerHighScore;


    private void Start()
    {
        _playerHighScore = PlayerPrefs.GetInt("HighScore", 0);
        // Load the player's high score from your saving system
        // For this example, let's assume the score is loaded into the playerScore variable
        // You can replace this with your actual score loading mechanism

        // Check if the player has a valid score (greater than 0)
        if (_playerHighScore <= 0)
        {
            // Set the default rank image for new users
            rankImage.texture = defaultRankImage;
            return;
        }

        // Determine the player's rank based on the score
        int playerRank = GetPlayerRank(_playerHighScore);

        // Set the corresponding rank image
        if (playerRank >= 0 && playerRank < rankImages.Length)
        {
            rankImage.texture = rankImages[playerRank];
        }
        else
        {
            // If the rank index is out of bounds, set the default rank image
            rankImage.texture = defaultRankImage;
        }
    }

    private int GetPlayerRank(int score)
    {
        for (int i = rankThresholds.Length - 1; i >= 0; i--)
        {
            if (score >= rankThresholds[i])
            {
                return i; // Return the index of the highest rank the player qualifies for
            }
        }
        return -1; // Return -1 if the score is below the lowest threshold
    }
}
