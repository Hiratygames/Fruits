using UnityEngine;

public class PressButtonUI : MonoBehaviour
{
	[SerializeField]
	private GameObject obj_PC;
	[SerializeField]
	private GameObject obj_Switch;

	Animator animator;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
#if UNITY_SWITCH && !UNITY_EDITOR
        obj_PC.SetActive(false);
		obj_Switch.SetActive(true);
		animator = obj_Switch.GetComponent<Animator>();
#else
		obj_PC.SetActive(true);
		obj_Switch.SetActive(false);
		animator = obj_PC.GetComponent<Animator>();
#endif
	}
	public void Press()
	{
		animator.SetTrigger("Press");
	}
	public void Hide()
	{
		obj_PC.SetActive(false);
		obj_Switch.SetActive(false);
	}
	public void Show()
	{
#if UNITY_SWITCH && !UNITY_EDITOR
		obj_Switch.SetActive(true);
#else
		obj_PC.SetActive(true);
#endif
	}
}
