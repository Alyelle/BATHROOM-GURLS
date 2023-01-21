using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BottomBarController : MonoBehaviour
{
    public static BottomBarController Instance;

    public TMP_Text DialogueText;
    public TMP_Text NameText;

    public TextEngine CurrentText;

    Coroutine currentPlayingCoroutine;

    [HideInInspector]
    public int SentenceIndex
    {
        get
        {
            return sentenceIndex;
        }
        set
        {
            sentenceIndex = value;

            if (sentenceIndex >= CurrentText.sentences.Count)
                sentenceIndex = CurrentText.sentences.Count - 1;

            if (currentPlayingCoroutine != null)
                StopCoroutine(currentPlayingCoroutine);

            currentPlayingCoroutine = StartCoroutine(TypeText(CurrentText.sentences[sentenceIndex].text, CurrentText.sentences[sentenceIndex].delay));
        }
    }

    int sentenceIndex = 0;

    State state = State.COMPLETED;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (CurrentText == null)
            return;

        SentenceIndex = 0;
    }

    private void OnEnable()
    {
        if (CurrentText == null)
            return;

        SentenceIndex = 0;
    }

    private void Update()
    {
        if (CurrentText == null)
            return;

        if (state == State.COMPLETED && Input.GetMouseButtonDown(0))
        {
            SentenceIndex++;
        } 
    }

    private IEnumerator TypeText(string text, float delay)
    {
        DialogueText.text = ""; // Reset the dialogue box

        state = State.PLAYING; // Set the play state of the dialogue

        int wordIndex = 0; // The position of the text character inside the string

        while (state != State.COMPLETED) 
        {
            DialogueText.text += text[wordIndex];

            yield return new WaitForSeconds(delay);

            wordIndex++;

            if (wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }

    private enum State
    {
        PLAYING, COMPLETED
    }

    public event Action onDialogueEnd;
    public void OnDialogueEnd() { if (onDialogueEnd != null) onDialogueEnd(); }
}
