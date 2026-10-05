using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrderDataList", menuName = "Order/List")]
public class OrderDataList : ScriptableObject
{
	public List<OrderData> list;
	public OrderData this[int ID] { get => list[ID]; }
	public int Num { get { return list.Count; } }
}
