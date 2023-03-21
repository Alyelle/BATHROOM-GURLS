using System;
using UnityEngine;

namespace Game.Entity
{
    public class EntityBase : MonoBehaviour
    {
        public EntityScriptable entity;

        [HideInInspector]
        public int currentHealth
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;

                OnHealthChange(value);
            }
        }

        int hp;

        bool invulnerable;

        float iFrameTimer;

        private void Awake()
        {
            currentHealth = entity.Health;
            iFrameTimer = 0f;
        }

        private void Update()
        {
            if (entity.IFrameTime > 0f && iFrameTimer > 0f)
            {
                iFrameTimer -= Time.deltaTime;

                if (iFrameTimer <= 0f)
                {
                    invulnerable = false;
                    iFrameTimer = 0f;

                    OnIFrameEnd();
                }
            }
        }

        public void TakeDamage(int dmg)
        {
            if (invulnerable)
                return;

            currentHealth -= dmg;

            OnDamage(dmg);

            if (currentHealth <= 0)
                Death();

            if (entity.IFrameTime > 0f)
            {
                invulnerable = true;
                iFrameTimer = entity.IFrameTime;

                OnIFrameStart();
            }
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

        public event Action<int> onHealthChange;
        public void OnHealthChange(int cHp) { onHealthChange?.Invoke(cHp); }

        public event Action onDeath;
        public void OnDeath() { onDeath?.Invoke(); }

        public event Action onIFrameStart;
        public void OnIFrameStart() { onIFrameStart?.Invoke(); }

        public event Action onIFrameEnd;
        public void OnIFrameEnd() { onIFrameEnd?.Invoke(); }
    }
}
