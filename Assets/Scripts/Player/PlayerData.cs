using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float m_playerSpeed;
    public float playerSpeed { get { return m_playerSpeed; } }

    [SerializeField] private float m_raycastDistance;
    public float raycastDistance { get { return m_raycastDistance; } }

    [ReadOnly][SerializeField] private PlayerState m_currentState;
    public PlayerState currentState { get { return m_currentState; } set { m_currentState = value; } }

}
