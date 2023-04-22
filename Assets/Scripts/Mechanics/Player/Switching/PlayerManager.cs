using Game.Entity;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public EntityBase WorldPlayer;

    public EntityBase[] Players;

    int CurrentPlayerIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && CurrentPlayerIndex != 0)
        {
            SwitchPlayer(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && CurrentPlayerIndex != 1)
        {
            SwitchPlayer(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && CurrentPlayerIndex != 2)
        {
            SwitchPlayer(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && CurrentPlayerIndex != 3)
        {
            SwitchPlayer(3);
        }
    }

    public void SwitchPlayer(int i)
    {
        EntityBase ent = Instantiate(Players[i], WorldPlayer.transform.position, WorldPlayer.transform.rotation);

        CurrentPlayerIndex = i;

        ent.currentHealth = WorldPlayer.currentHealth;

        float temp = WorldPlayer.iFrameTimer;

        if (WorldPlayer.iFrameTimer > 0f) StartCoroutine(TriggerIFrame(ent, temp));

        Destroy(WorldPlayer.gameObject);

        WorldPlayer = ent;
    }

    IEnumerator TriggerIFrame(EntityBase ent, float f)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        ent.TriggerIFrame(f);
    }
}
