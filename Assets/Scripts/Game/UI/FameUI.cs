using System;
using UnityEngine;
using UnityEngine.UI;

public class FameUI : MonoBehaviour
{
    [SerializeField]
    Image imageRank;
    [SerializeField]
    Image imageBar;
    private RectTransform rectTransformBar;
	private void Awake()
	{
		rectTransformBar = imageBar.GetComponent<RectTransform>();
        rectTransformBar.localScale = new Vector3(0, 1, 1);
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apply(Sprite sprite)
	{
		imageRank.sprite = sprite;
	}
    public void Apply(float f)
	{
		rectTransformBar.localScale = new Vector3(f, 1, 1);
	}
}
