using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSingleton : MonoBehaviour
{
    public static PlayerHealthSingleton Singleton;

    [HideInInspector]
    public Slider slider;

    private void Awake()
    {
        if (Singleton != null) Destroy(gameObject);
        else Singleton = this;

        slider = GetComponent<Slider>();
    }
}
