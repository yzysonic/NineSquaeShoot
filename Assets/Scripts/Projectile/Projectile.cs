using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Projectile : PooledMonoBehavior, IDamageSender
    {
        [SerializeField, Range(1, int.MaxValue)]
        private int life = 1;

        [SerializeField]
        private AudioClip se;

        public Character OwnerCharacter { get; set; }

        public ETeam Team => OwnerCharacter.Team;

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
                Projectile projectile = collision.GetComponent<Projectile>();
                if (projectile)
                {
                    if (FieldRowIndex == projectile.FieldRowIndex)
                    {
                        projectile.IsUsing = false;
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

            stayingBlock = null;
            IsUsing = false;
        }
    }
}
