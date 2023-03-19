using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Game.Entity
{
    public class EntityScreenShake : EntityAddon
    {
        public float Amplitude = 2f;
        public float Frequency = 3f;
        public float Duration = 0.5f;

        CinemachineBasicMultiChannelPerlin noise;

        float timer;

        public override void Init()
        {
            if (CameraSingleton.Singleton == null)
            {
                enabled = false;
                return;
            }

            noise = CameraSingleton.Singleton.vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            ent.onDamage += Damage;
        }

        private void Update()
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;

                noise.m_AmplitudeGain = Amplitude;
                noise.m_FrequencyGain = Frequency;
            }
            else
            {
                timer = 0f;

                noise.m_AmplitudeGain = 0f;
                noise.m_FrequencyGain = 0f;
            }
        }

        public void Damage(int dmg)
        {
            timer = Duration;
        }
    }
}
