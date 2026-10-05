using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "Customer/Data")]
public class CustomerData : ScriptableObject
{
	public Sprite sprite;
	public OrderData[] orders;
	public int time;
}
