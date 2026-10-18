using System;
using UnityEngine;
using UnityEngine.SceneManagement; 
public enum PlayerState { Idle, Walking, Talking, Painting, Dead }

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private Animator m_anim;
    [SerializeField] private SceneManagement sceneManager;
    [SerializeField] private AudioScripting audio;

    [SerializeField] LayerMask targetLayer;

    [Header("Spotlight")]
    [SerializeField] private GameObject m_spotlightA;
    [SerializeField] private GameObject m_spotlightB;
    [SerializeField] private GameObject m_spotlightC;

    private Vector2 m_movementInput;
    private bool m_flipped;
    private bool m_closeToNpc;
    private bool m_closeToPainting;

    public static Action OnInteract;

    private void Start()
    {
        SetCurrentState(PlayerState.Idle);
        CheckSaveData();
    }

    private void CheckSaveData()
    {
        transform.position = SaveData.saved_position;
    }

    private void OnEnable()
    {
        DialogueManager.EndDialogueState += SetCurrentState;
        DialogueManager.OnDeath += SetCurrentState;
    }

    private void OnDisable()
    {
        DialogueManager.EndDialogueState -= SetCurrentState;
        DialogueManager.OnDeath -= SetCurrentState;

        if (audio != null)
            audio.StopFootsteps();
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

            // G to interact with painting
            if (Input.GetKeyDown(KeyCode.G) && m_playerData.currentState != PlayerState.Talking && m_playerData.currentState != PlayerState.Painting)
            {
                if (m_closeToPainting == true)
                {
                    SetCurrentState(PlayerState.Painting);
                    SaveData.saved_position = transform.position;
                    SceneManager.LoadScene(2);
                }
            }
        }
        if (audio != null)
        {
            if (m_playerData.currentState == PlayerState.Walking && m_rb.velocity.sqrMagnitude > 0.001f)
            {
                audio.StartFootsteps();
            }
            else
            {
                audio.StopFootsteps();
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
                m_anim.SetBool("walk", false);
                m_rb.velocity = Vector2.zero;
                break;
            case PlayerState.Painting:
                m_anim.SetBool("walk", false);
                m_rb.velocity = Vector2.zero;
                break;
            case PlayerState.Dead:
                PlayerDeath();
                break;
        }
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
        m_anim.SetTrigger("dead");
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

        else if (collider.CompareTag("Painting"))
        {
            Debug.Log("Interacting with Painting");

            m_closeToPainting = true;
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

        else if (collider.CompareTag("Painting"))
        {
            Debug.Log("No longer interacting with Painting");

            m_closeToPainting = false;
        }
    }

    private void CreateSpotlightA()
    {
        Vector2 currentPos = transform.position;
        Instantiate(m_spotlightA, currentPos, Quaternion.identity);
    }
    private void CreateSpotlightB()
    {
        Vector2 currentPos = transform.position;
        Instantiate(m_spotlightB, currentPos, Quaternion.identity);
    }
    private void CreateSpotlightC()
    {
        Vector2 currentPos = transform.position;
        Instantiate(m_spotlightC, currentPos, Quaternion.identity);
    }

}
