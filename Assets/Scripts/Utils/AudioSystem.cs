using UnityEngine;

namespace Game.Utils
{
    public static class AudioSystem
    {
        #region Methods

        public static AudioSource PlaySound(AudioClip _sound, Vector2 _position, float _volume, int _priority)
        {
            if (_sound == null)
                return null;

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

            return au;
        }

        #endregion
    }
}

