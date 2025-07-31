using TMPro;
using UnityEngine;
using YG;

public class PlayerScore : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private TextMeshProUGUI recordText;

    public int CurrentScore { get; private set; }
    public int RecordScore { get; private set; }

    private void Start()
    {
        CurrentScore = PlayerPrefs.GetInt("Count", 0);
        RecordScore = PlayerPrefs.GetInt("Record", 0);
        UpdateUI();
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
        PlayerPrefs.SetInt("Count", CurrentScore);

        if (CurrentScore > RecordScore)
        {
            RecordScore = CurrentScore;
            PlayerPrefs.SetInt("Record", RecordScore);
            YandexGame.NewLeaderboardScores("SaveRecord", RecordScore);
        }

        UpdateUI();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        PlayerPrefs.SetInt("Count", CurrentScore);
        UpdateUI();
    }

    private void UpdateUI()
    {
        counterText.text = CurrentScore.ToString();
        recordText.text = RecordScore.ToString();
    }
}