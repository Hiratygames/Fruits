using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    NumberDisplay dispTime;
    [SerializeField] Text textAddTime;
    [SerializeField] Animator animAddTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(int time)
    {
        dispTime.Set(time);
    }
    public void ShowAddTime(int add)
    {
        animAddTime.SetTrigger("Add");
        textAddTime.text = "+" + add.ToString() + "•b";
    }
}
