using UnityEngine;

[CreateAssetMenu(fileName ="ItemData")]
public class ItemData : ScriptableObject
{
	public int ID;
	public GameObject prefab;
	public Sprite sprite;
	public int price;
}
