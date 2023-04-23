using UnityEngine;
using UnityEngine.UI;

public class BossbarSingleton : MonoBehaviour
{
    public static BossbarSingleton Singleton;

    [HideInInspector]
    public Slider slider;

    private void Awake()
    {
        if (Singleton != null) Destroy(gameObject);
        else Singleton = this;

        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
