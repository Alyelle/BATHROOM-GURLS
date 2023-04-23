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

            slider.gameObject.SetActive(true);

            slider.value = 1;

            ent.onHealthChange += OnHealthChange;
            ent.onDeath += Ent_onDeath;
        }

        private void Ent_onDeath()
        {
            slider.gameObject.SetActive(false);
        }

        private void OnHealthChange(int hp)
        {
            slider.value = Mathf.InverseLerp(0f, ent.entity.Health, hp);
        }
    }
}
