using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform PlayerRespawnPoint;

    public SpawnerTrigger[] TriggersToReset;

    public void Respawn()
    {
        foreach (SpawnerTrigger tr in TriggersToReset)
            tr.enabled = true;
    }
}
