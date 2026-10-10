using System;
using UnityEngine;

public enum NpcState { Idle, Talking }

public class NpcController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private NpcData m_npcData;
    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Animator m_anim;

    public static event Action<DialogueData> OnSendDialogue;

    private void Start()
    {
        SetCurrentState(NpcState.Idle);
    }

    private void OnEnable()
    {
        PlayerController.OnInteract += SendDialogue;
    }

    private void OnDisable()
    {
        PlayerController.OnInteract -= SendDialogue;
    }


    private void SetCurrentState(NpcState state)
    {
        m_npcData.currentState = state;

        switch (m_npcData.currentState)
        {
            case NpcState.Idle:
                m_anim.SetBool("walk", false);
                break;
            case NpcState.Talking:
                m_anim.SetTrigger("interact");
                break;
        }
    }

    private void SendDialogue()
    {
        OnSendDialogue?.Invoke(m_npcData.npcDialogue);
    }
}
