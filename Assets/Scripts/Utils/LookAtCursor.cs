using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    Camera mainCam;

    Vector2 mousePos;

    private void Awake()
    {
        mainCam = Camera.main;

        Look();
    }

    private void Update()
    {
        Look();
    }

    public void Look()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f);
    }
}
