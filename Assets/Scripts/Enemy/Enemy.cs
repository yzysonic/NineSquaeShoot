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

        public EnemyBehaviour Behaviour { get; private set; }

        public float MoveInterval => moveInterval;

        protected override void Awake()
        {
            base.Awake();
            Team = ETeam.enemy;
            Behaviour = GetComponent<EnemyBehaviour>();
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

        public override void EntryField(FieldBlock block)
        {
            base.EntryField(block);
            if (Behaviour)
            {
                Behaviour.IsWaitEntryPerformance = true;
            }
        }

        public override void OnEntryPerformanceFinished()
        {
            base.OnEntryPerformanceFinished();
            if (Behaviour)
            {
                Behaviour.IsWaitEntryPerformance = false;
            }
        }

        public override void OnDefeatPerformanceFinished()
        {
            base.OnDefeatPerformanceFinished();
            StayingBlock = null;
            Destroy(gameObject, 0.5f);
        }

        protected override void OnDefeated()
        {
            base.OnDefeated();
            EnemyManager.Instance.AddDefeatCount(1);
            ScoreManager.Instance.CurrentScore += baseScore;
        }

        protected override void SetComponentsEnabledOnDefeated(bool value)
        {
            base.SetComponentsEnabledOnDefeated(value);
            if (Behaviour) Behaviour.enabled = value;
        }
    }
}
