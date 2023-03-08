using Game.Entity;
using UnityEngine;

namespace Game.Projectile
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class ProjectileBase : MonoBehaviour
    {
        public ProjectileScriptable proj;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

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

            Destroy(gameObject);
        }
    }
}
