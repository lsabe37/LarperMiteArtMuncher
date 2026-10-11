using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    [SerializeField] private GameObject nextButton;

    private void OnEnable()
    {
        TypewriterEffect.CompleteTextRevealed += ShowNextButton;
    }

    private void OnDisable()
    {
        TypewriterEffect.CompleteTextRevealed -= ShowNextButton;
    }

    private void ShowNextButton()
    {
        nextButton.SetActive(true);
    }

    private void HideNextButton()
    {
        nextButton.SetActive(false);
    }
}
