using UnityEngine;
using UnityEngine.UI;

public class CustomerUI : MonoBehaviour
{
    [SerializeField]
    private Image imageFace;
	[SerializeField]
	private Image imageOrder;
	[SerializeField]
    private TimerGaugeUI timerGauge;

    private CustomerData customerData;
    public bool IsEnter { get; private set; } = false;
    public void Set(CustomerData data, int orderIdx)
    {
        customerData = data;

		imageFace.sprite = data.sprite;
        imageOrder.sprite = data.orders[orderIdx].sprite;
		timerGauge.Reset();
        IsEnter = true;
		gameObject.SetActive(true);
	}
    public void Hide()
	{
		IsEnter = false;
		gameObject.SetActive(false);
	}
    public void UpdateTime(float t)
	{
		timerGauge.UpdateTime(t);
	}
}
