using UnityEngine;

namespace Game
{
    public class SpinRandom : MonoBehaviour
    {
        public int SpeedMin;
        public int SpeedMax;

        private void Update() => transform.Rotate(0f, 0f, Random.Range(SpeedMin, SpeedMax) * Time.deltaTime, Space.Self);
    }
}