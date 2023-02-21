using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    Vector2 movement;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        transform.Translate(moveSpeed * Time.deltaTime * movement);

        anim.SetInteger("X", Mathf.RoundToInt(movement.x));
        anim.SetInteger("Y", Mathf.RoundToInt(movement.y));
    }
}
