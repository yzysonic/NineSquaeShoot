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

        private void Start() {
            var temp = ScriptableObjectController.Instance.CharacterStatusData.dataArray[PlayerData.CurrentCharacterID - 1];
            Status.SetStatus(temp.N_ID, temp.N_Name, temp.N_Description, temp.N_Hp, temp.N_Weapon, temp.N_Movetime, temp.N_Colddown, temp.N_Hprecovervalue, temp.N_Hprecovertime
                , temp.N_Str, temp.N_Weapontype2raito, temp.N_Weapontype3raito, temp.N_Weapontype4raito, temp.N_Critcalpercent, temp.N_Critcalratio, temp.N_Block, temp.N_Stealheal
                , temp.N_Stealhealratio, temp.N_Dodgeratio, temp.N_Skillcolddownratio, temp.N_Luckvalue, temp.S_Prefabname);
            Life.MaxValue = (uint)Status.HP;
            Life.Value = (uint)Status.HP;
            Movement.SetMoveDuration(Status.MoveTime);
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
