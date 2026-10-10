using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Scriptable Objects/NpcData")]
public class NpcData : ScriptableObject
{
    [SerializeField] private DialogueData m_dialogue;
    public DialogueData npcDialogue { get { return m_dialogue; } set { m_dialogue = value; } }

    [ReadOnly][SerializeField] private NpcState m_currentState;
    public NpcState currentState { get { return m_currentState; } set { m_currentState = value; } }

}
