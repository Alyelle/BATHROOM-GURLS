namespace Game.Entity
{
    public class EntityIFrameFlicker : EntityAddon
    {
        public float FlickerRate = 15f;

        SpriteFlicker flicker;

        public override void Init()
        {
            flicker = GetComponentInChildren<SpriteFlicker>();

            if (flicker == null)
            {
                enabled = false;
                return;
            }

            ent.onIFrameStart += IFrameStart;
            ent.onIFrameEnd += IFrameEnd;
        }

        public void IFrameStart()
        {
            flicker.FlickerRate = FlickerRate;
        }

        public void IFrameEnd()
        {
            flicker.FlickerRate = 0f;
        }
    }
}
