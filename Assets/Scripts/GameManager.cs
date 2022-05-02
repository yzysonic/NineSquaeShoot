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

        protected override void Awake()
        {
            base.Awake();
            FieldBlock startBlock = FieldManager.Instance.GetBlock(ETeam.player, playerStartFieldBlockIndex);
            if(startBlock != null)
            {
                GameObject playerObj = Instantiate(playerPrefab);
                Player = playerObj.GetComponent<Player>();
                var movement = playerObj.GetComponent<CharacterMovement>();
                if(movement != null)
                {
                    movement.TryEnterBlock(startBlock);
                }
            }
        }
    }
}
