using Game.Entity;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageZone : MonoBehaviour, IFaction
{
    public int Damage;

    Factions Faction;

    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();

        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out EntityBase ent))
            return;

        if (ent.entity.Faction != Faction)
            ent.TakeDamage(Damage);
    }

    public void SpawnWithFaction(Factions fac)
    {
        Faction = fac;
    }
}
