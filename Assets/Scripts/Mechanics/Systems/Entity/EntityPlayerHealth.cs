using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entity
{
    public class EntityPlayerHealth : EntityAddon
    {
        Slider slider;

        public override void Init()
        {
            if (PlayerHealthSingleton.Singleton != null)
                slider = PlayerHealthSingleton.Singleton.slider;

            ent.onHealthChange += OnHealthChange;
        }

        private void OnHealthChange(int hp)
        {
            slider.value = Mathf.InverseLerp(0f, ent.entity.Health, hp);
        }
    }
}
