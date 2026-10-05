using System;
using UnityEngine;
using static FameManager;

public class FameManager : MonoBehaviour
{
	public enum Rank
	{
		Lower,
		Middle,
		Upper,
		Top,

		Max
	};
	[Serializable]
	public struct FameData
	{
		public Sprite spriteRank;
		public int rankuppoint;
		public float addTime;
	};
	[SerializeField]
	FameData[] fameDatas = new FameData[(int)Rank.Max];
	[SerializeField]
	FameUI fameUI;
	[SerializeField]
	private RankupUI rankupUI;
	[SerializeField]
	private TimeManager timeManager;
	private AudioSource audioSource;
	public event Action<Rank> OnRankup;

	private Rank rank = Rank.Lower;
	private int currentFamePoint = 0;

	public Rank CurrentRank { get { return rank; } }

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	private void Start()
	{
		rankupUI.Hide();
	}
	private Rank Rankup()
	{
		rank++;
		if (rank >= Rank.Max) { rank = Rank.Max - 1; }
		rankupUI.Show(fameDatas[(int)CurrentRank].spriteRank);
		timeManager.AddTime(fameDatas[(int)CurrentRank].addTime);

        audioSource.Play();
		return rank;
	}
	public void AddPoint(int point)
	{
		currentFamePoint += point;
		if (currentFamePoint >= fameDatas[(int)CurrentRank].rankuppoint)
		{
			currentFamePoint -= fameDatas[(int)CurrentRank].rankuppoint;
			Rankup();
			fameUI.Apply(fameDatas[(int)CurrentRank].spriteRank);
			OnRankup?.Invoke(CurrentRank);
		}
		float f = (float)currentFamePoint / (float)fameDatas[(int)CurrentRank].rankuppoint;
		fameUI.Apply(f);
	}
}
