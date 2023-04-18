using Game.Entity;
using System;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        public ProjectileScriptable proj;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.gravityScale = 0f;
        }

        private void Update()
        {
            rb.velocity = proj.Speed * transform.up;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out EntityBase _ent))
            {
                if (proj.Faction != _ent.entity.Faction)
                {
                    _ent.TakeDamage(proj.Damage);
                }
            }

            foreach (GameObject hit in proj.HitObjects)
            {
                Instantiate(hit, transform.position, transform.rotation);
            }

            OnHit(transform.position);

            Destroy(gameObject);
        }

        public event Action<Vector2> onHit;
        public void OnHit(Vector2 vec) { onHit?.Invoke(vec); }
    }
}
