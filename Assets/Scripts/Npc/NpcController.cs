using System;
using UnityEngine;

public enum NpcState { Idle, Walking, Talking, Shocked }

public class NpcController : MonoBehaviour
{
    [Header("Npc Components")]
    [SerializeField] private NpcData m_npcData;
    [SerializeField] private Collider2D m_collider;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private Animator m_anim;
    public bool IsSpeaking;

    [Header("Target Ref")]
    [SerializeField] private Transform paintingLocation;

    public static event Action<DialogueData> OnSendDialogue;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        CheckSaveData();
    }

    private void CheckSaveData()
    {
        if(SaveData.passed_painting == true)
        {
            if (SaveData.current_painting == m_npcData.Id + 1)
            {
                SendDialogue();
                Debug.Log("Interacting with ant!");
                SetCurrentState(NpcState.Walking);
            }
            else
            {
                SetCurrentState(NpcState.Idle);
            }
        }
        else if (SaveData.passed_painting == false)
        {
            if (SaveData.current_painting == m_npcData.Id)
            {
                Debug.Log("You are busted!");
                SetCurrentState(NpcState.Shocked);
            }
            else
            {
                SetCurrentState(NpcState.Idle);
            }
        }


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

    private void Update()
    {
        if(m_npcData.currentState == NpcState.Walking)
        {
            float move = m_npcData.speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, paintingLocation.position, move);

            float distance = Vector2.Distance(transform.position, paintingLocation.position);
            if(distance <= 2f)
            {
                SetCurrentState(NpcState.Talking);
            }
        }
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
            case NpcState.Walking:
                m_anim.SetBool("walk", true);
                break;
            case NpcState.Talking:
                m_anim.SetBool("interact", true);
                m_anim.SetBool("walk", false); 
                break;
            case NpcState.Shocked:
                m_anim.SetBool("shock", true);
                m_anim.SetBool("interact", false);
                m_anim.SetBool("walk", false);
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
