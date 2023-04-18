using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    public GameObject spawnee;

    public float Timer;

    public bool Once = true;

    float timer;

    private void Awake()
    {
        if (spawnee == null)
            enabled = false;

        timer = Timer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = Timer;

            Spawn();
        }
    }

    public virtual GameObject Spawn()
    {
        GameObject go = Instantiate(spawnee, transform.position, transform.rotation);

        if (Once)
            enabled = false;

        return go;
    }
}
