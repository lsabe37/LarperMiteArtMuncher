using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    // this script handles starting, progress, and ending dialogue with NPC's

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image speakerIcon;

    [SerializeField] private DialogueData currentData;

    public static event Action OnFinishedOrdering;

    private int _index;

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
        dialogueBox.SetActive(true);

        speakerName.text = currentData.DataClass.speakerName;
        dialogueText.text = currentData.DataClass.dialogueLines[_index];
        speakerIcon.sprite = currentData.DataClass.speakerIcon;
    }

    public void NextDialogue()
    {
        _index++;

        if (_index < currentData.DataClass.dialogueLines.Length) dialogueText.text = currentData.DataClass.dialogueLines[_index];
        else EndDialogue();
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        currentData = null;
        _index = 0;
        OnFinishedOrdering();
    }
}

