using UnityEngine;

namespace Game
{
    public class RandomRotation : MonoBehaviour
    {
        public float Max;
        public float Min;

        public float Timer;

        float timer;

        private void Awake()
        {
            timer = Timer;
        }

        private void Update()
        {
            if (timer <= 0f)
            {
                Rotate();

                timer = Timer;
            }

            timer -= Time.deltaTime;
        }

        public void Rotate()
        {
            Quaternion q = transform.localRotation;

            Vector3 v = q.eulerAngles;

            v.z = Random.Range(Min, Max);

            q.eulerAngles = v;

            transform.localRotation = q;
        }
    }
}