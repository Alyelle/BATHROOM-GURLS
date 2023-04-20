using Game.Entity;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleporter : MonoBehaviour
{
    public EntityScriptable RequiredEntity;

    public Teleporter Destination;

    Collider2D col;

    bool delay;

    private void Awake()
    {
        col = GetComponent<Collider2D>();

        if (col == null)
            return;

        col.isTrigger = true;

        if (RequiredEntity != null)
        {
            GameEventSystem.onEntityDeath += CheckEntity;
            col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (delay)
        {
            delay = false;
            return;
        }

        if (!collision.gameObject.TryGetComponent(out EntityBase ent))
            return;

        if (ent == PlayerManager.Instance.WorldPlayer)
        {
            Destination.DestinationCallback();
            ent.gameObject.transform.position = Destination.transform.position;
        }
    }

    public void DestinationCallback()
    {
        if (col == null)
            return;

        delay = true;
    }

    public void CheckEntity(EntityBase ent)
    {
        if (col == null)
            return;

        if (ent.entity == RequiredEntity)
            col.enabled = true;
    }
}
