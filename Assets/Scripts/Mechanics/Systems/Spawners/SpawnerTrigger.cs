using Game.Entity;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpawnerTrigger : SpawnerAddon
{
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();

        col.isTrigger = true;

        spawner.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out EntityBase ent))
            return;

        if (ent == PlayerManager.Instance.WorldPlayer)
        {
            spawner.enabled = true;

            spawner.Spawn();
        }
    }
}
