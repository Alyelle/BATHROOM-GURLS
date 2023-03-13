using UnityEngine;

namespace Game.Projectile
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
    public class ProjectileScriptable : ScriptableObject
    {
        public int Damage;

        public float Speed;

        public Factions Faction;

        public GameObject[] HitObjects;
    }
}

public enum Factions
{
    Player,
    Enemy
}
