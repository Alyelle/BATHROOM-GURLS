using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour

    //new game > prologue scene
{
    [SerializeField] private string gameStart = "prologue";
    public void NewGameButton()
    {
        SceneManager.LoadScene(gameStart);
    }
}
