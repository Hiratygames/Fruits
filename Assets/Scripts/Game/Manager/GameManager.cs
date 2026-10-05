using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private ItemDataList itemDataList;
	[SerializeField]
	private ItemDataList[] dropItemLists = new ItemDataList[(int)FameManager.Rank.Max];
	[SerializeField]
	private StateManager stateManager;
	[SerializeField]
	private ItemManager itemManager;
	[SerializeField]
    private ItemDropper itemDropper;
	[SerializeField]
	private ItemMerger itemMerger;
	[SerializeField]
	private CustomerManager customerManager;
	[SerializeField]
	private OrderManager orderManager;
	[SerializeField]
	private ScoreManager scoreManager;
	[SerializeField]
	private FameManager fameManager;
	[SerializeField]
	private RankingManager rankingManager;
	[SerializeField]
	private NextItemUI nextItemUI;

	[SerializeField]
	private GameMenuUI menuUI;

	[SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameClearUI;

    [SerializeField]
	private AudioClip audioOffered;
	AudioSource audioSource;

	private float currentDropPosition = 0.5f;

    private int nextItemID;

	private void Awake()
	{
		nextItemID = GetNextItemID();
		itemMerger.SetItemDataList(itemDataList);
		nextItemUI.SetItemDataList(itemDataList);
		audioSource = GetComponent<AudioSource>();

        stateManager.OnChangeStateBefore += OnEndState;
        stateManager.OnChangeStateAfter += OnStartState;
    }
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
        menuUI.gameObject.SetActive(false);
		gameOverUI.SetActive(false);
        gameClearUI.SetActive(false);
        itemDropper.SetPosition(currentDropPosition);
		nextItemUI.Apply(nextItemID);
		stateManager.ChangeState(StateManager.State.Ready);
	}
	void EnterCustomer()
	{
		customerManager.EnterCustomer();
		float[] timerData = { 30.0f, 25.0f, 20.0f, 15.0f };
		Invoke("EnterCustomer", Random.Range(5.0f, timerData[(int)fameManager.CurrentRank]));
	}
	void OnEndState(StateManager.State state)
    {
        switch (state)
        {
            case StateManager.State.Ready:
                break;
        }
    }
    void OnStartState(StateManager.State state)
    {
        switch (state)
        {
			case StateManager.State.Ready:
				break;
			case StateManager.State.Game:
                Invoke("EnterCustomer", 3.0f);
                break;
			case StateManager.State.GameOver:
                Time.timeScale = 0.0f;
                gameOverUI.SetActive(true);
                StartCoroutine(GameOverCoroutine());
                break;
			case StateManager.State.GameClear:
                Time.timeScale = 0.0f;
                rankingManager.RegisterScore(scoreManager.Score);
                gameClearUI.SetActive(true);
                StartCoroutine(GameClearCoroutine());
				break;
        }
    }
    // Update is called once per frame
    void Update()
	{
		if (TransitionManager.Instance.AbleTransition == false) return;
		if (stateManager.CurrentState != StateManager.State.Game) return;
		if (menuUI.gameObject.activeInHierarchy) return;

		float horizontal = PlayerInput.GetAxis(PlayerInput.AxisType.Horizontal);
        if (Mathf.Abs(horizontal) > 0.01f)
        {
            currentDropPosition += horizontal * 0.5f * Time.deltaTime;
            currentDropPosition = Mathf.Clamp(currentDropPosition, 0.0f, 1.0f);
            itemDropper.SetPosition(currentDropPosition);
        }

        if (PlayerInput.Instance.Down(PlayerInput.ButtonType.Drop))
        {
            Item dropItem = itemDropper.Drop();
			if (dropItem != null)
			{
			}
		}
		if (PlayerInput.Instance.Down(PlayerInput.ButtonType.Offer))
		{
			if (customerManager.IsAbleOffer)
			{
				OrderData orderData = customerManager.CurrentOrder;
				foreach (OrderData.Ingredient ingredient in orderData.ingredients)
				{
					for (int i = 0; i < ingredient.num; i++)
					{
						itemManager.RemoveItem(itemManager.FindItem(ingredient.itemData.ID));
					}
				}
				scoreManager.AddScore(orderData.price);
				fameManager.AddPoint(orderData.famePoint);
				customerManager.CompleteLeaveCustomer();
				audioSource.PlayOneShot(audioOffered);
				if (customerManager.CurrentCustomerNum == 0)
				{
					//customerManager.EnterCustomer();
				}
			}
		}
		if (PlayerInput.Instance.Down(PlayerInput.ButtonType.Next))
		{
			customerManager.Next();
		}
		if (PlayerInput.Instance.Down(PlayerInput.ButtonType.Prev))
		{
			customerManager.Prev();
		}

		if (PlayerInput.Instance.Down(PlayerInput.ButtonType.Menu))
		{
			menuUI.Open();
		}
	}
	public ItemData GetItemData(int ID)
	{
		return itemDataList[ID];
	}
	public void ManageItem(Item item)
	{
		itemManager.AddItem(item);
		orderManager.UpdateEnoughIngredient(itemManager);
	}
	int GetNextItemID()
    {
		ItemDataList dataList = dropItemLists[(int)fameManager.CurrentRank];
		return dataList.list[Random.Range(0, dataList.Num)].ID;
	}
	public void OnMerged(int ID, Vector2 pos, Item item1, Item item2)
	{
		ItemData itemData = itemDataList[ID];
		Item newItem = CreateItem(ID, pos);
		itemManager.RemoveItem(item1);
		itemManager.RemoveItem(item2);
		if (newItem != null)
		{
			itemManager.AddItem(newItem);
			orderManager.UpdateEnoughIngredient(itemManager);
		}
	}
	public Item CreateNextItem()
	{
		if (scoreManager.SubtractScore(GetItemData(nextItemID).price))
		{
			Item item = CreateItem(nextItemID, Vector2.zero);
			nextItemID = GetNextItemID();
			nextItemUI.Apply(nextItemID);
			return item;
		}
		return null;
	}
	Item CreateItem(int ID, Vector2 pos)
	{
		if (itemDataList[ID].prefab != null)
		{
			Item item = Instantiate(itemDataList[ID].prefab, pos, Quaternion.identity).GetComponent<Item>();
			item.OnInsideGameOverRange += GameOver;
			item.SetID(ID);
			item.SetItemMerger(itemMerger);
			return item;
		}
		return null;
	}
	public void GotoTitle()
	{
		Time.timeScale = 0.0f;
		TransitionManager.Instance.OnCloseComplete += () =>
		{
			SceneManager.LoadScene("Title");
			Time.timeScale = 1.0f;
			TransitionManager.Instance.Open(0.5f);
		};
		TransitionManager.Instance.Close();
	}
	public void Retry()
	{
		Time.timeScale = 0.0f;
		TransitionManager.Instance.OnCloseComplete += () =>
		{
			SceneManager.LoadScene("Game");
			Time.timeScale = 1.0f;
			TransitionManager.Instance.Open(0.5f);
		};
		TransitionManager.Instance.Close();
	}
	void GameOver()
	{
		Debug.Log("GameOver");
		stateManager.ChangeState(StateManager.State.GameOver);
	}
	IEnumerator GameOverCoroutine()
	{
		yield return new WaitForSecondsRealtime(3);
		TransitionManager.Instance.OnCloseComplete += () =>
		{
			SceneManager.LoadScene("Title");
			Time.timeScale = 1.0f;
			TransitionManager.Instance.Open(0.5f);
		};
		TransitionManager.Instance.Close();
    }
    IEnumerator GameClearCoroutine()
    {
        yield return new WaitForSecondsRealtime(3);
        TransitionManager.Instance.OnCloseComplete += () =>
        {
            SceneManager.LoadScene("Result");
            Time.timeScale = 1.0f;
            TransitionManager.Instance.Open(0.5f);
        };
        TransitionManager.Instance.Close();
    }
    public void ItemOffered(Item item)
	{
		itemManager.RemoveItem(item);
		item.Offered();
	}
}
