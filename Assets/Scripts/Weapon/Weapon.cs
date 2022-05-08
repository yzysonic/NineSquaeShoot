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

        private Timer coolDownTimer;
        private Character character;

        protected override void Awake()
        {
            prefab = weaponProfile.projectilePrefab;
            if(weaponProfile.projectileVelocity * weaponProfile.fireInterval == 0)
            {
                maxCount = 0;
            }
            else
            {
                maxCount = Mathf.CeilToInt((float)ProjectProperty.baseResolution.x / weaponProfile.projectileVelocity / weaponProfile.fireInterval);
            }

            coolDownTimer = new Timer(weaponProfile.fireInterval);
            coolDownTimer.Elapsed = weaponProfile.fireInterval;
            character = GetComponent<Character>();

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

        public void StartRapidFire()
        {
            if(IsRapidFiring)
            {
                return;
            }

            TryFireOnce();

            IsRapidFiring = true;
        }

        public void StopRapidFire()
        {
            if(!IsRapidFiring)
            {
                return;
            }

            IsRapidFiring= false;
        }

        public void TryFireOnce()
        {
            if(IsCoolDownComplete)
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

                projectile.FieldRowIndex = character.StayingBlock.RowIndex;
                projectile.Velocity = weaponProfile.projectileVelocity / 100.0f;
                projectile.Damage = weaponProfile.damage;

                coolDownTimer.Reset();
            }
        }
    }
}
