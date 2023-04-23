using Game.Utils;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    public static BottomBarController Instance;

    public KeyCode[] KeyBindings;
    public KeyCode[] SkipKeyBindings;
    public KeyCode[] SpamKeyBindings;

    public float SpamDelay = 0.05f;

    public TMP_Text DialogueText;
    public TMP_Text NameText;

    public Image speakerSprite;
    public Image bg;
    public AudioSource bgm;

    public TextEngine CurrentText;

    Coroutine currentPlayingCoroutine;

    // Coroutine fadingCoroutine;

    // Color defaultSpriteColor;

    AudioSource currentVoiceline;

    Transform cameraTrans;

    Animator anim;

    float spamDelay;

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

            if (CurrentText.sentences[sentenceIndex].speakerSpriteId < CurrentText.sentences[sentenceIndex].speaker.sprites.Length)
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
        {
            gameObject.SetActive(false);
            return;
        }

        SentenceIndex = 0;

        bgm.clip = CurrentText.backgroundMusic;
        bgm.Stop();
        bgm.Play();
    }
    
    private void OnEnable()
    {
        if (CurrentText == null)
        {
            gameObject.SetActive(false);
            return;
        }

        SentenceIndex = 0;

        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
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

        bool spamKeyPressed = false;
        foreach (KeyCode key in SpamKeyBindings)
        {
            if (Input.GetKey(key)) spamKeyPressed = true;
        }

        if (spamDelay > 0f)
            spamDelay -= Time.unscaledDeltaTime;

        if (spamKeyPressed && !CurrentText.sentences[sentenceIndex].unskippable)
        {
            StopCoroutine(currentPlayingCoroutine);
            DialogueText.text = CurrentText.sentences[sentenceIndex].text;
            state = State.COMPLETED;
        }

        if (spamKeyPressed && spamDelay <= 0f && !CurrentText.sentences[sentenceIndex].unskippable)
        {
            spamDelay = SpamDelay;
            NextSentence();
            return;
        }

        if (state == State.COMPLETED && CurrentText.sentences[sentenceIndex].autoPlay) NextSentence();

        if (state == State.COMPLETED && keyPressed)
        {
            NextSentence();
        }
        else if (state == State.PLAYING && currentPlayingCoroutine != null && skipKeyPressed && !CurrentText.sentences[sentenceIndex].unskippable)
        {
            StopCoroutine(currentPlayingCoroutine);
            DialogueText.text = CurrentText.sentences[sentenceIndex].text;
            state = State.COMPLETED;
        }
    }

    public void NextSentence()
    {
        if (SentenceIndex >= CurrentText.sentences.Count - 1)
        {
            if (CurrentText.nextScene == null)
            {
                gameObject.SetActive(false);

                //bgm.Stop();

                Time.timeScale = 1f;

                OnDialogueEnd();

                if (CurrentText.Final)
                {
                    SceneManager.LoadScene(2);
                }

                CurrentText = null;
            }
            else
            {
                if (currentPlayingCoroutine != null)
                    StopCoroutine(currentPlayingCoroutine);

                CurrentText = CurrentText.nextScene;

                if (CurrentText.backgroundMusic != bgm.clip)
                {
                    bgm.clip = CurrentText.backgroundMusic;
                    bgm.Stop();
                    bgm.Play();
                }


                SentenceIndex = 0;
            }

            return;
        }

        SentenceIndex++;
    }

    private IEnumerator TypeText(string text, float delay, AudioClip voice)
    {
        if (currentVoiceline != null)
            Destroy(currentVoiceline.gameObject);

        currentVoiceline = AudioSystem.PlaySound(voice, cameraTrans.position, 1f, 128);

        DialogueText.text = ""; // Reset the dialogue box

        bg.sprite = CurrentText.background;

        state = State.PLAYING; // Set the play state of the dialogue

        int wordIndex = 0; // The position of the text character inside the string

        while (state != State.COMPLETED)
        {
            DialogueText.text += text[wordIndex];

            yield return new WaitForSecondsRealtime(delay);

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
        PLAYING, 
        COMPLETED
    }

    public event Action onDialogueEnd;
    public void OnDialogueEnd() { onDialogueEnd?.Invoke(); }
}
