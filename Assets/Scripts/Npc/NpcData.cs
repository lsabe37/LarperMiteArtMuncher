using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Scriptable Objects/NpcData")]
public class NpcData : ScriptableObject
{
    [SerializeField] private DialogueData m_dialogue;
    public DialogueData npcDialogue { get { return m_dialogue; } set { m_dialogue = value; } }

    [SerializeField] private int m_Id;
    public int Id { get { return m_Id; } set { m_Id = value; } }

    [SerializeField] private float m_speed = 3f;
    public float speed { get { return m_speed; } set { m_speed = value; } }

    [ReadOnly][SerializeField] private NpcState m_currentState;
    public NpcState currentState { get { return m_currentState; } set { m_currentState = value; } }

}
