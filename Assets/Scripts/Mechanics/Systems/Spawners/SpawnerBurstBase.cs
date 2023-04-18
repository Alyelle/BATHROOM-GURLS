using System.Collections;
using UnityEngine;

namespace Game.Spawner
{
    public class SpawnerBurstBase : SpawnerBase
    {
        public int Burst = 3;

        public float BurstDelay = 0.2f;

        public override GameObject Spawn()
        {
            StartCoroutine(BurstSpawn());
            return spawnee;
        }

        public IEnumerator BurstSpawn()
        {
            for (int i = 0; i < Burst; i++)
            {
                base.Spawn();
                yield return new WaitForSeconds(BurstDelay);
            }
        }
    }
}
