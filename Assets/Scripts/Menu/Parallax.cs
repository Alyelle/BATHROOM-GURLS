using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float Multiplier = 1f;

    Camera cam;

    Vector2 mousePos;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        transform.position = -mousePos * Multiplier;
    }
}
