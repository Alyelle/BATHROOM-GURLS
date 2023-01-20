using System.Collections;
using TMPro;
using UnityEngine;

//typewriter effect component of the text engine

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    public TextEngine currentScene;
    private State state = State.COMPLETED;
    private enum State
    {
        PLAYING, COMPLETED
    }

    void Update()
    {
        if (state == State.COMPLETED && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        }
    }

    private IEnumerator TypeText(string text)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
