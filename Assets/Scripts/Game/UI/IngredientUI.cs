using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Text textNum;
    [SerializeField]
    GameObject objCheck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDisp(bool isDisp)
    {
        gameObject.SetActive(isDisp);
	}
	public void Set(Sprite sprite, int num)
	{
        Set(sprite, num, false);
	}
	public void Set(Sprite sprite, int num, bool isEnough)
    {
        image.sprite = sprite;
        textNum.text = num.ToString();
		SetEnough(isEnough);
    }
    public void SetEnough(bool isEnough)
	{
		objCheck.SetActive(isEnough);
	}
}
