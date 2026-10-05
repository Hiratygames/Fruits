using System;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID { get; private set; }

    Rigidbody2D rb;
    Collider2D ownCollider;
    ItemMerger merger;
    private bool isDestroyed = false;

    private bool isFirstCollisionEnd = false;
    public event Action<Item> OnFirstCollision;
    public event Action OnInsideGameOverRange;
	public event Action<Item> OnAddEnterItemList;

	private List<Item> enterItemList = new List<Item>();

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		ownCollider = GetComponent<Collider2D>();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isFirstCollisionEnd)
		{
            OnInsideGameOverRange?.Invoke();
            SetEnable(false);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isFirstCollisionEnd == false)
		{
            // �ꂩItem�ɓ��������Ƃ��ɒʒm���΂�
            if (collision.gameObject.layer == LayerMask.NameToLayer("GlassBottom") ||
                collision.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                isFirstCollisionEnd = true;
                OnFirstCollision?.Invoke(this);
            }
		}
		if (collision.gameObject.TryGetComponent<Item>(out Item item))
        {
			enterItemList.Add(item);
			OnAddEnterItemList?.Invoke(this);

			if (item.isDestroyed == false && item.ID == ID)
            {
                isDestroyed = true;
                item.isDestroyed = true;
				merger.AddMergeCombination(this, item);
			}
        }
	}
    public void Offered()
    {
        if (isDestroyed) return;

        isDestroyed = true;
        Destroy(gameObject);
    }
    public bool IsEnterItems(int[] IDs, ref List<Item> items)
    {
        List<int> remainIDs = new List<int>(IDs);
        foreach (Item item in enterItemList)
        {
            for (int i = 0; i < remainIDs.Count; i++)
            {
                if (item.ID == remainIDs[i])
                {
                    items.Add(item);
                    remainIDs.RemoveAt(i);
                    break;
                }
            }
            if (remainIDs.Count == 0)
            {
                break;
            }
        }
        return remainIDs.Count == 0;
    }
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent<Item>(out Item item))
		{
            if (enterItemList.Contains(item))
            {
                enterItemList.Remove(item);
            }
		}
	}
	public void SetEnable(bool enabled)
    {
		ownCollider.enabled = enabled;
        if (enabled)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

		}
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0.0f;
        }
    }
    public void SetID(int ID)
    {
        this.ID = ID;
    }
    public void SetItemMerger(ItemMerger merger)
    {
        this.merger = merger;
	}
}
