using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityGlide : MonoBehaviour
{
    public float Speed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PlayerManager.Instance.WorldPlayer != null) {
            rb.MovePosition(Vector2.MoveTowards(transform.position, PlayerManager.Instance.WorldPlayer.transform.position, Speed * Time.deltaTime));
        }
    }
}
