using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    [SerializeField]
    ScrollRect scrollRect;
    [SerializeField]
    float pps = 500.0f;
	[SerializeField]
	float height = 0;
    [SerializeField]
    float position = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		scrollRect.verticalNormalizedPosition = 1.0f;
		height = scrollRect.content.sizeDelta.y;

	}

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.GetDown(PlayerInput.ButtonType.Decide) ||
			PlayerInput.GetDown(PlayerInput.ButtonType.Cancel))
        {
            SceneManager.LoadScene("Title");
        }
        float axis = PlayerInput.GetAxis(PlayerInput.AxisType.Vertical);
		if (Mathf.Abs(axis) > 0.1f)
        {
            position += pps * -axis * Time.deltaTime;
            position = Mathf.Clamp(position, 0, height);
            Scroll(position);
        }
	}
    void Scroll(float position)
    {
        scrollRect.verticalNormalizedPosition = 1.0f - (position / height);

	}
}
