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

    [SerializeField] private string m_optionA;
    public string optionA { get { return m_optionA; } }
    [SerializeField] private string m_optionB;
    public string optionB { get { return m_optionB; } }

    [SerializeField] private int m_correctOption;
    public int correctOption { get { return m_correctOption; } }

    [SerializeField] private string m_correctResponse;
    public string correctResponse { get { return m_correctResponse; } }
    [SerializeField] private string m_wrongResponse;
    public string wrongResponse { get { return m_wrongResponse; } }

}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Objects/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private DialogueDataClass m_DataClass;
    public DialogueDataClass DataClass { get { return m_DataClass; } }
}
