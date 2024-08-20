using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private GameObject playerPrefab;

        [SerializeField]
        private int playerStartFieldBlockIndex = 0;

        public Player Player { get; private set; }

        private FieldBlock playerStartBlock;

        [Header("是否要播放假通關動畫")]
        public bool CanPlayEndAni;

        public Animation FakeClearAni;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start() {
            AddressableLoader.Instance.RegisterAssetLoaded(OnPlayerLoaded);
        }

        private void OnPlayerDefeated()
        {
            MainGameTimer.Instance.enabled = false;
            ScoreManager.Instance.IsScoreReadonly = true;
            EnemyManager.Instance.EnableSpawnEnemy = false;
            BGMPlayer.Instance.Stop();
        }

        public void StartNewGame()
        {
            MainGameTimer.Instance.OnNewGameStarted();
            ScoreManager.Instance.OnNewGameStarted();
            EnemyManager.Instance.OnNewGameStarted();
            GameUIManager.Instance.OnNewGameStarted();
            ItemManager.Instance.OnNewGameStarted();
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

        private void OnPlayerLoaded() {
            playerStartBlock = FieldManager.Instance.GetBlock(ETeam.player, playerStartFieldBlockIndex, BlockType.Player);
            if (playerStartBlock != null) {
                playerPrefab = AddressableLoader.Instance.GetCharacterPrefaab(SaveDataController.Instance.Data.playerData.CurrentCharacterID);
                if (playerPrefab != null) {
                    GameObject playerObj = Instantiate(playerPrefab);
                    Player = playerObj.GetComponent<Player>();
                    if (Player) {
                        Player.EntryField(playerStartBlock);
                        Player.Defeated += OnPlayerDefeated;
                    }
                }
            }
        }
    }
}
