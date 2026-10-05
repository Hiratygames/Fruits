using System.Collections;
using UnityEngine;

public class ReadyManager : MonoBehaviour
{
    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private AudioClip audioNext;
    [SerializeField]
    private AudioClip audioStart;
    private AudioSource audioSource;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        stateManager.OnChangeStateBefore += OnEndState;
        stateManager.OnChangeStateAfter += OnStartState;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayNextSE()
    {
        audioSource.PlayOneShot(audioNext);
    }
    void PlayStartSE()
    {
        audioSource.PlayOneShot(audioStart);
    }

    void OnStartState(StateManager.State state)
    {
        if (state == StateManager.State.Ready)
        {
            gameObject.SetActive(true);
            StartCoroutine(ReadyCoroutine());
        }
    }
    void OnEndState(StateManager.State state)
    {
        if (state == StateManager.State.Ready)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator ReadyCoroutine()
    {
        while (TransitionManager.Instance.CurrentState == Transition.State.Close || TransitionManager.Instance.CurrentState == Transition.State.Opening)
        {
            yield return null;
        }
        animator.SetTrigger("Ready");
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Ready") == false || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        stateManager.ChangeState(StateManager.State.Game);
    }
}
