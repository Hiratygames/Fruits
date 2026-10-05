using System;
using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
	public enum State
	{
		None,
		Closing,
		Close,
		Opening,
		Open,
	}
	State state = State.None;
	public State CurrentState { get { return state; } }
	Animator animator;
	public event Action OnCloseComplete;
	public event Action OnOpenComplete;
	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
	public void Close()
	{
		state = State.Closing;
		animator.SetTrigger("Close");
		StartCoroutine(CloseCoroutine());
	}
	IEnumerator CloseCoroutine()
	{
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("Close") == false || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			yield return null;
		}
		state = State.Close;
		OnCloseComplete?.Invoke();
		OnCloseComplete = null;
	}
	public void Open()
	{
		state = State.Opening;
		animator.SetTrigger("Open");
		StartCoroutine(OpenCoroutine());
	}
	IEnumerator OpenCoroutine()
	{
		while (animator.GetCurrentAnimatorStateInfo(0).IsName("Open") == false || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			yield return null;
		}
		state = State.Open;
		OnOpenComplete?.Invoke();
		OnOpenComplete = null;
	}
}
