using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
	[SerializeField]
	private ScoreUI scoreUI;

    [SerializeField]
    private int initScore = 5000;
    int score = 0;
    public int Score { get { return score; } }
	private void Awake()
	{
		score = initScore;
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		scoreUI.Apply(Score);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int value)
    {
        score += value;
		scoreUI.Apply(Score);
	}
	public bool SubtractScore(int value)
    {
        if (score < value)
        {
            stateManager.ChangeState(StateManager.State.GameOver);
            return false;
        }
        score -= value;
        scoreUI.Apply(Score);
        return true;
    }
}
