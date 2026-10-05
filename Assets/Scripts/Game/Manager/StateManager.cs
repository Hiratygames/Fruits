using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        Ready,
        Game,
        GameOver,
        GameClear,

        Max
    }
    public event Action<State> OnChangeStateBefore;
	public event Action<State> OnChangeStateAfter;

    public State CurrentState { get; private set; } = State.Max;
    public void ChangeState(State next)
    {
        OnChangeStateBefore?.Invoke(CurrentState);
        CurrentState = next;
		OnChangeStateAfter?.Invoke(CurrentState);
	}
}
