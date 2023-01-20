using System.Collections.Generic;
using UnityEngine;

//text engine that generates dialogue + voice lines
[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Story Scene")]
[System.Serializable]
public class TextEngine : ScriptableObject
{
    public List<Sentence> sentences;
    public TextEngine nextScene;

    [System.Serializable]
    
    public struct Sentence
    {
        [TextArea(5, 50)]
        public string text;     
        public Speaker speaker;
        public Sprite speakerSprite;
        //insert voice lines (to be manually added)
        public AudioClip voiceLine;
    }
}
