using System.Collections.Generic;
using UnityEngine;

//text engine that generates dialogue + voice lines
[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Story Scene")]
public class TextEngine : ScriptableObject
{
    public List<Sentence> sentences;
    public TextEngine nextScene;

    [System.Serializable]
    
    public struct Sentence
    {
        [TextArea(5, 50)]
        public string text;
        public string animationName;

        public bool unskippable;
        public bool autoPlay;

        public float delay;

        public int speakerSpriteId;

        public Sprite background;

        public Speaker speaker;
        
        public AudioClip voiceLine; // insert voice lines (to be manually added)
    }
}
