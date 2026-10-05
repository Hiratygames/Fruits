using UnityEngine;
using UnityEngine.UI;

public class NextItemUI : MonoBehaviour
{
    [SerializeField] Image image;
	[SerializeField] Text textPrice;

	private ItemDataList itemDataList = null;
	public void SetItemDataList(ItemDataList itemDataList)
	{
		this.itemDataList = itemDataList;
	}
	public void Apply(int ID)
    {
        image.sprite = itemDataList[ID].sprite;
		textPrice.text = itemDataList[ID].price.ToString();

	}
}
