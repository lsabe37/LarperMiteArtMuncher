using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    private TMP_Text _dialogueBox;

    // Basic Typewriter Functionality
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

    private WaitForSeconds _normalDelay;
    private WaitForSeconds _punctuationDelay;

    [Header("Settings")]
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] private float punctuationDelay = 0.5f;

    // Skip Dialogue Functionality
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip Options")]
    [SerializeField] private bool quickSkip;
    [SerializeField][Min(1)] private int skipSpeedup = 5;

    // Event Functionality
    private WaitForSeconds _textboxFullEventDelay;
    [SerializeField][Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;


    private void Awake()
    {
        _dialogueBox = GetComponent<TMP_Text>();

        _normalDelay = new WaitForSeconds(1 / charactersPerSecond);
        _punctuationDelay = new WaitForSeconds(punctuationDelay);

        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));

        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    private void Start()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
    }

    private void OnDisbale()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_dialogueBox.maxVisibleCharacters != _dialogueBox.textInfo.characterCount - 1)
                Skip();
        }
    }

    public void PrepareForNewText(UnityEngine.Object obj)
    {
        if (obj != _dialogueBox || !_readyForNewText || _dialogueBox.maxVisibleCharacters >= _dialogueBox.textInfo.characterCount)
            return;

        CurrentlySkipping = false;
        _readyForNewText = false;

        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _dialogueBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _dialogueBox.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;
            if (_currentVisibleCharacterIndex == lastCharacterIndex)
            {
                Debug.Log("typing");
                _dialogueBox.maxVisibleCharacters++;
                yield return _textboxFullEventDelay;
                CompleteTextRevealed?.Invoke();
                _readyForNewText = true;
                yield break;
            }

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            _dialogueBox.maxVisibleCharacters++;

            if (!CurrentlySkipping && (character == '?' || character == '.' || character == ':' || character == ';' || character == '!' || character == '-'))
            {
                yield return _punctuationDelay;
            }
            else
            {
                yield return CurrentlySkipping ? _skipDelay : _normalDelay;
            }

            CharacterRevealed?.Invoke(character);
            _currentVisibleCharacterIndex++;
        }
    }

    private void Skip(bool quickSkipNeeded = false)
    {
        if (CurrentlySkipping)
            return;

        CurrentlySkipping = true;

        if (!quickSkip || !quickSkipNeeded)
        {
            StartCoroutine(SkipSpeedupReset());
            return;
        }

        StopCoroutine(_typewriterCoroutine);
        _dialogueBox.maxVisibleCharacters = _dialogueBox.textInfo.characterCount;
        _readyForNewText = true;
        CompleteTextRevealed?.Invoke();
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _dialogueBox.maxVisibleCharacters == _dialogueBox.textInfo.characterCount - 1);
        CurrentlySkipping = false;
    }

}
