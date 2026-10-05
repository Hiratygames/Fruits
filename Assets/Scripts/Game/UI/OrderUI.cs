using UnityEngine;
using UnityEngine.UI;
using static OrderData;

public class OrderUI : MonoBehaviour
{
	public const int MaxIngredientsKind = 5;
	[SerializeField] Image imageProduct;
	[SerializeField] Text textName;
    [SerializeField] NumberDisplay dispPrice;
    [SerializeField] TimerGaugeUI timerGauge;
    [SerializeField] IngredientUI[] ingredientsUI = new IngredientUI[MaxIngredientsKind];

    public void Set(OrderData data, float t)
    {
        imageProduct.sprite = data.sprite;
        textName.text = data.productName;
        dispPrice.Set(data.price);
        for (int i = 0; i < data.ingredients.Count; i++)
        {
            Ingredient ingredientData = data.ingredients[i];
            ItemData itemData = ingredientData.itemData;
            ingredientsUI[i].Set(itemData.sprite, ingredientData.num);
            ingredientsUI[i].SetDisp(true);
		}
		for (int i = data.ingredients.Count; i < MaxIngredientsKind; i++)
        {
            ingredientsUI[i].SetDisp(false);
		}
        timerGauge.Reset();
	}
    public void UpdateEnoughIngredient(OrderData data, bool[] isEnoughArray)
	{
		for (int i = 0; i < data.ingredients.Count; i++)
		{
            ingredientsUI[i].SetEnough(isEnoughArray[i]);
		}
	}
	public void UpdateTime(float t)
	{
		timerGauge.UpdateTime(t);
	}
}
