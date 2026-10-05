using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "OrderData", menuName = "Order/Data")]
public class OrderData : ScriptableObject
{
	[System.Serializable]
	public struct Ingredient
	{
		public ItemData itemData;
		public int num;
	};
	public string productName;
	public Sprite sprite;
	public List<Ingredient> ingredients;
	public int price;
	public int famePoint;
}
