using UnityEngine;

namespace Game.AI
{
    public class AttackPattern : MonoBehaviour
    {
        #region Variables

        public Attack[] Attacks;

        float currentTimer;

        int cur;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            ChangeAttack(0);
        }

        private void Update()
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer < 0f)
                NextAttack();
        }

        #endregion

        #region Methods

        public void ChangeAttack(int index)
        {
            foreach (Attack atk in Attacks)
                atk.Stop();

            currentTimer = Attacks[index].Timer;
            Attacks[index].Start();
            cur = index;
        }

        public void NextAttack()
        {
            if (cur == Attacks.Length - 1)
                ChangeAttack(0);
            else
                ChangeAttack(cur + 1);
        }

        #endregion

        #region Custom Classes

        [System.Serializable]
        public class Attack
        {
            public GameObject Turret;

            public float Timer = 3f;

            public void Start()
            {
                Turret.SetActive(true);
            }

            public void Stop()
            {
                Turret.SetActive(false);
            }
        }

        #endregion
    }
}