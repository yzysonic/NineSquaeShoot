using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField]
        private List<GameObject> enemyPrefabs = new();

        [SerializeField]
        private float baseEnemySpawnInterval = 30.0f;

        [SerializeField, Range(1, FieldManager.teamBlockCount)]
        private int baseEnemySpawnMaxCount = 3;

        [SerializeField, Range(0, FieldManager.teamBlockCount-1)]
        private int keepBlockFreeCount = 1;

        public bool EnableSpawnEnemy { get; set; } = false;

        private readonly Timer spawnTimer = new();

        protected override void Awake()
        {
            base.Awake();
            spawnTimer.Reset(0);
            EnableSpawnEnemy = true;
        }

        private void Update()
        {
            UpdateEnemySpawn();
        }

        private void UpdateEnemySpawn()
        {
            if (!EnableSpawnEnemy)
            {
                return;
            }

            spawnTimer.Step();
            if (spawnTimer.IsComplete)
            {
                SpawnEnemyRandomly();
                spawnTimer.Reset(baseEnemySpawnInterval);
            }
        }

        private void SpawnEnemyRandomly()
        {
            // Check available field block count
            int maxCount = FieldManager.Instance.GetAvailableBlockCount(ETeam.enemy) - keepBlockFreeCount;
            if(maxCount == 0)
            {
                return;
            }

            maxCount = Mathf.Min(maxCount, baseEnemySpawnMaxCount);

            // Determine spawn count
            int spawnCount = Random.Range(1, maxCount);

            for(int i = 0; i < spawnCount; i++)
            {
                // Determine enemy type
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count - 1)];

                // Determine spawn location
                List<FieldBlock> availableBlocs = FieldManager.Instance.GetAvailaleBlocks(ETeam.enemy);
                int blockNo = Random.Range(0, availableBlocs.Count - 1);

                // Spawn enemy
                GameObject enemyObj = Instantiate(enemyPrefab);
                var movement = enemyObj.GetComponent<CharacterMovement>();
                if (movement)
                {
                    movement.TryEnterBlock(availableBlocs[blockNo]);
                }

                availableBlocs.RemoveAtSwap(blockNo);
            }
        }
    }
}
