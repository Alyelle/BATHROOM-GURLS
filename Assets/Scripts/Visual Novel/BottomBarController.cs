using Game.Utils;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    public static BottomBarController Instance;

    public KeyCode[] KeyBindings;
    public KeyCode[] SkipKeyBindings;
    public TMP_Text DialogueText;
    public TMP_Text NameText;

    public Image speakerSprite;

    public TextEngine CurrentText;

    Coroutine currentPlayingCoroutine;

    AudioSource currentVoiceline;

    Transform cameraTrans;

    Animator anim;

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

            currentPlayingCoroutine = StartCoroutine(TypeText(CurrentText.sentences[sentenceIndex].text, CurrentText.sentences[sentenceIndex].delay, CurrentText.sentences[sentenceIndex].voiceLine));

            NameText.text = CurrentText.sentences[sentenceIndex].speaker.speakerName;
            NameText.color = CurrentText.sentences[sentenceIndex].speaker.textColor;

            speakerSprite.sprite = CurrentText.sentences[sentenceIndex].speaker.sprites[CurrentText.sentences[sentenceIndex].speakerSpriteId];

            if (string.IsNullOrEmpty(CurrentText.sentences[sentenceIndex].animationName))
                anim.Play("None", -1, 0f);
            else
                anim.Play(CurrentText.sentences[sentenceIndex].animationName, -1, 0f);
        }
    }

    int sentenceIndex = 0;

    State state = State.COMPLETED;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        cameraTrans = Camera.main.transform;

        anim = speakerSprite.GetComponent<Animator>();
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

        bool keyPressed = false;
        foreach (KeyCode key in KeyBindings)
        {
            if (Input.GetKeyDown(key)) keyPressed = true;
        }

        bool skipKeyPressed = false;
        foreach (KeyCode key in SkipKeyBindings)
        {
            if (Input.GetKeyDown(key)) skipKeyPressed = true;
        }

        if (state == State.COMPLETED && (Input.GetMouseButtonDown(0) || keyPressed))
        {
            if (SentenceIndex >= CurrentText.sentences.Count - 1)
            {
                CurrentText = null;

                gameObject.SetActive(false); // Exit animation

                Time.timeScale = 1f;

                OnDialogueEnd();

                return;
            }

            SentenceIndex++;
        }
        else if (state == State.PLAYING && currentPlayingCoroutine != null && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || skipKeyPressed))
        {
            StopCoroutine(currentPlayingCoroutine);
            DialogueText.text = CurrentText.sentences[sentenceIndex].text;
            state = State.COMPLETED;
        }
    }

    private IEnumerator TypeText(string text, float delay, AudioClip voice)
    {
        if (currentVoiceline != null)
            Destroy(currentVoiceline.gameObject);

        currentVoiceline = AudioSystem.PlaySound(voice, cameraTrans.position, 1f, 128);

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
