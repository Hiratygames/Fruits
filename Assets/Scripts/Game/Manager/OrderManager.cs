using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField]
    OrderUI orderUI;
    [SerializeField]
    GameObject objNoOrder;

    private OrderData currentOrder;
    public bool IsAbleOffer {  get; private set; } = false;
	public void SetOrder(OrderData orderData, ItemManager itemManager, float t)
	{
		objNoOrder.SetActive(false);
		currentOrder = orderData;
        orderUI.Set(currentOrder, t);
        UpdateEnoughIngredient(itemManager);
	}
    public void NoOrder()
    {
		currentOrder = null;
		objNoOrder.SetActive(true);

	}
    public void UpdateEnoughIngredient(ItemManager itemManager)
	{
		if (currentOrder == null) return;
		bool[] isEnoughArray = new bool[currentOrder.ingredients.Count];
		IsAbleOffer = true;
		for (int i = 0; i < currentOrder.ingredients.Count; i++)
		{
			OrderData.Ingredient ingredient = currentOrder.ingredients[i];
			isEnoughArray[i] = itemManager.IsEnoughIngredient(ingredient.itemData.ID, ingredient.num);
			if (isEnoughArray[i] == false)
			{
				IsAbleOffer = false;
			}
		}
		orderUI.UpdateEnoughIngredient(currentOrder, isEnoughArray);
	}
	public void UpdateTime(float t)
	{
		orderUI.UpdateTime(t);
	}
}
