using System;
using UnityEngine;

public enum PlayerState { Idle, Walking, Talking, Painting, Dead }

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private Animator m_anim;

    [SerializeField] LayerMask targetLayer;

    private Vector2 m_movementInput;
    private bool m_flipped;
    private bool m_closeToNpc;

    public static Action OnInteract;

    private void Start()
    {
        SetCurrentState(PlayerState.Idle);
    }

    private void OnEnable()
    {
        DialogueManager.EndDialogueState += EndTalkState;
    }

    private void Update()
    {
        if (m_playerData.currentState != PlayerState.Dead)
        {
            m_movementInput.x = Input.GetAxisRaw("Horizontal");
            m_movementInput = m_movementInput.normalized;

            if (m_rb.velocity != Vector2.zero && m_playerData.currentState != PlayerState.Talking && m_playerData.currentState != PlayerState.Painting)
            {
                SetCurrentState(PlayerState.Walking);
            }
            else if (m_rb.velocity == Vector2.zero && m_playerData.currentState != PlayerState.Talking && m_playerData.currentState != PlayerState.Painting)
            {
                SetCurrentState(PlayerState.Idle);
            }

            // Flip player when turning
            if (m_movementInput.x > 0 && m_flipped && m_playerData.currentState != PlayerState.Talking && m_playerData.currentState != PlayerState.Painting)
            {
                FlipPlayer();
            }
            else if (m_movementInput.x < 0 && !m_flipped && m_playerData.currentState != PlayerState.Talking && m_playerData.currentState != PlayerState.Painting)
            {
                FlipPlayer();
            }

            // F to talk to Npc
            if (Input.GetKeyDown(KeyCode.F) && m_playerData.currentState != PlayerState.Talking && m_playerData.currentState != PlayerState.Painting)
            {
                if(m_closeToNpc == true)
                {
                    SetCurrentState(PlayerState.Talking);
                    OnInteract?.Invoke();
                }
            }
        }
    }

    // Player movement
    private void FixedUpdate()
    {
        if (m_playerData.currentState != PlayerState.Talking && m_playerData.currentState != PlayerState.Painting && m_playerData.currentState != PlayerState.Dead)
        {
            m_rb.velocity = new Vector2(m_movementInput.x * m_playerData.playerSpeed, m_movementInput.y * 0);
        }
    }

    private void SetCurrentState(PlayerState state)
    {
        m_playerData.currentState = state;

        switch (m_playerData.currentState)
        {
            case PlayerState.Idle:
                m_anim.SetBool("walk", false);
                break;
            case PlayerState.Walking:
                m_anim.SetBool("walk", true);
                break;
            case PlayerState.Talking:
                m_anim.SetTrigger("interact");
                m_rb.velocity = Vector2.zero;
                break;
            case PlayerState.Painting:
                m_anim.SetTrigger("interact");
                m_rb.velocity = Vector2.zero;
                break;
            case PlayerState.Dead:
                PlayerDeath();
                break;
        }
    }

    private void EndTalkState()
    {
        SetCurrentState(PlayerState.Idle);
    }

    private void ExitActionState()
    {
        if (m_rb.velocity != Vector2.zero)
        {
            m_playerData.currentState = PlayerState.Walking;
        }
        else
        {
            m_playerData.currentState = PlayerState.Idle;
        }
    }

    // Flip player logic
    private void FlipPlayer()
    {
        m_flipped = !m_flipped;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Player death logic
    private void PlayerDeath()
    {
        m_anim.SetBool("dead", true);
        m_anim.SetBool("walk", false);
        m_rb.velocity = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Npc"))
        {
            Debug.Log("Interacting with Npc");

            NpcController npcController = collider.gameObject.GetComponent<NpcController>();
            m_closeToNpc = true;
            npcController.IsSpeaking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Npc"))
        {
            Debug.Log("No longer interacting with Npc");

            NpcController npcController = collider.gameObject.GetComponent<NpcController>();
            m_closeToNpc = false;
            npcController.IsSpeaking = false;
        }
    }

}
