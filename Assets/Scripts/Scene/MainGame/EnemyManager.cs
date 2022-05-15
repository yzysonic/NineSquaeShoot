using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    [System.Serializable]
    public class EnemyDifficultyParam
    {
        public int scoreMax = 0;
        public float hpBonusRate = 1.0f;
        public float damageBonusRate = 1.0f;
        public float attackIntervalBonusRate = 1.0f;
        public float spawnMaxCountBonusRate = 1.0f;
        public float spawnIntervalBonusRate = 1.0f;
    }

    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField]
        private List<GameObject> enemyPrefabs = new();

        [SerializeField]
        private float startEnemySpawnTime = 1.5f;

        [SerializeField]
        private float baseEnemySpawnInterval = 30.0f;

        [SerializeField, Range(1, FieldManager.teamBlockCount)]
        private int baseEnemySpawnMaxCount = 3;

        [SerializeField, Range(0, FieldManager.teamBlockCount - 1)]
        private int keepBlockFreeCount = 1;

        [SerializeField]
        private List<EnemyDifficultyParam> enemyDiffcultyParamList;

        public bool EnableSpawnEnemy { get; set; } = false;

        private EnemyDifficultyParam currentDiffcultyParam;

        private readonly Timer spawnTimer = new();

        private readonly HashSet<Enemy> enemies = new HashSet<Enemy>();

        protected override void Awake()
        {
            base.Awake();
            EnableSpawnEnemy = true;

            ScoreManager.Instance.CurrentScoreChanged += OnScoreChanged;
            OnScoreChanged(ScoreManager.Instance.CurrentScore);
            
            spawnTimer.Reset(startEnemySpawnTime);
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
            if (maxCount == 0)
            {
                return;
            }

            // Determine spawn count
            int spawnCount = Mathf.Min(maxCount, (int)(baseEnemySpawnMaxCount * currentDiffcultyParam.spawnMaxCountBonusRate));

            for (int i = 0; i < spawnCount; i++)
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

                ApplyEnemyDiffcultyParam(enemyObj);

                availableBlocs.RemoveAtSwap(blockNo);
            }
        }

        public void DestroyAllEnemies(bool disableAllProjectiles = true)
        {
            HashSet<Enemy> list = enemies;
            foreach (Enemy enemy in list)
            {
                if (enemy.Weapon && disableAllProjectiles)
                {
                    enemy.Weapon.DisableAllPoolObject();
                }
                Destroy(enemy.gameObject);
            }
        }

        public void OnNewGameStarted()
        {
            DestroyAllEnemies();
            spawnTimer.Reset(startEnemySpawnTime);
            EnableSpawnEnemy = true;
        }

        public void OnEnemySpawned(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void OnEnemyDestroyed(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        private void OnScoreChanged(int score)
        {
            if (enemyDiffcultyParamList.Count == 0)
            {
                return;
            }

            System.Index index = enemyDiffcultyParamList.FindIndex(row => score <= row.scoreMax);
            if (index.Value < 0)
            {
                index = ^1;
            }

            currentDiffcultyParam = enemyDiffcultyParamList[index];
            spawnTimer.Interval = baseEnemySpawnInterval * currentDiffcultyParam.spawnIntervalBonusRate;
        }

        private void ApplyEnemyDiffcultyParam(GameObject enemyObj)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (!enemy)
            {
                return;
            }

            // Apply life bonus
            var life = enemy.Life;
            if (life)
            {
                var applyValue = (uint)(life.MaxValue * currentDiffcultyParam.hpBonusRate);
                life.ResetValue(applyValue);
            }

            var weapon = enemy.Weapon;
            if (weapon)
            {
                // Apply damage bonus
                weapon.DamageBonusRate = currentDiffcultyParam.damageBonusRate;

                // Apply attack frequency bonus
                weapon.FireIntervalBonusRate = currentDiffcultyParam.attackIntervalBonusRate;
            }
        }
    }
}
