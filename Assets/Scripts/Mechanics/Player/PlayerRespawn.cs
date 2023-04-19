using Game.Entity;

public class PlayerRespawn : EntityAddon
{
    public override void Init()
    {
        ent.onDeath += Ent_onDeath;
    }

    private void Ent_onDeath()
    {
        transform.position = LevelManager.Instance.currentLevel.PlayerRespawnPoint.position;
        ent.currentHealth = ent.entity.Health;

        GameEventSystem.OnRespawn(ent);

        LevelManager.Instance.currentLevel.Respawn();
    }
}
