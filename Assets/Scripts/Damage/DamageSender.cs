using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public interface IDamageSender
    {
        public void SendDamage(GameObject receiverObject, DamageInfo damageInfo, IDamageReceiver receiver = null)
        {
            if(damageInfo == null)
            {
                return;
            }

            if (receiver == null)
            {
                receiver = receiverObject ? receiverObject.GetComponent<IDamageReceiver>() : null;
            }

            if (receiver != null)
            {
                damageInfo.Receiver = receiver;
                damageInfo.ReceiverObject = receiverObject;
                receiver.ReceiveDamage(damageInfo);
            }
        }

        public void OnDamageReceived(DamageInfo damageInfo) { }
    }
}
