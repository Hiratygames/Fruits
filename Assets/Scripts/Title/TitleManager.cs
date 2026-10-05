using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private enum State
    {
        PressButton,
        Menu,
    }
    [SerializeField]
    private PressButtonUI pressButtonUI;
	[SerializeField]
	private TitleMenuUI menuUI;
	private AudioSource audioSource;

    State state = State.PressButton;
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		pressButtonUI.Show();
		menuUI.gameObject.SetActive(false);
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TransitionManager.Instance.AbleTransition == false) return;
		switch (state)
		{
			case State.PressButton: UpdatePressButton(); break;
			case State.Menu: UpdateMenu(); break;
		}
    }
    void UpdatePressButton()
	{
		if (PlayerInput.GetDown(PlayerInput.ButtonType.Decide))
		{
			pressButtonUI.Press();
			pressButtonUI.Hide();
			menuUI.gameObject.SetActive(true);
			state = State.Menu;
		}
	}
    void UpdateMenu()
	{
		if (PlayerInput.GetDown(PlayerInput.ButtonType.Decide))
		{
			switch (menuUI.CurrentType)
			{
				case TitleMenuUI.Type.Game:
					audioSource.Play();
					GotoGame();
					break;
				case TitleMenuUI.Type.Tutorial:
					GotoTutorial();
					break;
				case TitleMenuUI.Type.Credit:
					GotoCredit();
					break;

			}
			return;
		}
		if (PlayerInput.GetDown(PlayerInput.DirectionType.Down))
		{
			menuUI.Down();
		}
		if (PlayerInput.GetDown(PlayerInput.DirectionType.Up))
		{
			menuUI.Up();
		}
	}
    void GotoGame()
    {
        TransitionManager.Instance.OnCloseComplete += () =>
		{
			SceneManager.LoadScene("Game");
			TransitionManager.Instance.Open(0.5f);
		};
        TransitionManager.Instance.Close();
	}
	void GotoTutorial()
	{
		SceneManager.LoadScene("Tutorial");
	}
	void GotoCredit()
	{
		SceneManager.LoadScene("Credit");
	}
}
