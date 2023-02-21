using UnityEngine;

namespace Game.Entity
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
    public class EntityScriptable : ScriptableObject
    {
        public int Health;

        public GameObject[] DeathObjects;
    }
}
