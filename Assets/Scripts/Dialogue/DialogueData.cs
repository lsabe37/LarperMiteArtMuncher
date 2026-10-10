using UnityEngine;
using System;

[System.Serializable]
public class DialogueDataClass
{
    [SerializeField] private string m_speakerName;
    public string speakerName { get { return m_speakerName; } }
    [SerializeField] private Sprite m_speakerIcon;
    public Sprite speakerIcon { get { return m_speakerIcon; } }
    [SerializeField] private string[] m_dialogueLines;
    public string[] dialogueLines { get { return m_dialogueLines; } }
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private DialogueDataClass m_DataClass;
    public DialogueDataClass DataClass { get { return m_DataClass; } }
}
