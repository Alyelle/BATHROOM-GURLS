using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGlide : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        if (PlayerManager.Instance.WorldPlayer != null) {
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.Instance.WorldPlayer.transform.position, Speed * Time.deltaTime);
        }
    }
}
