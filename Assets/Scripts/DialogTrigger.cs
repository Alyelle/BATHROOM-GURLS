using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public TextEngine Dialogue;

    private void Start() // Subscribe to events
    {
        BottomBarController.Instance.onDialogueEnd += OnDialogueEnd;
    }

    private void OnDestroy() // Unsubscribe to events
    {
        BottomBarController.Instance.onDialogueEnd -= OnDialogueEnd;
    }

    public void TriggerDialogue() // Run this from other scripts to trigger the dialogue
    {
        BottomBarController.Instance.CurrentText = Dialogue;

        BottomBarController.Instance.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void OnDialogueEnd() // Callback from the BottomBarController after the dialogue ends
    {
        BottomBarController.Instance.CurrentText = null;

        BottomBarController.Instance.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}
