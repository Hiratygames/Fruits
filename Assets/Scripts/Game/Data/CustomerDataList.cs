using UnityEngine;

[CreateAssetMenu(fileName = "CustomDataList", menuName = "Customer/List")]
public class CustomerDataList : ScriptableObject
{
	public CustomerData[] list;
	public CustomerData this[int ID] { get => list[ID]; }

	public int Num { get { return list.Length; } }
}
