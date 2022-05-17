using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

        public LifeComponent Life { get => life; }

        public CharacterMovement Movement { get => movement; }

        public Weapon Weapon { get => weapon; }

        public CharacterAudioPlayer AudioPlayer { get; private set; }

        public bool IsInvincible { get; set; } = false;

        public event Action Defeated;

        private FieldBlock stayingBlock;

        private LifeComponent life;

        private CharacterMovement movement;

        private Weapon weapon;

        private Animator animator;

        protected virtual void Awake()
        {
            life = GetComponent<LifeComponent>();
            if (life)
            {
                life.ValueChanged += OnLifeChanged;
            }

            movement = GetComponent<CharacterMovement>();

            weapon = GetComponent<Weapon>();

            animator = gameObject.GetComponent<Animator>();

            AudioPlayer = GetComponent<CharacterAudioPlayer>();

            GameUIManager.Instance.BindCharacterLifeUI(this);
        }

        protected virtual void OnDestroy()
        {
            StayingBlock = null;
            if (GameUIManager.IsCreated)
            {
                GameUIManager.Instance.UnbindCharacterLifeUI(this);
            }
        }

        public void ReceiveDamage(DamageInfo damageInfo)
        {
            if(IsInvincible || damageInfo == null)
            {
                return;
            }

            if (damageInfo.DamageValue > 0)
            {
                if (life)
                {
                    life.TackDamage(damageInfo.DamageValue);
                }

                if (animator)
                {
                    animator.SetTrigger("damaged");
                }

                if (AudioPlayer)
                {
                    AudioPlayer.Play(ECharacterAudio.Damage);
                }
            }

            (this as IDamageReceiver).NotifyDamageSender(damageInfo);
        }

        protected virtual void OnLifeChanged(uint life)
        {
            if (enabled && life == 0)
            {
                OnDefeated();
            }
        }

        protected virtual void OnDefeated()
        {
            if (IsInvincible)
            {
                var i = 0;
            }
            IsInvincible = true;

            if (Defeated != null)
            {
                Defeated.Invoke();
            }

            if (AudioPlayer)
            {
                AudioPlayer.Play(ECharacterAudio.Defeat);
            }

            GameUIManager.Instance.UnbindCharacterLifeUI(this);
            SetComponentsEnabledOnDefeated(false);
        }

        protected virtual void SetComponentsEnabledOnDefeated(bool value)
        {
            var renderer = GetComponent<SpriteRenderer>();
            if (renderer)   renderer.enabled = value;
            if (animator)   animator.enabled = value;
            if (movement)   movement.enabled = value;
            if (weapon)     weapon.enabled = value;
        }

        public void ResetStatus()
        {
            IsInvincible = false;
            StayingBlock = null;
            if (Life)
            {
                Life.ResetValue();
            }
            if (Movement)
            {
                Movement.ResetStatus();
            }
            if (Weapon)
            {
                weapon.ResetStatus();
            }
            if (animator)
            {
                animator.Rebind();
                animator.Play("Idle");
            }
        }
    }

}