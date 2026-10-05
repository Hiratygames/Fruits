using UnityEngine;
using UnityEngine.UI;

public class DigitDisplay : MonoBehaviour
{
	[SerializeField]
    Image image;

	public void SetSprite(Sprite sprite)
	{
		image.sprite = sprite;
		gameObject.SetActive(true);
	}
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	public void SetColor(Color color)
	{
		image.color = color;
	}
}
