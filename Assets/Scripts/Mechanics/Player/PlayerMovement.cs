using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dashSpeed = 1f;

    Vector2 movement;

    ParticleSystem par;

    Animator anim;

    Rigidbody2D rb;

    bool lockVel;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        par = GetComponentInChildren<ParticleSystem>();

        lockVel = false;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        if (!lockVel)
        {
            rb.velocity = moveSpeed * movement;

            anim.SetInteger("X", Mathf.RoundToInt(movement.x));
            anim.SetInteger("Y", Mathf.RoundToInt(movement.y));

            if (Input.GetMouseButtonDown(1) && movement.magnitude != 0f)
            {
                lockVel = true;

                rb.velocity = dashSpeed * movement;
                if (par != null)
                    par.Play();
            }
        }

        if (rb.velocity.magnitude <= moveSpeed)
            lockVel = false;
    }
}
