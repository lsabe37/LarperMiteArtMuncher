using System;
using UnityEngine;

public enum NpcState { Idle, Talking, Shocked }

public class NpcController : MonoBehaviour
{
    [Header("Npc Components")]
    [SerializeField] private NpcData m_npcData;
    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private Animator m_anim;
    public bool IsSpeaking;

    public static event Action<DialogueData> OnSendDialogue;

    private void Start()
    {
        SetCurrentState(NpcState.Idle);
    }

    private void OnEnable()
    {
        PlayerController.OnInteract += SendDialogue;
        DialogueManager.OnShocked += SetCurrentState;
        DialogueManager.OnSuccess += SetCurrentState;
    }

    private void OnDisable()
    {
        PlayerController.OnInteract -= SendDialogue;
        DialogueManager.OnShocked -= SetCurrentState;
        DialogueManager.OnSuccess += SetCurrentState;
    }

    private void SetCurrentState(NpcState state)
    {
        m_npcData.currentState = state;

        switch (m_npcData.currentState)
        {
            case NpcState.Idle:
                m_anim.SetBool("walk", false);
                m_anim.SetBool("interact", false);
                break;
            case NpcState.Talking:
                m_anim.SetBool("interact", true);
                break;
            case NpcState.Shocked:
                m_anim.SetBool("shock", true);
                m_anim.SetBool("interact", false);
                break;
        }
    }

    private void SendDialogue()
    {
        if (IsSpeaking == false) return;

        OnSendDialogue?.Invoke(m_npcData.npcDialogue);
        SetCurrentState(NpcState.Talking);
    }
}
