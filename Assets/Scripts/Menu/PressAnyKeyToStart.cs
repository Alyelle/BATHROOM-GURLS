using UnityEngine;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    [SerializeField]
    GameObject[] disable;
    [SerializeField]
    GameObject[] enable;

    private void Update()
    {
        if (Input.anyKeyDown) 
        { 
            anim.SetBool("Menu", true);

            foreach (GameObject go in disable)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in enable)
            {
                go.SetActive(true);
            }

            enabled = false;
        }
    }
}
