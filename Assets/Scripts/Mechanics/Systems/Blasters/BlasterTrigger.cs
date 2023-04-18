using UnityEngine;

namespace Game.Blasters
{
    public class BlasterTrigger : MonoBehaviour, ITargeting
    {
        #region Variables

        public BlasterBase blaster;

        [HideInInspector]
        public Transform target;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            if (target != null)
                transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f);

            BlasterBase b = Instantiate(blaster, transform.position + transform.up * -14f, Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0f, 0f, 180f)));

            b.targetPos = transform.position;
            b.targetRot = transform.rotation.eulerAngles.z;

            Destroy(gameObject, 1f);
        }

        #endregion

        #region Methods

        public void SpawnWithTarget(Transform t)
        {
            target = t;
        }

        #endregion
    }
}