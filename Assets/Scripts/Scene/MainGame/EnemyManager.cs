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
        private float startEnemySpawnTime = 1.5f;

        [SerializeField]
        private float baseEnemySpawnInterval = 30.0f;

        [SerializeField]
        private float overrideSpawnIntervalOnEnemyEmpty = 2f;

        [SerializeField]
        private int baseEnemySpawnMaxCount = 3;

        [SerializeField]
        private int keepBlockFreeCount = 1;

        [SerializeField] private int CurrentCreateEnemyCount = 0;
        [SerializeField] private int CurrentDefeatEnemyCount = 0;

        [SerializeField]
        private List<EnemyDifficultyParam> enemyDiffcultyParamList;

        public bool EnableSpawnEnemy { get; set; } = false;

        private EnemyDifficultyParam currentDiffcultyParam;

        private readonly Timer spawnTimer = new();

        private readonly HashSet<Enemy> enemies = new HashSet<Enemy>();

        private List<FieldBlock> availableBlocks;

        private RoundGroupData CurrentGroup;

        protected override void Awake() {
            base.Awake();
            ScoreManager.Instance.CurrentScoreChanged += OnScoreChanged;
            OnScoreChanged(ScoreManager.Instance.CurrentScore);
            WaveController.Instance.RegisterWaveChanged(OnWaveChanged);
            spawnTimer.Reset(startEnemySpawnTime);
        }

        private void Start() {
            availableBlocks = FieldManager.Instance.GetAvailaleBlocks(ETeam.enemy);
            CurrentGroup = StageController.Instance.GetCurrentGroupData();
            baseEnemySpawnMaxCount = CurrentGroup.CreateGridList.Count;
            AddressableLoader.Instance.RegisterAssetLoaded(OnPrefabLoaded);
        }

        private void Update() {
            UpdateEnemySpawn();
        }

        private void UpdateEnemySpawn() {
            if (!EnableSpawnEnemy) {
                return;
            }

            spawnTimer.Step();
            if (spawnTimer.IsComplete && CurrentCreateEnemyCount < CurrentGroup.CreateGridList.Count - 1) {
                SpawnEnemy();
                spawnTimer.Reset(baseEnemySpawnInterval);
            }
        }

        void SpawnEnemy() {
            int maxCount = FieldManager.Instance.GetAvailableBlockCount(ETeam.enemy) - keepBlockFreeCount;
            if (maxCount == 0) {
                return;
            }
            for (int i = 0; i < CurrentGroup.CreateGridList.Count; i++) {
                int blockNo = CurrentGroup.CreateGridList[i];
                GameObject enemyPrefab = AddressableLoader.Instance.GetCharacterPrefaab(CurrentGroup.MonsterID);
                if (enemyPrefab != null) {
                    GameObject enemyObj = Instantiate(enemyPrefab);
                    var enemy = enemyObj.GetComponent<Enemy>();
                    if (enemy) {
                        enemy.Status = ScriptableObjectController.Instance.EnemyStatusDic[CurrentGroup.MonsterID];
                        enemy.EntryField(availableBlocks[blockNo]);
                    }
                    ApplyEnemyDiffcultyParam(enemyObj);
                    CurrentCreateEnemyCount++;
                }
            }   
        }

        private void SpawnEnemyRandomly() {
            //// Check available field block count
            //int maxCount = FieldManager.Instance.GetAvailableBlockCount(ETeam.enemy) - keepBlockFreeCount;
            //if (maxCount == 0) {
            //    return;
            //}

            //// Determine spawn count
            //int spawnCount = Mathf.Min(maxCount, (int)(baseEnemySpawnMaxCount * currentDiffcultyParam.spawnMaxCountBonusRate));

            //List<FieldBlock> availableBlocks = FieldManager.Instance.GetAvailaleBlocks(ETeam.enemy);

            //for (int i = 0; i < spawnCount; i++) {
            //    // Determine enemy type
            //    GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count - 1)];

            //    // Determine spawn location
            //    int blockNo = Random.Range(0, availableBlocks.Count - 1);

            //    // Spawn enemy
            //    GameObject enemyObj = Instantiate(enemyPrefab);
            //    var enemy = enemyObj.GetComponent<Enemy>();
            //    if (enemy) {
            //        enemy.EntryField(availableBlocks[blockNo]);
            //    }

            //    ApplyEnemyDiffcultyParam(enemyObj);

            //    availableBlocks.RemoveAtSwap(blockNo);
            //}
        }

        public void DestroyAllEnemies(bool disableAllProjectiles = true) {
            HashSet<Enemy> list = enemies;
            foreach (Enemy enemy in list) {
                if (enemy.Weapon && disableAllProjectiles) {
                    enemy.Weapon.DisableAllPoolObject();
                }
                Destroy(enemy.gameObject);
            }
        }

        public void OnNewGameStarted() {
            DestroyAllEnemies();
            spawnTimer.Reset(startEnemySpawnTime);
            EnableSpawnEnemy = true;
            WaveController.Instance.ResetWave();
            CurrentDefeatEnemyCount = 0;
            CurrentCreateEnemyCount = 0;
            CurrentGroup = StageController.Instance.GetCurrentGroupData();
            baseEnemySpawnMaxCount = CurrentGroup.CreateGridList.Count;
        }

        public void OnEnemySpawned(Enemy enemy) {
            enemies.Add(enemy);
        }

        public void OnEnemyDestroyed(Enemy enemy) {
            enemies.Remove(enemy);
            if (enemies.Count == 0) {
                OnEnemyEmpty();
            }
        }

        private void OnEnemyEmpty() {
            if (spawnTimer.RemainingTime > overrideSpawnIntervalOnEnemyEmpty) {
                spawnTimer.Elapsed = spawnTimer.Interval - overrideSpawnIntervalOnEnemyEmpty;
            }
        }

        private void OnScoreChanged(int score) {
            if (enemyDiffcultyParamList.Count == 0) {
                return;
            }

            int findResult = enemyDiffcultyParamList.FindIndex(row => score <= row.scoreMax);
            System.Index index = findResult >= 0 ? findResult : ^1;

            currentDiffcultyParam = enemyDiffcultyParamList[index];
            spawnTimer.Interval = baseEnemySpawnInterval * currentDiffcultyParam.spawnIntervalBonusRate;
        }

        private void ApplyEnemyDiffcultyParam(GameObject enemyObj) {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (!enemy) {
                return;
            }

            // Apply life bonus
            var life = enemy.Life;
            if (life) {
                var applyValue = (uint)(life.MaxValue * currentDiffcultyParam.hpBonusRate);
                life.ResetValue(applyValue);
            }

            var weapon = enemy.Weapon;
            if (weapon) {
                // Apply damage bonus
                weapon.DamageBonusRate = currentDiffcultyParam.damageBonusRate;

                // Apply attack frequency bonus
                weapon.FireIntervalBonusRate = currentDiffcultyParam.attackIntervalBonusRate;
            }
        }

        public void AddDefeatCount(int Value) {
            CurrentDefeatEnemyCount += Value;
            if (CurrentDefeatEnemyCount >= CurrentCreateEnemyCount && CurrentCreateEnemyCount == baseEnemySpawnMaxCount) {
                CurrentDefeatEnemyCount = 0;
                StageController.Instance.CheckStageClear();
            }
        }

        void OnWaveChanged(int wave) {
            CurrentCreateEnemyCount = 0;
            CurrentGroup = StageController.Instance.GetCurrentGroupData();
            baseEnemySpawnMaxCount = CurrentGroup.CreateGridList.Count;
        }

        void OnPrefabLoaded() {
            EnableSpawnEnemy = true;
        }
    }
}
