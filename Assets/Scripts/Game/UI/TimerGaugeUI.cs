using UnityEngine;
using UnityEngine.UI;

public class TimerGaugeUI : MonoBehaviour
{
	private enum State
	{
		Green,
		Yellow,
		Red,

		Max
	}
	[SerializeField]
	private Image imageTime;
	private RectTransform rectTransformTime;
	private State state = State.Green;

	private void Awake()
	{
		rectTransformTime = imageTime.GetComponent<RectTransform>();
		state = State.Green;
		rectTransformTime.localScale = Vector3.one;
		imageTime.color = GetColor(state);
	}
	public void Reset()
	{
		state = State.Green;
		rectTransformTime.localScale = Vector3.one;
		imageTime.color = GetColor(state);
	}
	public void UpdateTime(float t)
	{
		rectTransformTime.localScale = new Vector3(t, 1, 1);
		State newState = CheckState(t);
		if (state != newState)
		{
			state = newState;
			imageTime.color = GetColor(state);
		}
	}
	State CheckState(float t)
	{
		if (t >= 0.5f) return State.Green;
		if (t >= 0.2f) return State.Yellow;
		return State.Red;
	}
	Color GetColor(State state)
	{
		switch (state)
		{
			case State.Green: return Color.green;
			case State.Yellow: return Color.yellow;
			case State.Red: return Color.red;
			default: return Color.green;
		}
	}
}
