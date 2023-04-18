using Game.Entity;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileRaycastBase : ProjectileBase
    {
        public float Width;

        public bool Pierce;

        private void Start()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Width, transform.up);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.TryGetComponent(out EntityBase ent))
                {
                    if (ent.entity.Faction != proj.Faction)
                    {
                        ent.TakeDamage(proj.Damage);

                        if (!Pierce)
                            break;
                    }
                }

                foreach (GameObject go in proj.HitObjects)
                {
                    Instantiate(go, hit.point, transform.rotation);
                }

                OnHit(hit.point);
            }
        }

        private void Update() { }
    }
}