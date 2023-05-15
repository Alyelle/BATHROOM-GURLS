using UnityEngine;
using System.Collections;

public class AltTab : MonoBehaviour
{
    void PauseOnTab() {
        Application.runInBackground = true;
    }
}
