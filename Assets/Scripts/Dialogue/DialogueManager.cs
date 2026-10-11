using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    // this script handles starting, progress, and ending dialogue with NPC's

    [SerializeField] private GameObject m_uiBackground;
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private TMP_Text m_speakerName;
    [SerializeField] private TMP_Text m_dialogueText;
    [SerializeField] private Image m_speakerIcon;

    public DialogueData currentData;

    private int m_index;

    private void OnEnable()
    {
        NpcController.OnSendDialogue += SetDialogue;
    }

    private void OnDisable()
    {
        NpcController.OnSendDialogue -= SetDialogue;
    }

    private void SetDialogue(DialogueData data)
    {
        currentData = data;
        StartDialogue();
    }

    public void StartDialogue()
    {
        m_uiBackground.SetActive(true);
        m_dialogueBox.SetActive(true);

        m_speakerName.text = currentData.DataClass.speakerName;
        m_dialogueText.text = currentData.DataClass.dialogueLines[m_index];
        m_speakerIcon.sprite = currentData.DataClass.speakerIcon;
    }

    public void NextDialogue()
    {
        m_index++;

        if (m_index < currentData.DataClass.dialogueLines.Length) m_dialogueText.text = currentData.DataClass.dialogueLines[m_index];
        else EndDialogue();
    }

    public void EndDialogue()
    {
        m_uiBackground.SetActive(false);
        m_dialogueBox.SetActive(false);
        currentData = null;
        m_index = 0;
    }
}

