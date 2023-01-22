using UnityEngine;
using UnityEngine.Audio;

namespace Game.Utils
{
    public static class AudioSystem
    {
        #region Methods

        public static void PlaySound(AudioClip _sound, Vector2 _position, float _volume, int _priority)
        {
            GameObject soundObj = new GameObject("Sound", typeof(AudioSource), typeof(DestroyAfter));
            AudioSource au = soundObj.GetComponent<AudioSource>();
            DestroyAfter des = soundObj.GetComponent<DestroyAfter>();
            soundObj.transform.position = _position;
            au.playOnAwake = false;
            au.clip = _sound;
            au.volume = _volume;
            au.pitch = Time.timeScale;
            au.priority = _priority;
            des.Timer = _sound.length + 0.1f;
            au.minDistance = 1.5f;

            au.Play();
        }

        #endregion
    }
}

