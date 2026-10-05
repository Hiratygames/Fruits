using Unity.VisualScripting;
using UnityEngine;

public class NumberDisplay : MonoBehaviour
{
    [SerializeField] NumberSpriteSet spriteSet;

	[SerializeField] int initNumber = 0;
	[SerializeField] Color color = Color.white;
	[SerializeField] bool isZeroFill = false;

    DigitDisplay[] digitDisplays;
	int number = 0;

	private void Awake()
	{
		number = initNumber;
		digitDisplays = transform.GetComponentsInChildren<DigitDisplay>();
		Set(number);
		SetColor(color);
	}
	public void SetColor(Color color)
	{
		this.color = color;
		foreach (DigitDisplay digit in digitDisplays)
		{
			digit.SetColor(this.color);
		}
	}
	public void Set(int value)
	{
		number = value;
		int maxDigits = digitDisplays.Length;
		int limit = GetLimitValue();

		// value が最大桁数を超えていたら全桁を 9 にする
		if (value > limit)
		{
			Set(limit);
			return;
		}
		// 通常の桁分解処理（除算・剰余算方式）
		for (int i = 0; i < maxDigits; i++)
		{
			int number = value % 10;  // 下位桁を取得
			if (number == 0 && value == 0 && i != 0)
			{
				if (isZeroFill)
				{
					digitDisplays[i].SetSprite(spriteSet.sprites[0]);
				}
				else
				{
					digitDisplays[i].Hide();
				}
			}
			else
			{
				digitDisplays[i].SetSprite(spriteSet.sprites[number]);
			}
			value /= 10;	// 次の桁へ
		}
	}
	int GetLimitValue()
	{
		int maxDigits = digitDisplays.Length;
		int limit = 1;
		for (int i = 0; i < maxDigits; i++)
		{
			limit *= 10;
		}
		return limit - 1;
	}
}
