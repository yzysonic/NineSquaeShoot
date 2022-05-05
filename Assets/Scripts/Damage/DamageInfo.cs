using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class DamageInfo
    {
        public uint DamageValue { get; set; } = 0;
        public GameObject SenderObject { get; set; }
        public GameObject ReceiverObject { get; set; }
        public IDamageSender Sender { get; set; }
        public IDamageReceiver Receiver { get; set; }
    }
}
