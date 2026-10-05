using UnityEngine;

public class ItemDropper : MonoBehaviour
{
	[SerializeField]
	GameManager manager;
	[SerializeField]
	StateManager stateManager;
	[SerializeField]
	FameManager fameManager;
	[SerializeField]
	private NextItemUI nextItemUI;
	[SerializeField]
	private Transform leftLimit;
	[SerializeField]
	private Transform rightLimit;
	[SerializeField]
	private GameObject objLine;
	[SerializeField]
	AudioClip audioDrop;
	Item item = null;
	AudioSource audioSource;
	private int nextItemID;

	public bool IsDropping { get; private set; }
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
        stateManager.OnChangeStateBefore += OnEndState;
        stateManager.OnChangeStateAfter += OnStartState;
    }

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
    }
    public Item Drop()
	{
		if (IsDropping) return null;
		IsDropping = true;
		item.transform.SetParent(null);
		item.SetEnable(true);
		item.OnFirstCollision += RequestNextItem;
		item.OnFirstCollision += manager.ManageItem;
		objLine.SetActive(false);
		audioSource.PlayOneShot(audioDrop);
		return item;
	}
	public void RequestNextItem(Item prevItem)
	{
		item = manager.CreateNextItem();
		if (item != null)
		{
			item.transform.SetParent(transform);
			item.transform.localPosition = Vector2.zero;
			item.SetEnable(false);
			objLine.SetActive(true);
			IsDropping = false;
		}
	}
	public void SetPosition(float f)
	{
		transform.position = Vector2.Lerp(leftLimit.position, rightLimit.position, Mathf.Clamp(f, 0.0f, 1.0f));
	}

    void OnStartState(StateManager.State state)
    {
		switch (state)
		{
			case StateManager.State.Game:
                RequestNextItem(item);
				break;
        }
    }
    void OnEndState(StateManager.State state)
    {
    }
}
