using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class FieldManager : Singleton<FieldManager>
    {
        public const int teamBlockSideCount = 3;
        public const int teamBlockCount = teamBlockSideCount * teamBlockSideCount;

        [SerializeField]
        private GameObject playerFieldBlockPrefab;

        [SerializeField]
        private GameObject enemyFieldBlockPrefab;

        [SerializeField]
        private Vector2 fieldBlockStartLocation = new Vector2(3, 5);

        [SerializeField]
        private Vector2 fieldBlockInterval = 1.3f * Vector2.one;

        private FieldBlock[][] blocks;

        protected override void Awake()
        {
            base.Awake();

            blocks = new FieldBlock[(int)ETeam.count][];
            for(int i = 0; i < (int)ETeam.count; i++)
            {
                blocks[i] = new FieldBlock[teamBlockCount];
            }

            // Spawn player's field
            for (int y = 0; y < teamBlockSideCount; y++)
            {
                for (int x = 0; x < teamBlockSideCount; x++)
                {
                    Vector3 pos = (Vector3)fieldBlockStartLocation + new Vector3(x * fieldBlockInterval.x, y * -fieldBlockInterval.y, 0);
                    GameObject blockObj = Instantiate(playerFieldBlockPrefab, pos, Quaternion.identity);
                    var block = blockObj.GetComponent<FieldBlock>();
                    block.Index = y * teamBlockSideCount + x;
                    block.Team = ETeam.player;
                    blocks[(int)ETeam.player][block.Index] = block;
                }
            }

            // Spawn Enemy's field
            for (int y = 0; y < teamBlockSideCount; y++)
            {
                for (int x = 0; x < teamBlockSideCount; x++)
                {
                    Vector3 pos = new Vector3(-fieldBlockStartLocation.x, fieldBlockStartLocation.y) + new Vector3(ProjectProperty.baseResolution.x/100.0f + x * fieldBlockInterval.x - (teamBlockSideCount - 1) * fieldBlockInterval.x, y * -fieldBlockInterval.y, 0);
                    GameObject blockObj = Instantiate(enemyFieldBlockPrefab, pos, Quaternion.identity);
                    var block = blockObj.GetComponent<FieldBlock>();
                    block.Index = y * teamBlockSideCount + x;
                    block.Team = ETeam.enemy;
                    blocks[(int)ETeam.enemy][block.Index] = block;
                }
            }
        }

        public FieldBlock GetAdjacentBlock(in FieldBlock currentBlock, Vector2 direction)
        {
            if (currentBlock == null)
            {
                return null;
            }

            // Try to get target block depends on moving direction
            if (direction.x > 0)
            {
                return GetRightBlock(currentBlock);
            }
            else if (direction.x < 0)
            {
                return GetLeftBlock(currentBlock);
            }
            else if (direction.y > 0)
            {
                return GetUpBlock(currentBlock);
            }
            else if (direction.y < 0)
            {
                return GetDownBlock(currentBlock);
            }

            // No block can move
            return null;
        }

        public FieldBlock GetBlock(ETeam team, int index)
        {
            if (index < 0 || index >= teamBlockCount)
            {
                return null;
            }

            return blocks[(int)team][index];
        }

        public FieldBlock GetUpBlock(in FieldBlock currentBlock)
        {
            int upBlockIndex = currentBlock.Index - teamBlockSideCount;
            return GetBlock(currentBlock.Team, upBlockIndex);
        }

        public FieldBlock GetDownBlock(in FieldBlock currentBlock)
        {
            int downBlockIndex = currentBlock.Index + teamBlockSideCount;
            return GetBlock(currentBlock.Team, downBlockIndex);
        }

        public FieldBlock GetLeftBlock(in FieldBlock currentBlock)
        {
            int modIndex = currentBlock.Index % teamBlockSideCount;
            if (modIndex == 0)
            {
                return null;
            }

            int leftBlockIndex = currentBlock.Index - 1;
            return GetBlock(currentBlock.Team, leftBlockIndex);
        }

        public FieldBlock GetRightBlock(in FieldBlock currentBlock)
        {
            int modIndex = currentBlock.Index % teamBlockSideCount;
            if (modIndex == (teamBlockSideCount - 1))
            {
                return null;
            }

            int rightBlockIndex = currentBlock.Index + 1;
            return GetBlock(currentBlock.Team, rightBlockIndex);
        }
    }
}
