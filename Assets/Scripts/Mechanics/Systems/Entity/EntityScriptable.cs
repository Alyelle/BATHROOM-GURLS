using UnityEngine;

namespace Game.Entity
{
    [CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
    public class EntityScriptable : ScriptableObject
    {
        public int Health;

        public float IFrameTime;

        public GameObject[] DeathObjects;

        public Factions Faction;
    }
}
