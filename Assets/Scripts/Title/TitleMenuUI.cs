using UnityEngine;
using UnityEngine.UI;

public class TitleMenuUI : MonoBehaviour
{
    public enum Type
    {
        Game,
        Tutorial,
		Credit,
		Max
    }
    private Type currentType = Type.Game;
    public Type CurrentType { get { return currentType; } }
    [SerializeField]
    Text[] texts = new Text[(int)Type.Max];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (Type type = 0; type < Type.Max; type++)
        {
            Deselect(type);
        }
        Select(currentType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Down()
    {
        Deselect(currentType);
		currentType++;
		currentType = Clamp(currentType);
        Select(currentType);
	}
	public void Up()
	{
		Deselect(currentType);
		currentType--;
		currentType = Clamp(currentType);
		Select(currentType);
	}
    private void Deselect(Type type)
    {
        texts[(int)type].color = Color.gray;
    }
	private void Select(Type type)
	{
		texts[(int)type].color = Color.cyan;
	}
	private Type Clamp(Type type)
	{
		return (Type)Mathf.Clamp((int)type, 0, (int)(Type.Max) - 1);
	}
}
