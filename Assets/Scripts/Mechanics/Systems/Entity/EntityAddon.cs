using UnityEngine;

namespace Game.Entity 
{
    public abstract class EntityAddon : MonoBehaviour
    {
        protected EntityBase ent;

        private void Awake()
        {
            ent = GetComponent<EntityBase>();
        }

        private void Start()
        {
            Init();
        }

        public abstract void Init();
    }
}