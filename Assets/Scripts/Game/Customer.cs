using System;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private CustomerUI customerUI;

	private int ID;
	private CustomerData data;
	private int orderIndex;
	private float timer;

    public bool IsEntering { get; private set; } = false;
	public event Action<int> OnTimeupLeave;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		customerUI.Hide();
	}

    // Update is called once per frame
    void Update()
    {
        if (IsEntering)
        {
			timer -= Time.deltaTime;
			if (timer <= 0.0f)
			{
				OnTimeupLeave?.Invoke(ID);
			}
			else
			{
				customerUI.UpdateTime(GetTimeAmmount());
			}
		}
    }
    public void Enter(CustomerData data, int orderIndex, int ID)
    {
        this.data = data;
        this.orderIndex = orderIndex;
		this.ID = ID;
        timer = data.time;
		customerUI.Set(data, orderIndex);
		IsEntering = true;
	}
	public void Leave()
	{
		customerUI.Hide();
		OnTimeupLeave = null;
		IsEntering = false;
	}
	public OrderData GetOrder()
	{
		return data.orders[orderIndex];
	}
	public Vector3 GetPosition()
	{
		return customerUI.GetComponent<RectTransform>().anchoredPosition;
	}
	public float GetTimeAmmount()
	{
		return timer / data.time;
	}
}
