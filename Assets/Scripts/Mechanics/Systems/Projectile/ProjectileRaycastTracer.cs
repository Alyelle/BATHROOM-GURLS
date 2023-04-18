using Game.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Projectile
{
    [RequireComponent(typeof(LineRenderer))]
    public class ProjectileRaycastTracer : ProjectileAddon
    {
        public float FadeoutTime;

        float timer;

        LineRenderer[] lns;

        private void Start()
        {
            lns = GetComponentsInChildren<LineRenderer>();

            proj.onHit += Hit;

            foreach (LineRenderer ln in lns)
            {
                for (int i = 0; i < ln.positionCount; i++)
                {
                    ln.SetPosition(i, Vector2.Lerp(transform.position, transform.position + transform.up * 40f, i / (ln.positionCount - 1f)));
                }   
            }

            timer = FadeoutTime;
        }

        private void Update()
        {
            foreach (LineRenderer ln in lns)
            {
                if (timer >= 0f)
                {
                    Color temp = ln.startColor;
                    temp.a = Mathf.InverseLerp(0f, FadeoutTime, timer);

                    ln.startColor = temp;
                    ln.endColor = temp;
                }
            }

            timer -= Time.deltaTime;

            if (timer < 0f)
                Destroy(gameObject);
        }

        public void Hit(Vector2 pos)
        {
            if (proj is ProjectileRaycastBase p)
            {
                if (!p.Pierce)
                {
                    foreach (LineRenderer ln in lns)
                        for (int i = 0; i < ln.positionCount; i++)
                        {
                            ln.SetPosition(i, Vector2.Lerp(transform.position, pos, i / (ln.positionCount - 1f)));
                        }
                }
            }
        }
    }
}