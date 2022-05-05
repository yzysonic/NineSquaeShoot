using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Enemy : Character
    {
        [SerializeField]
        private float moveInterval = 1;

        public float MoveInterval => moveInterval;

        protected override void Awake()
        {
            base.Awake();
            Team = ETeam.enemy;
        }

        protected override void OnDefeated()
        {
            base.OnDefeated();
            Destroy(gameObject);
        }
    }
}
