using Game.Entity;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(DialogueTrigger))]
public class TriggerColliderDialogue : MonoBehaviour
{
    Collider2D col;

    DialogueTrigger dt;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        dt = GetComponent<DialogueTrigger>();

        if (col == null)
        {
            Destroy(gameObject);
            return;
        }

        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out EntityBase ent))
            return;

        if (ent == PlayerManager.Instance.WorldPlayer)
        {
            dt.TriggerDialogue();

            Destroy(gameObject);
        }
    }
}
