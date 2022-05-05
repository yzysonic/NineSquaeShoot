using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public interface IDamageReceiver
    {
        public void ReceiveDamage(DamageInfo damageInfo);
        public void NotifyDamageSender(DamageInfo damageInfo)
        {
            if (damageInfo != null || damageInfo.Sender != null)
            {
                damageInfo.Sender.OnDamageReceived(damageInfo);
            }
        }
    }
}
