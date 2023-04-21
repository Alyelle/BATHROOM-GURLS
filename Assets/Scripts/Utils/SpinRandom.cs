using UnityEngine;

namespace Game
{
    public class SpinRandom : MonoBehaviour
    {
        public float SpeedMin;
        public float SpeedMax;

        //private void Update() => transform.Rotate(0f, 0f, Random.Range(SpeedMin, SpeedMax) * Time.deltaTime, Space.Self);
        private void Update() => transform.Rotate(0f, 0f, Random.Range(SpeedMin, SpeedMax) * Time.deltaTime, Space.Self);
    }
}