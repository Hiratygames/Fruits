using System;
using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Instantiate(Resources.Load<GameObject>("Prefabs/TransitionCanvas")).GetComponent<TransitionManager>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}
	private static TransitionManager instance;

	[SerializeField] GameObject prefabFirstTransition;
	public event Action OnCloseComplete;
	public event Action OnOpenComplete;
	Transition transition = null;
	public Transition.State CurrentState { get { return transition == null ? Transition.State.None : transition.CurrentState; } }
	public bool AbleTransition { get { return CurrentState == Transition.State.None || CurrentState == Transition.State.Open || CurrentState == Transition.State.Close; } }
	private void Awake()
	{
		transition = Instantiate(prefabFirstTransition, transform).GetComponent<Transition>();
	}
	public void CreateTransition(string name)
	{
		if (transition != null)
		{
			Destroy(transition.gameObject);
		}
		transition = Instantiate(Resources.Load<GameObject>(name), transform).GetComponent<Transition>();
	}
	public void Close()
	{
		transition.OnCloseComplete += () =>
		{
			OnCloseComplete?.Invoke();
			OnCloseComplete = null;
		};
		transition.Close();
	}
	public void Close(float delay)
	{
		Invoke("Close", delay);
	}
	public void Open()
	{
		transition.OnOpenComplete += () =>
		{
			OnOpenComplete?.Invoke();
			OnOpenComplete = null;
		};
		transition.Open();
	}
	public void Open(float delay)
	{
		Invoke("Open", delay);
	}
}
