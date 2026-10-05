using System.Collections.Generic;
using UnityEngine;

public class ItemMerger : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] AudioClip audioMerge;
    HashSet<Item> items = new HashSet<Item>();
    Queue<(Item, Item)> queue = new Queue<(Item, Item)>();

    AudioSource audioSource;
	private ItemDataList itemDataList = null;
	public void SetItemDataList(ItemDataList itemDataList)
	{
		this.itemDataList = itemDataList;
	}
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (queue.Count == 0) return;

        var (item1, item2) = queue.Dequeue();
		PerformMerge(item1, item2);
        audioSource.PlayOneShot(audioMerge);
	}

    public void AddMergeCombination(Item item1, Item item2)
    {
        if (items.Contains(item1) || items.Contains(item2))
        {
            // Šù‚É•Ğ•ûi‚à‚µ‚­‚Í—¼•ûj‚ª“o˜^Ï‚İ‚Ì‚½‚ßAˆ—‚µ‚È‚¢
            return;
        }
        items.Add(item1);
        items.Add(item2);
        queue.Enqueue((item1, item2));

	}
	void PerformMerge(Item item1, Item item2)
	{
		Vector2 pos = (item1.transform.position + item2.transform.position) * 0.5f;
		int nextID = item1.ID + 1;

		manager.OnMerged(nextID, pos, item1, item2);
		Destroy(item1.gameObject);
		Destroy(item2.gameObject);

	}
}
