using UnityEngine;

public class MovementRotator : MonoBehaviour
{
    PlayerMovement PM;

    private void Awake()
    {
        PM = GetComponentInParent<PlayerMovement>();

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, PM.Rotation));
    }
}
