using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Player : Character
    {
        public PlayerController Controller { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Team = ETeam.player;
            Controller = GetComponent<PlayerController>();
        }

        public void OnNewGameStarted()
        {
            SetComponentsEnabledOnDefeated(true);
            ResetStatus();
            GameUIManager.Instance.BindCharacterLifeUI(this);
        }

        protected override void OnDefeated()
        {
            base.OnDefeated();
            GameUIManager.Instance.UnbindCharacterLifeUI(this);
        }

        protected override void SetComponentsEnabledOnDefeated(bool value)
        {
            base.SetComponentsEnabledOnDefeated(value);
            if (Controller) Controller.enabled = value;
        }
    }
}
