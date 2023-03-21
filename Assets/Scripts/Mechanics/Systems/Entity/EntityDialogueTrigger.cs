using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entity
{
    [RequireComponent(typeof(DialogueTrigger))]
    public class EntityDialogueTrigger : EntityAddon
    {
        DialogueTrigger dt;

        public override void Init()
        {
            dt = GetComponent<DialogueTrigger>();

            ent.onDeath += OnDeath;
        }

        private void OnDeath()
        {
            dt.TriggerDialogue();
        }
    }
}
