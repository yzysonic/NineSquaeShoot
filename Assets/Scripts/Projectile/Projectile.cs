using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Projectile : PooledMonoBehavior, IDamageSender
    {
        [SerializeField]
        private AudioClip se;

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
                    stayingBlock.CancelDamageTransferring(this);
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
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("FieldCollider"))
            {
                var fieldCollider = collision.GetComponent<FieldCollider>();
                FieldBlock block = fieldCollider ? fieldCollider.GetBlock(FieldRowIndex) : null;
                if(block && block == StayingBlock)
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
