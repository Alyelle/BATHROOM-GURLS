using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dashSpeed = 1f;

    public int Rotation;

    Vector2 movement;

    ParticleSystem par;

    Animator anim;

    Rigidbody2D rb;

    bool lockVel;

    float dashBuffer;

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

        if (movement != Vector2.zero)
        {
            switch (movement)
            {
                case Vector2 v when v.Equals(Vector2.down):
                    Rotation = 0;
                    break;
                case Vector2 v when v.Equals(Vector2.up):
                    Rotation = 180;
                    break;
                case Vector2 v when v.Equals(Vector2.right):
                    Rotation = 90;
                    break;
                case Vector2 v when v.Equals(Vector2.left):
                    Rotation = 270;
                    break;
                case Vector2 v when v.Equals(new Vector2(1f, 1f)):
                    Rotation = 90;
                    break;
                case Vector2 v when v.Equals(new Vector2(-1f, 1f)):
                    Rotation = 270;
                    break;
                case Vector2 v when v.Equals(new Vector2(1f, -1f)):
                    Rotation = 90;
                    break;
                case Vector2 v when v.Equals(new Vector2(-1f, -1f)):
                    Rotation = 270;
                    break;
            }
        }

        movement.Normalize();

        if (dashBuffer > 0f)
        {
            dashBuffer -= Time.deltaTime;

            if (movement.magnitude != 0f)
            {
                Dash();
                dashBuffer = 0f;
            }
        }

        if (!lockVel)
        {
            rb.velocity = moveSpeed * movement;

            anim.SetInteger("X", Mathf.RoundToInt(movement.x));
            anim.SetInteger("Y", Mathf.RoundToInt(movement.y));

            if (Input.GetMouseButtonDown(1))
            {
                if (movement.magnitude != 0f)
                {
                    Dash();
                }
                else
                {
                    dashBuffer = 0.1f;
                }
            }
        }

        if (rb.velocity.magnitude <= moveSpeed)
            lockVel = false;
    }

    public void Dash()
    {
        lockVel = true;

        rb.velocity = dashSpeed * movement;
        if (par != null)
            par.Play();
    }
}
