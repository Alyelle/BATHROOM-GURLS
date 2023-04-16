using UnityEngine;

[RequireComponent(typeof(SpawnerBase))]
public class SpawnerAddon : MonoBehaviour
{
    [HideInInspector]
    public SpawnerBase spawner;

    private void Awake()
    {
        spawner = GetComponent<SpawnerBase>();
    }
}
