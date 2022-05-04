using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField]
        private List<GameObject> enemyPrefabs = new();

        private void Start()
        {
            // For test only
            GameObject test = Instantiate(enemyPrefabs[0]);
            var enemy = test.GetComponent<Enemy>();
            enemy.Team = ETeam.enemy;

            FieldBlock startBlock = FieldManager.Instance.GetBlock(ETeam.enemy, 4);
            var movement = test.GetComponent<CharacterMovement>();
            movement.TryEnterBlock(startBlock);
        }
    }
}
