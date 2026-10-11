using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    // this script handles starting, progress, and ending dialogue with NPC's
    [Header("UI Background")]
    [SerializeField] private GameObject m_uiBackground;

    [Header("Dialogue Box")]
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private TMP_Text m_speakerName;
    [SerializeField] private TMP_Text m_dialogueText;
    [SerializeField] private Image m_speakerIcon;

    [Header("Response Box")]
    [SerializeField] private GameObject m_optionsBox;
    [SerializeField] private TMP_Text m_optionA;
    [SerializeField] private TMP_Text m_optionB;
    [SerializeField] private Button m_buttonA;
    [SerializeField] private Button m_buttonB;

    public DialogueData currentData;
    private int m_index;
    private int m_playerSelectedOption;
    private bool m_answeredQuestion;

    public static event Action<PlayerState> EndDialogueState;
    public static event Action<NpcState> OnShocked;
    public static event Action<NpcState> OnSuccess;
    public static event Action<PlayerState> OnDeath;

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
        m_optionA.text = data.DataClass.optionA;
        m_optionB.text = data.DataClass.optionB;
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

        if (m_index < currentData.DataClass.dialogueLines.Length)
        {
            m_dialogueText.text = currentData.DataClass.dialogueLines[m_index];
        }
            
        else
        {
            m_optionsBox.SetActive(true);
        }

        if(m_answeredQuestion == true)
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        m_uiBackground.SetActive(false);
        m_dialogueBox.SetActive(false);
        
        currentData = null;
        m_index = 0;
        m_answeredQuestion = false;

        EndDialogueState?.Invoke(PlayerState.Idle);
    }

    // select response option logic for buttons
    public void SelectOptionA()
    {
        m_playerSelectedOption = 0;
        CheckAnswers();
    }

    public void SelectOptionB()
    {
        m_playerSelectedOption = 1;
        CheckAnswers();
    }

    // checks to see if selected option is correct or not
    private void CheckAnswers()
    {
        if(m_playerSelectedOption == currentData.DataClass.correctOption)
        {
            m_dialogueText.text = currentData.DataClass.correctResponse;
            OnSuccess?.Invoke(NpcState.Idle);
        }
        else
        {
            m_dialogueText.text = currentData.DataClass.wrongResponse;
            OnShocked?.Invoke(NpcState.Shocked);
            OnDeath?.Invoke(PlayerState.Dead);
        }
        m_answeredQuestion = true;
        m_optionsBox.SetActive(false);
    }
}

