using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Entity
{
    public class EntityBase : MonoBehaviour
    {
        public static bool Joe;

        public EntityScriptable entity;

        public bool IsJoe;

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

        public bool IsPlayer;

        int hp;

        bool invulnerable;

        [HideInInspector]
        public float iFrameTimer;

        private void Awake()
        {
            currentHealth = entity.Health;
            iFrameTimer = 0f;

            if (IsPlayer)
                GameEventSystem.onEntityDeath += PlayerHeal;

            if (IsJoe)
                Joe = true;
        }

        private void OnDestroy()
        {
            if (IsPlayer)
                GameEventSystem.onEntityDeath -= PlayerHeal;
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

        public void PlayerHeal(EntityBase ent)
        {
            if (!IsPlayer)
                return;

            currentHealth = entity.Health;
        }

        public void TriggerIFrame(float timer)
        {
            if (entity.IFrameTime > 0f && iFrameTimer <= 0f)
            {
                invulnerable = true;
                iFrameTimer = timer;

                OnIFrameStart();
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

            TriggerIFrame(entity.IFrameTime);
        }

        public void Death()
        {
            if (entity.DeathObjects.Length > 0)
                foreach (GameObject go in entity.DeathObjects)
                    Instantiate(go, transform.position, transform.rotation);

            GameEventSystem.OnEntityDeath(this);

            OnDeath();

            if (IsPlayer)
            {
                if (!Joe)
                    GameEventSystem.OnPlayerDeath(this);
                else
                    GameEventSystem.OnJoe();

                return;
            }

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
