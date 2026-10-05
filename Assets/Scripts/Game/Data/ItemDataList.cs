using System;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDataList")]
public class ItemDataList : ScriptableObject
{
	public ItemData[] list;
	public ItemData this[int ID] { get => list[ID]; }

	public int Num { get { return list.Length; } }
}
