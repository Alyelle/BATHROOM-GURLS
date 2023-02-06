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
    public Image bg;

    public TextEngine CurrentText;

    Coroutine currentPlayingCoroutine;
    
    // Coroutine fadingCoroutine;
    
    // Color defaultSpriteColor;

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
    }

    private void OnEnable()
    {
        if (CurrentText == null)
        {
            gameObject.SetActive(false);
            return;
        }

        SentenceIndex = 0;

        //Time.timeScale = 0f;
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

        if (state == State.COMPLETED && CurrentText.sentences[sentenceIndex].autoPlay) NextSentence();

        if (/*fadingCoroutine == null && */state == State.COMPLETED && (Input.GetMouseButtonDown(0) || keyPressed))
        {
            NextSentence();
        }
        else if (state == State.PLAYING && currentPlayingCoroutine != null && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || skipKeyPressed) && !CurrentText.sentences[sentenceIndex].unskippable)
        {
            StopCoroutine(currentPlayingCoroutine);
            //if(fadingCoroutine != null)
            //StopCoroutine(fadingCoroutine);
            /*
            speakerSprite.color = defaultSpriteColor;
            NameText.color = CurrentText.sentences[sentenceIndex].speaker.textColor;
            DialogueText.color = new Color(DialogueText.color.r, DialogueText.color.g, DialogueText.color.b, 1f);
            */
            DialogueText.text = CurrentText.sentences[sentenceIndex].text;
            state = State.COMPLETED;
            // fadingCoroutine = null;
        }
    }

    public void NextSentence()
    {
        if (SentenceIndex >= CurrentText.sentences.Count - 1)
        {
            if (CurrentText.nextScene == null)
            {
                CurrentText = null;

                gameObject.SetActive(false); // Exit animation

                Time.timeScale = 1f;

                OnDialogueEnd();
            }
            else
            {
                CurrentText = CurrentText.nextScene;

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

            yield return new WaitForSeconds(delay);

            wordIndex++;

            if (wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
    /*
    private IEnumerator FadeSpeaker(int indx)
    {
        defaultSpriteColor = speakerSprite.color;
        
        for(float alpha = 1f; alpha >= 0f; alpha -= Time.deltaTime * 8f)
        {
            NameText.color = new Color(NameText.color.r, NameText.color.g, NameText.color.b, alpha);
            DialogueText.color = new Color(DialogueText.color.r, DialogueText.color.g, DialogueText.color.b, alpha);
            speakerSprite.color = new Color(defaultSpriteColor.r * alpha, defaultSpriteColor.g * alpha, defaultSpriteColor.b * alpha, 1f);
            yield return null;
        }
        
        NameText.text = CurrentText.sentences[indx].speaker.speakerName;
        DialogueText.text = "";
        DialogueText.color = new Color(DialogueText.color.r, DialogueText.color.g, DialogueText.color.b, 1f);
        speakerSprite.sprite = CurrentText.sentences[indx].speaker.sprites[CurrentText.sentences[indx].speakerSpriteId];
        Color newColor = CurrentText.sentences[indx].speaker.textColor;
        NameText.color = new Color(newColor.r, newColor.g, newColor.b, 0f);
        
        
        currentPlayingCoroutine = StartCoroutine(TypeText(CurrentText.sentences[indx].text, CurrentText.sentences[indx].delay, CurrentText.sentences[indx].voiceLine));
        
        
        for(float alpha = 0f; alpha < 1f; alpha += Time.deltaTime * 8f)
        {
            NameText.color = new Color(NameText.color.r, NameText.color.g, NameText.color.b, alpha);
            speakerSprite.color = new Color(defaultSpriteColor.r * alpha, defaultSpriteColor.g * alpha, defaultSpriteColor.b * alpha, 1f);
            yield return null;
        }
        
        
        fadingCoroutine = null;
        yield break;
    }
    */

    private enum State
    {
        PLAYING, COMPLETED
    }

    public event Action onDialogueEnd;
    public void OnDialogueEnd() { onDialogueEnd?.Invoke(); }
}
