using UnityEngine;

namespace Game.Projectile 
{
    [RequireComponent(typeof(ProjectileBase))]
    public class ProjectileAddon : MonoBehaviour
    {
        [HideInInspector]
        public ProjectileBase proj;

        private void Awake()
        {
            proj = GetComponent<ProjectileBase>();
        }
    }
}
