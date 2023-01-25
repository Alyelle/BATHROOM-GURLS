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
    
    Coroutine fadingCoroutine;
    
    Color defaultSpriteColor;

    AudioSource currentVoiceline;

    Transform cameraTrans;

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
            
            fadingCoroutine = StartCoroutine(FadeSpeaker(sentenceIndex));
        }
    }

    int sentenceIndex = 0;

    State state = State.COMPLETED;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        cameraTrans = Camera.main.transform;
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

        if (fadingCoroutine == null && state == State.COMPLETED && (Input.GetMouseButtonDown(0) || keyPressed))
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
            if(fadingCoroutine != null)
                StopCoroutine(fadingCoroutine);
            speakerSprite.color = defaultSpriteColor;
            NameText.color = CurrentText.sentences[sentenceIndex].speaker.textColor;
            DialogueText.color = new Color(DialogueText.color.r, DialogueText.color.g, DialogueText.color.b, 1f);
            DialogueText.text = CurrentText.sentences[sentenceIndex].text;
            state = State.COMPLETED;
            fadingCoroutine = null;
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

    private enum State
    {
        PLAYING, COMPLETED
    }

    public event Action onDialogueEnd;
    public void OnDialogueEnd() { if (onDialogueEnd != null) onDialogueEnd(); }
}
