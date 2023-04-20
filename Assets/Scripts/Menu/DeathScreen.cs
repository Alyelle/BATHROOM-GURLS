using Game.Entity;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreen;

    private void Awake()
    {
        GameEventSystem.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        GameEventSystem.onPlayerDeath -= OnPlayerDeath;
    }

    public void OnPlayerDeath(EntityBase ent)
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
