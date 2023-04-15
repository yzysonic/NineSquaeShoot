using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class PlayerCounterAction : MonoBehaviour
    {
        private enum EState
        {
            None,
            Cooling,
            Ready,
            WaitMove,
            Executing,
            Success
        }

        [SerializeField]
        private float coolTime = 5.0f;

        [SerializeField]
        private float failDamageBonusRate = 1.5f;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private GameObject projectilePrefab;

        [SerializeField]
        private uint projectileVelocity = 1600;

        [SerializeField]
        private float projectileDamageBonusRate = 1.5f;

        [SerializeField]
        private float projectileLifeBounusRate = 5.0f;

        [SerializeField]
        private Animator effectAnimator;

        public bool ShouldProcessDamage => state == EState.Executing;
        public bool ShouldBlockOtherAction => state == EState.Executing || state == EState.Success;

        public event Action<float> CoolTimeProgressChanged;
        public event Action PreCounterExecuted;
        public float CurrentCoolTimeProgress => coolTimer.Progress;

        private Player player;
        private CharacterMovement movement;
        private PlayerCounterPerformance performance;
        private Animator animator;
        private Timer coolTimer;
        private EState state = EState.None;
        private bool CanCounter = false;

        private void Awake()
        {
            player = GetComponent<Player>();
            movement = GetComponent<CharacterMovement>();
            performance = GetComponent<PlayerCounterPerformance>();
            animator = GetComponent<Animator>();
            coolTimer = new Timer(coolTime);
            InitCoolTimer();
        }

        private void Update()
        {
            if (state == EState.None)
            {
                return;
            }

            if (state == EState.Cooling)
            {
                if (coolTimer.IsComplete)
                {
                    state = EState.Ready;
                    if (effectAnimator)
                    {
                        effectAnimator.gameObject.SetActive(true);
                        effectAnimator.enabled = true;
                        effectAnimator.Play("Start");
                    }
                }
                else
                {
                    coolTimer.Step();
                    CoolTimeProgressChanged?.Invoke(coolTimer.Progress);
                }
                return;
            }

            if (state == EState.WaitMove)
            {
                if (!movement || !movement.IsMoving)
                {
                    ExecutePreCounter();
                }
            }
        }

        private void ExecutePreCounter()
        {
            if (player)
            {
                player.OnSkillStarted();
            }

            if (effectAnimator)
            {
                effectAnimator.StopPlayback();
                effectAnimator.enabled = false;
            }

            if (performance)
            {
                performance.StartSlowPerformance();
            }

            if (PreCounterExecuted != null)
            {
                PreCounterExecuted.Invoke();
            }

            coolTimer.Reset(coolTime);
            state = EState.Executing;
        }

        private void ExecuteCounter(uint receivedDamage)
        {
            if (player)
            {
                player.OnSkillPerformanceStarted();
                player.IsInvincible = true;

                GameObject projectileObj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                Projectile projectile = projectileObj.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.IsDestroyOnDisabled = true;
                    projectile.ShouldHitOtherProjectile = true;
                    projectile.gameObject.layer = LayerMask.NameToLayer("PlayerAttack");
                    projectile.OwnerCharacter = player;
                    projectile.FieldRowIndex = player.StayingBlock.RowIndex;
                    projectile.Velocity = projectileVelocity / 100.0f;
                    projectile.Damage = (uint)(receivedDamage * projectileDamageBonusRate);
                    projectile.Life = (int)(projectileLifeBounusRate * projectile.Life);
                }

                if (player.AudioPlayer)
                {
                    player.AudioPlayer.Play(ECharacterAudio.SkillFire);
                }
            }

            state = EState.Success;
        }

        public void RequestCounter()
        {
            if (coolTimer.IsComplete && state == EState.Ready)
            {
                if (movement && movement.IsMoving)
                {
                    state = EState.WaitMove;
                }
                else
                {
                    ExecutePreCounter();
                }
            }
        }

        public void OnPlayerDamaged()
        {
            if (state == EState.WaitMove)
            {
                state = EState.Ready;
            }
        }

        public void OnPlayerDefeated()
        {
            if (effectAnimator)
            {
                effectAnimator.gameObject.SetActive(false);
            }
            state = EState.None;
        }

        public void SetCounterValidFlag(bool flag)
        {
            CanCounter = state == EState.Executing && flag;

            if (!flag && performance)
            {
                performance.StopSlowPerformance();
            }
        }

        public void ProcessDamage(DamageInfo damageInfo)
        {
            // Counter success
            if (CanCounter)
            {
                uint receivedDamage = damageInfo.DamageValue;
                damageInfo.DamageValue = 0;

                if (player)
                {
                    (player as IDamageReceiver).NotifyDamageSender(damageInfo);
                }

                ExecuteCounter(receivedDamage);
            }

            // Counter fial
            else
            {
                damageInfo.DamageValue = (uint)(damageInfo.DamageValue * failDamageBonusRate);
                state = EState.Cooling;

                if (effectAnimator)
                {
                    effectAnimator.gameObject.SetActive(false);
                }
            }

            if (performance)
            {
                performance.StopSlowPerformance();
            }
        }

        public void OnCounterAnimationEnd()
        {
            if (player)
            {
                player.IsInvincible = false;
            }

            if (effectAnimator)
            {
                effectAnimator.gameObject.SetActive(false);
            }


            state = EState.Cooling;
        }

        public void InitCoolTimer()
        {
            coolTimer.Reset();
            state = EState.Cooling;
        }
    }
}
