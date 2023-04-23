using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
public class Endgame : MonoBehaviour
{
    DialogueTrigger dt;

    private void Awake()
    {
        dt = GetComponent<DialogueTrigger>();

        GameEventSystem.onJoe += EndGame;
    }

    public void EndGame()
    {
        dt.TriggerDialogue();
    }
}
