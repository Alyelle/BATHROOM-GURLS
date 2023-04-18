using Game.Blasters;

namespace Game.Spawner
{
    public class SpawnerBlaster : SpawnerAddon
    {
        BlasterBase blaster;

        private void Start()
        {
            blaster = GetComponentInParent<BlasterBase>();

            if (blaster != null)
            {
                blaster.onExit += OnExit;

                spawner.enabled = false;
            }
            else
            {
                enabled = false;
            }
        }

        private void OnDestroy()
        {
            if (blaster != null)
            {
                blaster.onExit -= OnExit;
            }
        }

        public void OnExit()
        {
            spawner.enabled = true;
            spawner.Once = true;
        }
    }
}