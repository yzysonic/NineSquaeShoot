using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public enum ETeam
    {
        none = -1,
        player,
        enemy,
        count
    }

    public class Character : MonoBehaviour, IDamageReceiver
    {
        public ETeam Team { get; set; } = ETeam.none;
        public FieldBlock StayingBlock
        {
            get => stayingBlock;
            set
            {
                if (stayingBlock != null)
                {
                    stayingBlock.CharacterExit();
                }
                stayingBlock = value;
            }
        }

        private FieldBlock stayingBlock;

        private LifeComponent life;

        private Animator animator;

        protected virtual void Awake()
        {
            life = GetComponent<LifeComponent>();
            if (life)
            {
                life.ValueChanged += OnLifeChanged;
            }

            animator = gameObject.GetComponent<Animator>();

            GameUIManager.Instance.BindCharacterLifeUI(this);
        }

        public void ReceiveDamage(DamageInfo damageInfo)
        {
            if(damageInfo == null)
            {
                return;
            }

            if (life)
            {
                life.TackDamage(damageInfo.DamageValue);
            }

            (this as IDamageReceiver).NotifyDamageSender(damageInfo);
        }

        protected virtual void OnLifeChanged(uint life)
        {
            if (animator)
            {
                animator.SetTrigger("damaged");
            }
            if (life == 0)
            {
                OnDefeated();
            }
        }

        protected virtual void OnDefeated()
        {
            GameUIManager.Instance.UnbindCharacterLifeUI(this);
        }
    }

}