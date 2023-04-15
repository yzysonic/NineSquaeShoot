using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Player : Character
    {
        public PlayerController Controller { get; private set; }

        private PlayerCounterAction counterAction;

        protected override void Awake()
        {
            base.Awake();
            Team = ETeam.player;
            Controller = GetComponent<PlayerController>();
            counterAction = GetComponent<PlayerCounterAction>();
        }

        public void OnNewGameStarted()
        {
            SetComponentsEnabledOnDefeated(true);
            ResetStatus();
            GameUIManager.Instance.BindCharacterLifeUI(this);

            if (counterAction)
            {
                counterAction.InitCoolTimer();
            }
        }

        protected override void OnDefeated()
        {
            base.OnDefeated();
            GameUIManager.Instance.UnbindCharacterLifeUI(this);

            if (counterAction)
            {
                counterAction.OnPlayerDefeated();
            }
        }

        public override void OnDefeatPerformanceFinished()
        {
            ResultManager.Instance.DisplayResult();
        }

        protected override void SetComponentsEnabledOnDefeated(bool value)
        {
            base.SetComponentsEnabledOnDefeated(value);
            if (Controller) Controller.enabled = value;
        }

        public override void ReceiveDamage(DamageInfo damageInfo)
        {
            if (counterAction && counterAction.ShouldProcessDamage)
            {
                counterAction.ProcessDamage(damageInfo);
            }

            if (damageInfo.DamageValue > 0)
            {
                base.ReceiveDamage(damageInfo);
            }
        }

        public void OnCounterValidSectionStart()
        {
            if (counterAction)
            {
                counterAction.SetCounterValidFlag(true);
            }
        }

        public void OnCounterValidSectionEnd()
        {
            if (counterAction)
            {
                counterAction.SetCounterValidFlag(false);
            }
        }

        public void OnCounterAnimationEnd()
        {
            if (counterAction)
            {
                counterAction.OnCounterAnimationEnd();
            }
        }
    }
}
