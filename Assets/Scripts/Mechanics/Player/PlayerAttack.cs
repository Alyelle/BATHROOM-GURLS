using Game.Entity;
using UnityEngine;

[RequireComponent(typeof(EntityBase))]
public class PlayerAttack : MonoBehaviour
{
    public GameObject attackObject;

    public float attackDelay;

    float timer;

    EntityBase owner;

    private void Awake()
    {
        owner = GetComponent<EntityBase>();
        timer = attackDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (timer > 0)
            return;

        timer = attackDelay;

        GameObject go = Instantiate(attackObject, transform);

        if (owner != null && go.TryGetComponent(out IFaction fac))
        {
            fac.SpawnWithFaction(owner.entity.Faction);
        }
    }
}
