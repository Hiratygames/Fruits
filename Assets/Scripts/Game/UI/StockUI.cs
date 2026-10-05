using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockUI : MonoBehaviour
{
    [SerializeField]
    List<Text> textsStock = new List<Text>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(int ID, int num)
    {
        textsStock[ID].text = num.ToString();
    }
}
