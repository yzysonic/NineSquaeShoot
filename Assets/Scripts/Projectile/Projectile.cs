using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Projectile : PooledMonoBehavior
    {
        [SerializeField]
        private uint hp = 1;

        [SerializeField]
        private AudioClip se;

        public float Velocity { get; set; } = 0;

        public override void OnDisabled()
        {
            Velocity = 0;
            base.OnDisabled();
        }

        private void Update()
        {
            if(Time.deltaTime != 0)
            {
                transform.Translate(Time.deltaTime * Velocity * transform.right, Space.World);
            }
        }
    }
}
