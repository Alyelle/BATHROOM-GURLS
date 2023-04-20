using Game.Entity;
using System;

public static class GameEventSystem
{
    public static Action<EntityBase> onEntityDeath;
    public static void OnEntityDeath(EntityBase ent) { onEntityDeath?.Invoke(ent); }

    public static Action<EntityBase> onPlayerDeath;
    public static void OnPlayerDeath(EntityBase ent) { onPlayerDeath?.Invoke(ent); }
}
