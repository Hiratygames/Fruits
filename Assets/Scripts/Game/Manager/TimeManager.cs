using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] StateManager stateManager;
    [SerializeField] TimeUI timeUI;
    [SerializeField]
    private float time = 300;
    private int time_int = 300;
    bool isTimeUp = false;
    public bool IsTimeUp { get { return isTimeUp; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time_int = (int)time;
        timeUI.Set(time_int);

	}

    // Update is called once per frame
    void Update()
    {
        if (stateManager.CurrentState != StateManager.State.Game) return;
        if (isTimeUp) return;
        time -= Time.deltaTime;
        if (time_int > (int)time)
        {
            time_int = (int)time;
            if (time_int <= 0)
            {
                isTimeUp = true;
                time_int = 0;
                stateManager.ChangeState(StateManager.State.GameClear);
            }
			timeUI.Set(time_int);
		}
    }
    public void AddTime(float addTime)
    {
        time += addTime;
        time_int = (int)time;
        timeUI.Set(time_int);
        timeUI.ShowAddTime((int)addTime);
    }
}
