using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject deathMenu;
    [SerializeField]
    GameObject dialogue;

    private void Update()
    {
        if (deathMenu.activeSelf || dialogue.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        if (pauseMenu.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
