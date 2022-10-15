using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private GameObject playerPrefab;

        [SerializeField, Range(0, FieldManager.teamBlockCount-1)]
        private int playerStartFieldBlockIndex = 0;

        public Player Player { get; private set; }

        private FieldBlock playerStartBlock;

        protected override void Awake()
        {
            base.Awake();
            playerStartBlock = FieldManager.Instance.GetBlock(ETeam.player, playerStartFieldBlockIndex);
            if(playerStartBlock != null)
            {
                GameObject playerObj = Instantiate(playerPrefab);
                Player = playerObj.GetComponent<Player>();
                if (Player)
                {
                    Player.EntryField(playerStartBlock);
                    Player.Defeated += OnPlayerDefeated;
                }
            }
        }

        private void OnPlayerDefeated()
        {
            MainGameTimer.Instance.enabled = false;
            ScoreManager.Instance.IsScoreReadonly = true;
            EnemyManager.Instance.EnableSpawnEnemy = false;
            ResultManager.Instance.OnPlayerDefeated();
        }

        public void StartNewGame()
        {
            MainGameTimer.Instance.OnNewGameStarted();
            ScoreManager.Instance.OnNewGameStarted();
            EnemyManager.Instance.OnNewGameStarted();
            GameUIManager.Instance.OnNewGameStarted();
            BGMPlayer.Instance.FadeOutToPlayNextBGM("Default", 0.5f);

            if (Player)
            {
                Player.OnNewGameStarted();
                if (Player.Movement && playerStartBlock)
                {
                    Player.Movement.TryEnterBlock(playerStartBlock);
                }
            }
        }
    }
}
