using UnityEngine;
using UnityEngine.UI;

public class RankupUI : MonoBehaviour
{
    [SerializeField]
    Image imageRank;

    Animator animator;

	private void Awake()
	{
        animator = GetComponent<Animator>();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Show(Sprite sprite)
    {
        imageRank.sprite = sprite;
        gameObject.SetActive(true);
        Invoke("Hide", 1.5f);
    }
    public void Hide()
	{
		gameObject.SetActive(false);
	}
}
