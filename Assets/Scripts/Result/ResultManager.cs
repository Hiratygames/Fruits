using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
	[SerializeField] RankingManager rankingManager;
	[SerializeField] NumberDisplay score;
	[SerializeField] GameObject[] objectsNewIcon;
    [SerializeField] Button[] buttons;
    int current = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        buttons[current].Select();
		score.Set(rankingManager.GetCurrentScore());
		int scoreRank = rankingManager.GetScoreRank();
		for (int i = 0; i < RankingManager.Num; i++)
		{
			objectsNewIcon[i].SetActive((i == scoreRank));
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (TransitionManager.Instance.AbleTransition == false) return;
		if (PlayerInput.Instance.Down(PlayerInput.DirectionType.Down))
        {
            current = (current + 1) % buttons.Length;
			buttons[current].Select();
		}
		if (PlayerInput.Instance.Down(PlayerInput.DirectionType.Up))
		{
			current = (buttons.Length + current - 1) % buttons.Length;
			buttons[current].Select();
		}
		if (PlayerInput.Instance.Down(PlayerInput.ButtonType.Decide))
		{
			buttons[current].onClick?.Invoke();
		}
	}
    public void Retry()
	{
		GotoGame();
	}
	void GotoGame()
	{
		TransitionManager.Instance.OnCloseComplete += () =>
		{
			SceneManager.LoadScene("Game");
			TransitionManager.Instance.Open(0.5f);
		};
		TransitionManager.Instance.Close();
	}
	void GotoTitle()
	{
		TransitionManager.Instance.OnCloseComplete += () =>
		{
			SceneManager.LoadScene("Title");
			TransitionManager.Instance.Open(0.5f);
		};
		TransitionManager.Instance.Close();
	}
}
