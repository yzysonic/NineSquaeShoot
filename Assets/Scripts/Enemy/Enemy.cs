using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Enemy : Character
    {
        [SerializeField]
        private float moveInterval = 1;

        [SerializeField]
        private int baseScore = 100;

        public float MoveInterval => moveInterval;

        protected override void Awake()
        {
            base.Awake();
            Team = ETeam.enemy;
            EnemyManager.Instance.OnEnemySpawned(this);
        }

        protected override void OnDestroy()
        {
            if (EnemyManager.IsCreated)
            {
                EnemyManager.Instance.OnEnemyDestroyed(this);
            }
            base.OnDestroy();
        }

        protected override void OnDefeated()
        {
            base.OnDefeated();

            ScoreManager.Instance.CurrentScore += baseScore;

            Destroy(gameObject);
        }
    }
}
