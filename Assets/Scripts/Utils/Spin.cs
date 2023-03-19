using UnityEngine;

namespace Game
{
    public class Spin : MonoBehaviour
    {
        public float Speed;

        private void Update() => transform.Rotate(0f, 0f, Speed * Time.deltaTime, Space.Self);
    }
}