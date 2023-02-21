using System;
using UnityEngine;

namespace Game.Entity
{
    public class EntityBase : MonoBehaviour
    {
        public EntityScriptable entity;

        [HideInInspector]
        public int currentHealth;

        private void Awake()
        {
            currentHealth = entity.Health;
        }

        public void TakeDamage(int dmg)
        {
            currentHealth -= dmg;

            OnDamage(dmg);

            if (currentHealth <= 0)
                Death();
        }

        public void Death()
        {
            if (entity.DeathObjects.Length > 0)
                foreach (GameObject go in entity.DeathObjects)
                    Instantiate(go, transform.position, transform.rotation);

            OnDeath();

            Destroy(gameObject);
        }

        public event Action<int> onDamage;
        public void OnDamage(int dmg) { onDamage?.Invoke(dmg); }

        public event Action onDeath;
        public void OnDeath() { onDeath?.Invoke(); }
    }
}
