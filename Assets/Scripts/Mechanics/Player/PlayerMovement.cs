using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    Vector2 movement;

    Animator anim;

    Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = moveSpeed * movement;

        anim.SetInteger("X", Mathf.RoundToInt(movement.x));
        anim.SetInteger("Y", Mathf.RoundToInt(movement.y));
    }
}
