using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entity
{
    public class EntityBossbar : EntityAddon
    {
        Slider slider;

        public override void Init()
        {
            if (BossbarSingleton.Singleton != null)
                slider = BossbarSingleton.Singleton.slider;

            ent.onHealthChange += OnHealthChange;
        }

        private void OnHealthChange(int hp)
        {
            slider.value = Mathf.InverseLerp(0f, ent.entity.Health, hp);
        }
    }
}
