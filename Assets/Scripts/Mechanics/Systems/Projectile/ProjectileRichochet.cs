using Game.Entity;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileRichochet : ProjectileAddon
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out EntityBase _) || collision.transform.TryGetComponent(out ProjectileBase _))
                return;

            RaycastHit2D[] r = Physics2D.RaycastAll(transform.position, transform.up, proj.proj.Speed);
            Debug.DrawRay(transform.position, transform.up, Color.white, proj.proj.Speed);
            if (r.Length > 0)
                foreach (RaycastHit2D rs in r)
                {
                    if (rs.transform.TryGetComponent(out EntityBase _) || rs.transform.TryGetComponent(out ProjectileBase _))
                        continue;

                    Vector2 vec = Vector2.Reflect(transform.up, rs.normal);
                    float rot = Mathf.Atan2(-vec.x, vec.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
                    break;
                }
        }
    }
}