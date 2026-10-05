using System;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public const int MaxCustomer = 5;
    [SerializeField]
    private CustomerDataList[] customerLists = new CustomerDataList[(int)FameManager.Rank.Max];
    [SerializeField]
    private Customer[] customers = new Customer[MaxCustomer];
    [SerializeField]
    private GameObject objCursor;

	[SerializeField]
	private ItemManager itemManager;
	[SerializeField]
    private OrderManager orderManager;
	[SerializeField]
	private FameManager fameManager;
    [SerializeField]
    private AudioClip audioEnter;
	[SerializeField]
	private AudioClip audioAngry;
	private AudioSource audioSource;
	[SerializeField]
    private int currentSelectedCustomerIdx = 0;
    [SerializeField]
    private int enteringCustomerNum = 0;
	public int CurrentCustomerNum { get { return enteringCustomerNum; } }
    public bool IsAbleOffer {
        get {
            if (enteringCustomerNum == 0) return false;
            return orderManager.IsAbleOffer;
		}
    }
	public OrderData CurrentOrder
	{
		get { return customers[currentSelectedCustomerIdx].GetOrder(); }
	}
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        orderManager.NoOrder();
		objCursor.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if (enteringCustomerNum == 0) return;
		Customer selectedCustomer = customers[currentSelectedCustomerIdx];
        orderManager.UpdateTime(selectedCustomer.GetTimeAmmount());
	}
    public int FindVacancy()
	{
		for (int i = 0; i < MaxCustomer; i++)
		{
			if (customers[i].IsEntering == false)
			{
				return i;
			}
		}
        return -1;
	}
    public int EnterCustomer()
    {
        int index = FindVacancy();
        if (index >= 0)
        {
			CustomerDataList customerDataList = customerLists[(int)fameManager.CurrentRank];

			CustomerData data = customerDataList[UnityEngine.Random.Range(0, customerDataList.Num)];
            int orderIdx = UnityEngine.Random.Range(0, data.orders.Length);
            enteringCustomerNum++;

            Customer customer = customers[index];
            customer.Enter(data, orderIdx, index);
            customer.OnTimeupLeave += TimeupLeaveCustomer;
			if (enteringCustomerNum == 1)
            {
				orderManager.SetOrder(data.orders[orderIdx], itemManager, 1.0f);
                currentSelectedCustomerIdx = 0;
                ShowOrder(customer);
				objCursor.SetActive(true);
			}
            
			audioSource.PlayOneShot(audioEnter);
		}
        return index;
    }
    public void Next()
	{
		if (enteringCustomerNum == 0) return;
        for (int i = currentSelectedCustomerIdx + 1; i < MaxCustomer; i++)
        {
            if (customers[i].IsEntering)
			{
                currentSelectedCustomerIdx = i;
				ShowOrder(customers[currentSelectedCustomerIdx]);
                return;
			}
		}
		for (int i = 0; i < currentSelectedCustomerIdx; i++)
		{
			if (customers[i].IsEntering)
			{
				currentSelectedCustomerIdx = i;
				ShowOrder(customers[currentSelectedCustomerIdx]);
				return;
			}
		}
	}
	public void Prev()
	{
        if (enteringCustomerNum == 0) return;
		for (int i = currentSelectedCustomerIdx - 1; i >= 0; i--)
		{
			if (customers[i].IsEntering)
			{
				currentSelectedCustomerIdx = i;
				ShowOrder(customers[currentSelectedCustomerIdx]);
				return;
			}
		}
		for (int i = MaxCustomer - 1; i > currentSelectedCustomerIdx; i--)
		{
			if (customers[i].IsEntering)
			{
				currentSelectedCustomerIdx = i;
				ShowOrder(customers[currentSelectedCustomerIdx]);
				return;
			}
		}
	}
    private void ShowOrder(Customer customer)
	{
		objCursor.GetComponent<RectTransform>().anchoredPosition = customer.GetPosition();
		orderManager.SetOrder(customer.GetOrder(), itemManager, customer.GetTimeAmmount());
	}
	public void CompleteLeaveCustomer()
	{
		Debug.Log("CompleteLeaveCustomer");
		LeaveCustomer(currentSelectedCustomerIdx);
	}
	private void TimeupLeaveCustomer(int idx)
	{
		Debug.Log("TimeupLeaveCustomer");
		LeaveCustomer(idx);
		audioSource.PlayOneShot(audioAngry);
	}
	private void LeaveCustomer(int idx)
	{
		Debug.Log("LeaveCustomer");
		Customer customer = customers[idx];
		customer.Leave();
		enteringCustomerNum--;
        if (enteringCustomerNum == 0)
        {
            orderManager.NoOrder();
            objCursor.SetActive(false);
        }
        else
        {
            if (currentSelectedCustomerIdx == idx)
            {
                bool isFound = false;
                for (int i = idx - 1; i >= 0; i--)
                {
                    if (customers[i].IsEntering)
                    {
                        currentSelectedCustomerIdx = i;
						isFound = true;
						break;
                    }
                }
                if (isFound == false)
				{
					for (int i = idx + 1; i < MaxCustomer; i++)
					{
						if (customers[i].IsEntering)
						{
							currentSelectedCustomerIdx = i;
							isFound = true;
							break;
						}
					}
				}
                ShowOrder(customers[currentSelectedCustomerIdx]);
			}
		}
	}
}
