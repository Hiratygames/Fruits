using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		if (PlayerInput.GetDown(PlayerInput.ButtonType.Decide) ||
			PlayerInput.GetDown(PlayerInput.ButtonType.Cancel))
		{
			SceneManager.LoadScene("Title");
		}
	}
}
