using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Projectile : PooledMonoBehavior, IDamageSender, IDamageReceiver
    {
        [SerializeField, Range(1, int.MaxValue)]
        private int life = 1;

        [SerializeField]
        private AudioClip se;

        public Character OwnerCharacter { get; set; }

        public ETeam Team => OwnerCharacter.Team;

        public int Life
        {
            get => life;
            set => life = value;
        }

        public bool ShouldHitOtherProjectile { get; set; } = false;

        public uint FieldRowIndex { get; set; } = 0;

        public float Velocity { get; set; } = 0;

        public uint Damage { get; set; } = 0;

        private FieldBlock StayingBlock
        {
            get => stayingBlock;
            set
            {
                if (stayingBlock)
                {
                    if( stayingBlock.Team == Team)
                    {
                        stayingBlock.OnSelfProjectileExited(this);
                    }
                    else
                    {
                        stayingBlock.CancelDamageTransferring(this);
                    }
                }

                stayingBlock = value;
            }
        }

        private FieldBlock stayingBlock;

        public override void OnDisabled()
        {
            FieldRowIndex = 0;
            Velocity = 0;
            Damage = 0;
            StayingBlock = null;
            base.OnDisabled();
        }

        private void Update()
        {
            if(Time.deltaTime != 0)
            {
                transform.Translate(Time.deltaTime * Velocity * transform.right, Space.World);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsUsing)
            {
                return;
            }

            DamageInfo damageInfo = new()
            {
                DamageValue = Damage,
                SenderObject = gameObject,
                Sender = this,
            };

            if (collision.gameObject.CompareTag("FieldCollider"))
            {
                var fieldCollider = collision.GetComponent<FieldCollider>();
                FieldBlock block = fieldCollider ? fieldCollider.GetBlock(FieldRowIndex) : null;
                if (!block)
                {
                    return;
                }

                // Entered target's field
                if (Team != block.Team)
                {
                    if (block.CanTransferDamage)
                    {
                        StayingBlock = null;
                        (this as IDamageSender).SendDamage(block.gameObject, damageInfo, block);
                    }
                    else
                    {
                        StayingBlock = block;
                        block.ReserveDamageTransferring(this, damageInfo);
                    }
                }

                // Entered self field
                else
                {
                    StayingBlock = block;
                    if (block.StayingCharacter != OwnerCharacter)
                    {
                        block.OnSelfProjectileEntered(this);
                    }
                }
            }
            else if (collision.gameObject.CompareTag("Projectile"))
            {
                if (ShouldHitOtherProjectile)
                {
                    Projectile projectile = collision.GetComponent<Projectile>();
                    if (projectile && projectile.IsUsing)
                    {
                        if (FieldRowIndex == projectile.FieldRowIndex)
                        {
                            damageInfo.DamageValue = 1;
                            (this as IDamageSender).SendDamage(projectile.gameObject, damageInfo, projectile);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("FieldCollider"))
            {
                var fieldCollider = collision.GetComponent<FieldCollider>();
                FieldBlock block = fieldCollider ? fieldCollider.GetBlock(FieldRowIndex) : null;
                if (block == StayingBlock)
                {
                    StayingBlock = null;
                }
            }
        }

        public void OnDamageReceived(DamageInfo damageInfo)
        {
            if (damageInfo == null || !damageInfo.ReceiverObject)
            {
                return;
            }

            var projectile = damageInfo.Receiver as Projectile;
            if (projectile)
            {
                StayingBlock = null;
                TackDamage(1);
            }
            else
            {
                TackDamage(life);
            }
        }

        public void ReceiveDamage(DamageInfo damageInfo)
        {
            StayingBlock = null;
            TackDamage((int)damageInfo.DamageValue);
            (this as IDamageReceiver).NotifyDamageSender(damageInfo);
        }

        private void TackDamage(int damage)
        {
            life -= damage;
            if (life <= 0)
            {
                stayingBlock = null;
                IsUsing = false;
            }
        }
    }
}
