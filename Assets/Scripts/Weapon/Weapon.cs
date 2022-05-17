using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Weapon : Pool<Projectile>
    {
        [SerializeField]
        private WeaponProfile weaponProfile;

        [SerializeField]
        private Transform firePoint;

        public bool IsRapidFiring { get; private set; } = false;

        public bool IsCoolDownComplete => coolDownTimer.IsComplete;

        public float DamageBonusRate { get; set; } = 1.0f;

        public float FireIntervalBonusRate
        {
            get => fireIntervalBonusRate;
            set
            {
                if (fireIntervalBonusRate != value)
                {
                    fireIntervalBonusRate = value;

                    // Reset cooldown timer
                    coolDownTimer.Reset(FireInterval);
                    coolDownTimer.Elapsed = FireInterval;

                    // Reset pool size
                    DeterminePoolSize();
                    ResizePool();
                }
            }
        }

        private float FireInterval
        {
            get
            {
                if (weaponProfile)
                {
                    return weaponProfile.fireInterval * FireIntervalBonusRate;
                }
                return 0.0f;
            }
        }

        private Timer coolDownTimer;
        private Character character;
        private float fireIntervalBonusRate = 1.0f;

        protected override void Awake()
        {
            prefab = weaponProfile.projectilePrefab;
            DeterminePoolSize();

            coolDownTimer = new Timer(FireInterval);
            coolDownTimer.Elapsed = FireInterval;
            character = GetComponent<Character>();

            shouldClearAllObjectOnDestry = false;
            base.Awake();
        }

        private void Update()
        {
            coolDownTimer.Step();
            if (!IsRapidFiring)
            {
                return;
            }

            TryFireOnce();
        }

        protected override void OnDestroy()
        {
            foreach (var projectile in PooledObjects)
            {
                if (!projectile)
                {
                    continue;
                }

                if (!projectile.gameObject.activeInHierarchy)
                {
                    Destroy(projectile.gameObject);
                }
                else
                {
                    projectile.IsDestroyOnDisabled = true;
                }
            }

            base.OnDestroy();
        }

        private void DeterminePoolSize()
        {
            if (weaponProfile.projectileVelocity * FireInterval == 0)
            {
                maxCount = 0;
            }
            else
            {
                maxCount = Mathf.CeilToInt((float)ProjectProperty.baseResolution.x / weaponProfile.projectileVelocity / FireInterval);
            }
        }

        public void StartRapidFire()
        {
            if (IsRapidFiring)
            {
                return;
            }

            TryFireOnce();

            IsRapidFiring = true;
        }

        public void StopRapidFire()
        {
            if (!IsRapidFiring)
            {
                return;
            }

            IsRapidFiring = false;
        }

        public void TryFireOnce()
        {
            if (IsCoolDownComplete)
            {
                LaunchProjectile();
            }
        }

        private void LaunchProjectile()
        {
            if (!character || !character.StayingBlock)
            {
                return;
            }

            Projectile projectile = GetAvailablePoolObject(firePoint.position, firePoint.rotation, null);
            if (projectile != null)
            {
                if (character.Team == ETeam.player)
                {
                    projectile.gameObject.layer = LayerMask.NameToLayer("PlayerAttack");
                }
                else if (character.Team == ETeam.enemy)
                {
                    projectile.gameObject.layer = LayerMask.NameToLayer("EnemyAttack");
                }

                projectile.OwnerCharacter = character;
                projectile.FieldRowIndex = character.StayingBlock.RowIndex;
                projectile.Velocity = weaponProfile.projectileVelocity / 100.0f;
                projectile.Damage = (uint)(weaponProfile.damage * DamageBonusRate);

                if (character.AudioPlayer)
                {
                    character.AudioPlayer.Play(ECharacterAudio.Fire);
                }

                coolDownTimer.Reset();
            }
        }

        public void ResetStatus()
        {
            if (coolDownTimer)
            {
                coolDownTimer.Elapsed = FireInterval;
            }
        }
    }
}
