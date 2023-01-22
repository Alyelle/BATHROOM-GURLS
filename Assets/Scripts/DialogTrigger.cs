using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public TextEngine Dialogue;

    public void TriggerDialogue() // Run this from other scripts to trigger the dialogue
    {
        BottomBarController.Instance.CurrentText = Dialogue;

        BottomBarController.Instance.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }
}
