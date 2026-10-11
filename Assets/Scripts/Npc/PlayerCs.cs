using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCs : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerData m_playerData;
    [SerializeField] private Animator m_anim;


    private void Start()
    {
        m_anim.SetBool("walk", true);
    }

    public void StopWalking()
    {
        m_anim.SetBool("walk", false);
    }
}
