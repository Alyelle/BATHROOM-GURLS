using Cinemachine;
using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    public static CameraSingleton Singleton;

    [HideInInspector]
    public CinemachineVirtualCamera vCam;

    private void Awake()
    {
        if (Singleton != null) Destroy(gameObject);
        else Singleton = this;

        vCam = GetComponent<CinemachineVirtualCamera>();
    }
}
