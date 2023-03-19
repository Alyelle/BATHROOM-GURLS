using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRandomizer : MonoBehaviour
{
    SpriteRenderer sp;

    [SerializeField]
    Sprite[] sprites;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();

        sp.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
