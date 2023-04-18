using Game.Entity;
using Game.Utils;
using System;
using UnityEngine;

namespace Game.Blasters
{
    public class BlasterBase : MonoBehaviour
    {
        #region Variables

        public float EntryTime;
        public float Duration;

        public AudioClip SpawnSound;
        public float Volume = 1f;
        public int Priority = 128;

        [HideInInspector]
        public Vector3 targetPos;

        [HideInInspector]
        public float targetRot;

        Vector3 velPos;
        float velRot;

        float timer;

        bool exited;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            timer = Duration + EntryTime * 2f;
        }

        private void Start()
        {
            if (SpawnSound != null)
                AudioSystem.PlaySound(SpawnSound, transform.position, Volume, Priority);
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
                TriggerExit();

            transform.SetPositionAndRotation(
                Vector3.SmoothDamp(transform.position, targetPos, ref velPos, EntryTime),
                Quaternion.Euler(new Vector3(0f, 0f, Mathf.SmoothDamp(transform.rotation.eulerAngles.z, targetRot, ref velRot, EntryTime)))
                );
        }

        #endregion

        #region Methods

        public void TriggerExit()
        {
            if (!exited)
            {
                exited = true;
                timer = EntryTime * 2f;
                targetPos = transform.position + transform.up * -14f;
            }

            if (timer <= 0f)
                Destroy(gameObject);

            OnExit();
        }

        #endregion

        #region Events

        public event Action onExit;
        public void OnExit() { onExit?.Invoke(); }

        #endregion
    }
}