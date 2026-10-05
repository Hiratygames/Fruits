using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankingManager : MonoBehaviour
{
    public static int Num = 3;
    [SerializeField] RankingUI rankingUI;
    [Serializable]
    class RankingData
	{
		public int[] scores = new int[Num];
		public int Register(int newScore)
		{
			for (int i = 0; i < scores.Length; i++)
			{
				if (newScore > scores[i])
				{
					// i ‚ÌˆÊ’u‚É‘}“ü‚·‚é
					InsertAt(i, newScore);
					return i;
				}
			}
			return -1;
		}
		public void InsertAt(int index, int value)
		{
			// Œã‚ë‚ðˆê‚Â‚¸‚Â‚¸‚ç‚·
			for (int i = scores.Length - 1; i > index; i--)
			{
				scores[i] = scores[i - 1];
			}
			scores[index] = value;
		}
	}
    RankingData rankingData = new RankingData();
	private void Awake()
	{
		LoadScores();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		if (rankingUI != null)
		{
			rankingUI.Apply(rankingData.scores);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	public void RegisterScore(int newScore)
	{
		int rank = rankingData.Register(newScore);
		SaveCurrentScore(newScore, rank);
		SaveScores();
	}
	void SaveCurrentScore(int socre, int rank)
	{
		PlayerPrefs.SetInt("CurrentScore", socre);
		PlayerPrefs.SetInt("ScoreRank", rank);
		PlayerPrefs.Save();
	}
	public int GetCurrentScore()
	{
		return PlayerPrefs.GetInt("CurrentScore", 0);
	}
	public int GetScoreRank()
	{
		return PlayerPrefs.GetInt("ScoreRank", -1);
	}
	void LoadScores()
	{
        string json = PlayerPrefs.GetString("RankingScore");
        if (json.Length > 3)
        {
			rankingData = JsonUtility.FromJson<RankingData>(json);
        } else
        {
            for (int i = 0; i < Num; i++)
            {
				rankingData.scores[i] = 0;
            }
        }
	}
    void SaveScores()
    {
        string json = JsonUtility.ToJson(rankingData);
		PlayerPrefs.SetString("RankingScore", json);
        PlayerPrefs.Save();
    }
}
