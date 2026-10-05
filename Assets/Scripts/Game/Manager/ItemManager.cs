using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
	[SerializeField] ItemDataList itemList;
	[SerializeField] StockUI stockUI;
	private List<Item> items = new List<Item>();
    public List<Item> Items { get { return items; } }
	List<int> stocks = new List<int>();

	private void Awake()
	{
		for (int i = 0; i < itemList.Num; i++)
		{
			stocks.Add(0);
		}
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		for (int i = 0; i < itemList.Num; i++)
		{
			stockUI.Set(i, 0);
		}
	}
	public void AddItem(Item item)
    {
        items.Add(item);
        AddStock(item.ID, 1);

	}
    public void RemoveItem(Item item)
    {
		UseStock(item.ID, 1);
        items.Remove(item);
        Destroy(item.gameObject);
	}
	public Item FindItem(int itemID)
	{
        foreach (Item item in items)
        {
            if (item.ID == itemID) return item;
        }
        return null;
	}
	public bool IsEnoughIngredient(int itemID, int num)
	{
		return stocks[itemID] >= num;
	}
	void AddStock(int ID, int num)
	{
		stocks[ID] += num;
		stockUI.Set(ID, stocks[ID]);
	}
	bool UseStock(int ID, int num)
	{
		if (IsEnoughIngredient(ID, num) == false) return false;
		stocks[ID] -= num;
		stockUI.Set(ID, stocks[ID]);
		return true;
	}
}
