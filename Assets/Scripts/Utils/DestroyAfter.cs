using UnityEngine;

namespace Game
{
    public class DestroyAfter : MonoBehaviour
    {
        public float Timer;

        private void Start()
        {
            Destroy(gameObject, Timer);
        }
    }
}
