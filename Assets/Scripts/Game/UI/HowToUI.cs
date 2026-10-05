using UnityEngine;

public class HowToUI : MonoBehaviour
{
    [SerializeField]
    private GameObject objHowTo_PC;
	[SerializeField]
	private GameObject objHowTo_Switch;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
#if UNITY_SWITCH && !UNITY_EDITOR
        objHowTo_PC.SetActive(false);
		objHowTo_Switch.SetActive(true);
#else
		objHowTo_PC.SetActive(true);
		objHowTo_Switch.SetActive(false);
#endif

	}
}
