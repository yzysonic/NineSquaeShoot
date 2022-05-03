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

        public bool IsFiring { get; private set; } = false;

        private Timer timer;
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
                maxCount = Mathf.RoundToInt(ProjectProperty.baseResolution.x / weaponProfile.projectileVelocity / weaponProfile.fireInterval);
            }

            timer = new Timer(weaponProfile.fireInterval);
            timer.Elapsed = weaponProfile.fireInterval;
            character = GetComponent<Character>();

            base.Awake();
        }

        private void Update()
        {
            timer.Step();
            if (!IsFiring)
            {
                return;
            }

            if(timer.IsComplete)
            {
                LaunchProjectile();
                timer.Reset();
            }
        }

        public void StartFire()
        {
            if(IsFiring)
            {
                return;
            }

            if(timer.IsComplete)
            {
                LaunchProjectile();
                timer.Reset();
            }
            
            IsFiring = true;
        }

        public void StopFire()
        {
            if(!IsFiring)
            {
                return;
            }

            IsFiring= false;
        }

        private void LaunchProjectile()
        {
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
                
                projectile.Velocity = weaponProfile.projectileVelocity / 100.0f;
            }
        }
    }
}
