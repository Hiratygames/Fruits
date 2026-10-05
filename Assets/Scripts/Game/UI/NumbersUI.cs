using UnityEngine;
using UnityEngine.UI;

public class NumbersUI : MonoBehaviour
{
	[SerializeField] Sprite[] spritesNumber = new Sprite[10];
    [SerializeField] int initNumber = 0;
    [SerializeField] Color color = Color.white;
    [SerializeField] bool isZeroFill = false;

	[SerializeField] Image[] imagesNumber;
    public int DigitNum { get { return imagesNumber.Length; } }

	private void Awake()
	{
        imagesNumber = transform.GetComponentsInChildren<Image>();
        Set(initNumber);
		SetColor(color);
	}

	public void SetColor(Color color)
	{
		this.color = color;
		foreach (Image image in imagesNumber)
		{
			image.color = color;
		}
	}
	public void Set(int value)
	{
		int maxDigits = imagesNumber.Length;

		// value が最大桁数を超えていたら全桁を 9 にする
		if (value >= Mathf.Pow(10, maxDigits))
		{
			SetMaxValue();
			return;
		}
		// 通常の桁分解処理（除算・剰余算方式）
		for (int i = 0; i < maxDigits; i++)
		{
			int number = value % 10;  // 下位桁を取得
			if (number == 0 && value == 0)
			{
				SetDigitHide(i);
			} else
			{
				SetDigitValue(i, number);
			}
			value /= 10;			// 次の桁へ
		}
	}
	// カンスト値を設定する
	void SetMaxValue()
	{
		for (int i = 0; i < imagesNumber.Length; i++)
		{
			imagesNumber[i].sprite = spritesNumber[9];
			imagesNumber[i].gameObject.SetActive(true);
		}
	}
	// 指定桁の数字を隠す
	void SetDigitHide(int digit)
	{
		if (isZeroFill)
		{
			imagesNumber[digit].sprite = spritesNumber[0];
			imagesNumber[digit].gameObject.SetActive(true);
		}
		else
		{
			imagesNumber[digit].gameObject.SetActive(false);
		}
	}
	// 指定桁の数字を設定する
	void SetDigitValue(int digit, int number)
	{
		imagesNumber[digit].sprite = spritesNumber[number];
		imagesNumber[digit].gameObject.SetActive(true);
	}
}
