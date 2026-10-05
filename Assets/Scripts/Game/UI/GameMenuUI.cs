using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Button[] buttons;
	[SerializeField] RectTransform rectTransformCursor;
	[SerializeField] AudioClip audioSelect;
	[SerializeField] AudioClip audioDecide;

	[SerializeField] AudioSource audioSource;
	int currentButtonIdx = 0;
	private void OnEnable()
	{
        currentButtonIdx = 0;
		buttons[currentButtonIdx].Select();
		rectTransformCursor.anchoredPosition = buttons[currentButtonIdx].GetComponent<RectTransform>().anchoredPosition;
	}

	// Update is called once per frame
	void Update()
	{
		if (PlayerInput.Instance.Down(PlayerInput.DirectionType.Down))
		{
			if (currentButtonIdx < (buttons.Length - 1))
			{
				currentButtonIdx++;
				buttons[currentButtonIdx].Select();
				rectTransformCursor.anchoredPosition = buttons[currentButtonIdx].GetComponent<RectTransform>().anchoredPosition;
				audioSource.PlayOneShot(audioSelect);
			}
		}
		else if (PlayerInput.Instance.Down(PlayerInput.DirectionType.Up))
		{
			if (currentButtonIdx > 0)
			{
				currentButtonIdx--;
				buttons[currentButtonIdx].Select();
				rectTransformCursor.anchoredPosition = buttons[currentButtonIdx].GetComponent<RectTransform>().anchoredPosition;
				audioSource.PlayOneShot(audioSelect);
			}
		}
		else if (PlayerInput.Instance.Down(PlayerInput.ButtonType.Decide))
		{
			buttons[currentButtonIdx].onClick?.Invoke();
			audioSource.PlayOneShot(audioDecide);
		}
	}
	public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Close()
	{
		Time.timeScale = 1.0f;
		gameObject.SetActive(false);
	}
    public void Retry()
    {
        Close();
		gameManager.Retry();
	}
	public void GotoTitle()
	{
        Close();
        gameManager.GotoTitle();
	}
}
