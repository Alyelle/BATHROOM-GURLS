using Game.Entity;
using System;

public static class GameEventSystem
{
    public static Action<EntityBase> onEntityDeath;
    public static void OnEntityDeath(EntityBase ent) { onEntityDeath?.Invoke(ent); }
}
