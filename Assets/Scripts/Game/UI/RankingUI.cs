using UnityEngine;

public class RankingUI : MonoBehaviour
{
    [SerializeField] NumberDisplay[] numbersScore = new NumberDisplay[RankingManager.Num];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Apply(int[] scores)
	{
        for (int i = 0; i < RankingManager.Num; i++)
        {
            numbersScore[i].Set(scores[i]);
        }
	}
}
