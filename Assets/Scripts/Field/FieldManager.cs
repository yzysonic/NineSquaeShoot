using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

namespace NSS
{
    public class FieldManager : Singleton<FieldManager>
    {
        [SerializeField] private GameObject playerFieldBlockPrefab;
        [SerializeField] private GameObject enemyFieldBlockPrefab;
        [SerializeField] private GameObject fieldColliderPrefab;

        private Vector2 PlayerFieldBlockStartLocation = new(1.22f, 3.88f);
        private Vector2 PlayerFieldBlockInterval = new Vector2(2.08f, 0.9f);
        private Vector2 EnemyFieldBlockStartLocation = new(-1.22f, 3.88f);
        private Vector2 EnemyFieldBlockInterval = new Vector2(2.08f, 0.9f);

        private Vector3 PlayerFieldBlockScale = new Vector3(1, 1, 1);
        private Vector3 EnemyFieldBlockScale = new Vector3(1, 1, 1);

        #region PlayerField
        [Header("是否自動生成玩家戰鬥區域")]
        [PropertyTooltip("打勾表示遊戲開始時會自動生成，如果沒有則表示需要預先在場景中部署玩家戰鬥區域的格子")]
        [SerializeField] private bool AutoCreatePlayerBlock;

        [ShowIf("AutoCreatePlayerBlock")]
        [Header("玩家戰鬥區域長度")]
        [PropertyTooltip("橫向的格子有幾格")]
        [SerializeField] private int _PlayerWidthCount;
        public int PlayerWidthCount => _PlayerWidthCount;

        [ShowIf("AutoCreatePlayerBlock")]
        [Header("玩家戰鬥區域寬度")]
        [PropertyTooltip("直向的格子有幾格")]
        [SerializeField] private int _PlayerHeightCount;
        public int PlayerHeightCount => _PlayerHeightCount;

        [ShowIf("AutoCreatePlayerBlock")]
        [PropertyTooltip("打勾後就可以設定自動生成玩家戰鬥區域的各項參數")]
        [Header("是否自訂生成玩家戰鬥區域")]
        [SerializeField] private bool CustomCreatePlayerBlock;

        [ShowIf("CustomCreatePlayerBlock")]
        [Header("玩家戰鬥區域起始位置")]
        [PropertyTooltip("格子的起始位置, 原本的初始值是X:1.22, Y:3.88")]
        [SerializeField] private Vector2 PlayerBlockStartPosition = new Vector2(-1.22f, 3.88f);

        [ShowIf("CustomCreatePlayerBlock")]
        [Header("是否自訂戰鬥區域間隔")]
        [PropertyTooltip("打勾後就可以自己設定格子之間的間隔")]
        [SerializeField] private bool CustomPlayerPadding;

        [ShowIf("@CustomPlayerPadding && CustomCreatePlayerBlock")]
        [Header("玩家戰鬥區域寬度間隔")]
        [PropertyTooltip("格子之間的左右間隔, 原本的初始值是2.08")]
        [SerializeField] private float PlayerWidthPadding = 2.08f;

        [ShowIf("@CustomPlayerPadding && CustomCreatePlayerBlock")]
        [Header("玩家戰鬥區域高度間隔")]
        [PropertyTooltip("格子之間的上下間隔, 原本的初始值是0.9")]
        [SerializeField] private float PlayerHeightPadding = 0.9f;

        [ShowIf("CustomCreatePlayerBlock")]
        [Header("是否自訂玩家戰鬥區域大小")]
        [PropertyTooltip("打勾後就可以自己設定每個格子的大小")]
        [SerializeField] private bool CustomPlayerScaling;

        [ShowIf("@CustomPlayerScaling && CustomCreatePlayerBlock")]
        [Header("玩家戰鬥區域單格寬度")]
        [PropertyTooltip("單一格子的寬")]
        [SerializeField] private float PlayerBlockWidth;

        [ShowIf("@CustomPlayerScaling && CustomCreatePlayerBlock")]
        [Header("玩家戰鬥區域單格高度")]
        [PropertyTooltip("單一格子的高")]
        [SerializeField] private float PlayerBlockHeight;
        #endregion

        #region EnemyField
        [Header("是否自動生成敵人戰鬥區域")]
        [PropertyTooltip("打勾表示遊戲開始時會自動生成，如果沒有則表示需要預先在場景中部署敵人戰鬥區域的格子")]
        [SerializeField] private bool AutoCreateEnemyBlock;

        [ShowIf("AutoCreateEnemyBlock")]
        [Header("敵人戰鬥區域長度")]
        [PropertyTooltip("橫向的格子有幾格")]
        [SerializeField] private int _EnemyWidthCount;
        public int EnemyWidthCount => _EnemyWidthCount;

        [ShowIf("AutoCreateEnemyBlock")]
        [Header("敵人戰鬥區域寬度")]
        [PropertyTooltip("直向的格子有幾格")]
        [SerializeField] private int _EnemyHeightCount;
        public int EnemyHeightCount => _EnemyHeightCount;

        [ShowIf("AutoCreateEnemyBlock")]
        [PropertyTooltip("打勾後就可以設定自動生成敵人戰鬥區域的各項參數")]
        [Header("是否自訂生成敵人戰鬥區域")]
        [SerializeField] private bool CustomCreateEnemyBlock;

        [ShowIf("CustomCreateEnemyBlock")]
        [Header("敵人戰鬥區域起始位置")]
        [PropertyTooltip("格子的起始位置, 原本的初始值是X:1.22, Y:3.88")]
        [SerializeField] private Vector2 EnemyBlockStartPosition = new Vector2(-1.22f, 3.88f);

        [ShowIf("CustomCreateEnemyBlock")]
        [Header("是否自訂敵人戰鬥區域間隔")]
        [PropertyTooltip("打勾後就可以自己設定格子之間的間隔")]
        [SerializeField] private bool CustomEnemyPadding;

        [ShowIf("@CustomEnemyPadding && CustomCreateEnemyBlock")]
        [Header("敵人戰鬥區域寬度間隔")]
        [PropertyTooltip("格子之間的左右間隔, 原本的初始值是2.08")]
        [SerializeField] private float EnemyWidthPadding = 2.08f;

        [ShowIf("@CustomEnemyPadding && CustomCreateEnemyBlock")]
        [Header("敵人戰鬥區域高度間隔")]
        [PropertyTooltip("格子之間的上下間隔, 原本的初始值是0.9")]
        [SerializeField] private float EnemyHeightPadding = 0.9f;

        [ShowIf("CustomCreateEnemyBlock")]
        [Header("是否自訂敵人戰鬥區域大小")]
        [PropertyTooltip("打勾後就可以自己設定每個格子的大小")]
        [SerializeField] private bool CustomEnemyScaling;

        [ShowIf("@CustomEnemyScaling && CustomCreateEnemyBlock")]
        [Header("敵人戰鬥區域單格寬度")]
        [PropertyTooltip("單一格子的寬")]
        [SerializeField] private float EnemyBlockWidth;

        [ShowIf("@CustomEnemyScaling && CustomCreateEnemyBlock")]
        [Header("敵人戰鬥區域單格高度")]
        [PropertyTooltip("單一格子的高")]
        [SerializeField] private float EnemyBlockHeight;
        #endregion

        private readonly FieldBlock[][] blocks = new FieldBlock[(int)ETeam.count][];

        private readonly HashSet<FieldBlock>[] availableBlocks = new HashSet<FieldBlock>[(int)ETeam.count];

        private List<Transform> PlayerFieldTransformList;
        private List<Transform> EnemyFieldTransformList;

        private int _PlayerFieldCount;
        public int PlayerFieldCount => _PlayerFieldCount;
        private int _EnemyFieldCount;
        public int EnemyFieldCount => _EnemyFieldCount;

        protected override void Awake()
        {
            base.Awake();
            PlayerFieldTransformList = new List<Transform>();
            EnemyFieldTransformList = new List<Transform>();
            CheckBlockInScene();
            if (AutoCreatePlayerBlock) {
                _PlayerFieldCount = _PlayerWidthCount * _PlayerHeightCount;
                if (CustomCreatePlayerBlock) {
                    PlayerFieldBlockStartLocation = PlayerBlockStartPosition;
                    if (CustomPlayerPadding) {
                        PlayerFieldBlockInterval = new Vector2(PlayerWidthPadding, PlayerHeightPadding);
                    }

                    if (CustomPlayerScaling) {
                        PlayerFieldBlockScale = new Vector3(PlayerBlockWidth, PlayerBlockHeight, 1);
                    }
                }
                else {
                    if (_PlayerWidthCount < 4) {
                        PlayerFieldBlockStartLocation += new Vector2(0.98f, -0.38f) * (4 - _PlayerWidthCount);
                    }
                }
            }

            if (AutoCreateEnemyBlock) {
                _EnemyFieldCount = _EnemyWidthCount * _EnemyHeightCount;
                if (CustomCreateEnemyBlock) {
                    EnemyFieldBlockStartLocation = EnemyBlockStartPosition;
                    if (CustomEnemyPadding) {
                        EnemyFieldBlockInterval = new Vector2(EnemyWidthPadding, EnemyHeightPadding);
                    }

                    if (CustomPlayerScaling) {
                        EnemyFieldBlockScale = new Vector3(EnemyBlockWidth, EnemyBlockHeight, 1);
                    }
                }
                else {
                    if (_EnemyWidthCount < 4) {
                        EnemyFieldBlockStartLocation += new Vector2(-0.98f, -0.38f) * (4 - _EnemyWidthCount);
                    }
                }
            }

            if (_EnemyFieldCount == 0 || _PlayerFieldCount == 0) {
                Debug.LogError("沒有自動或手動佈署玩家跟敵人的戰鬥區域，請檢查後再重新開始遊戲");
                return;
            }

            for (int i = 0; i < (int)ETeam.count; i++)
            {
                blocks[i] = new FieldBlock[i == 0 ? _PlayerFieldCount : _EnemyFieldCount];
                availableBlocks[i] = new HashSet<FieldBlock>();
            }

            // Reference to FieldColliders
            FieldCollider[] colliders = new FieldCollider[_PlayerWidthCount];
            // Spawn player's field
            for (int y = 0; y < _PlayerHeightCount; y++)
            {
                for (int x = 0; x < _PlayerWidthCount; x++)
                {
                    Vector3 pos = (Vector3)PlayerFieldBlockStartLocation + new Vector3(x * PlayerFieldBlockInterval.x, y * -PlayerFieldBlockInterval.y, -y / 100.0f);
                    GameObject blockObj = Instantiate(playerFieldBlockPrefab, pos, Quaternion.identity);
                    blockObj.transform.localScale = PlayerFieldBlockScale;
                    var block = blockObj.GetComponent<FieldBlock>();
                    block.Index = y * _PlayerWidthCount + x;
                    block.Team = ETeam.player;
                    block.RowIndex = (uint)y;

                    int teamNo = (int)ETeam.player;
                    blocks[teamNo][block.Index] = block;
                    availableBlocks[teamNo].Add(block);

                    // Create field's collider for one row
                    if (y == 0)
                    {
                        Vector3 colliderPos = new(pos.x, 0.5f * ProjectProperty.baseResolution.y / 100.0f, 0);
                        GameObject colliderObject = Instantiate(fieldColliderPrefab, colliderPos, Quaternion.identity);
                        colliderObject.layer = LayerMask.NameToLayer("PlayerField");
                        colliders[x] = colliderObject.GetComponent<FieldCollider>();
                    }

                    if (colliders[x] != null)
                    {
                        colliders[x].SetBlockMap((uint)y, block);
                    }
                }
            }

            // Reset reference of FieldCollider
            for (int i = 0; i < _PlayerWidthCount; i++)
            {
                colliders[i] = null;
            }

            // Spawn Enemy's field
            for (int y = 0; y < _EnemyHeightCount; y++)
            {
                for (int x = 0; x < _EnemyWidthCount; x++)
                {
                    Vector3 pos = new Vector3(EnemyFieldBlockStartLocation.x, EnemyFieldBlockStartLocation.y) + new Vector3(ProjectProperty.baseResolution.x/100.0f + x * EnemyFieldBlockInterval.x - (_EnemyWidthCount - 1) * EnemyFieldBlockInterval.x, y * -EnemyFieldBlockInterval.y, -y / 100.0f);
                    GameObject blockObj = Instantiate(enemyFieldBlockPrefab, pos, Quaternion.identity);
                    blockObj.transform.localScale = EnemyFieldBlockScale;
                    var block = blockObj.GetComponent<FieldBlock>();
                    block.Index = y * _EnemyWidthCount + x;
                    block.Team = ETeam.enemy;
                    block.RowIndex = (uint)y;

                    int teamNo = (int)ETeam.enemy;
                    blocks[teamNo][block.Index] = block;
                    availableBlocks[teamNo].Add(block);

                    // Create field's collider for one row
                    if (y == 0)
                    {
                        Vector3 colliderPos = new(pos.x, 0.5f * ProjectProperty.baseResolution.y / 100.0f, 0);
                        GameObject colliderObject = Instantiate(fieldColliderPrefab, colliderPos, Quaternion.identity);
                        colliderObject.layer = LayerMask.NameToLayer("EnemyField");
                        colliders[x] = colliderObject.GetComponent<FieldCollider>();
                    }

                    if (colliders[x] != null)
                    {
                        colliders[x].SetBlockMap((uint)y, block);
                    }
                }
            }
        }

        void CheckBlockInScene() {
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Block");
            for (int i = 0; i < temp.Length; i++) {
                if (temp[i].GetComponent<FieldBlock>()) {
                    switch (temp[i].GetComponent<FieldBlock>().FieldBlockType) {
                        case BlockType.Player:
                            PlayerFieldTransformList.Add(temp[i].transform);
                            break;

                        case BlockType.Enemy:
                            EnemyFieldTransformList.Add(temp[i].transform);
                            break;
                    }
                }
            }

            if (PlayerFieldTransformList.Count != 0) {
                PlayerFieldTransformList = PlayerFieldTransformList.OrderBy(x => x.position.x).OrderBy(y => y.position.y).ToList();
                _PlayerFieldCount = PlayerFieldTransformList.Count;
            }

            if (EnemyFieldTransformList.Count != 0) {
                EnemyFieldTransformList = EnemyFieldTransformList.OrderBy(x => x.position.x).OrderBy(y => y.position.y).ToList();
                _EnemyFieldCount = EnemyFieldTransformList.Count;
            }
        }

        public FieldBlock GetAdjacentBlock(in FieldBlock currentBlock, EMoveDirection direction)
        {
            if (currentBlock == null)
            {
                return null;
            }

            // Try to get target block depends on moving direction
            switch (direction)
            {
                case EMoveDirection.Upper:
                    return GetUpBlock(currentBlock);

                case EMoveDirection.Lower:
                    return GetDownBlock(currentBlock);

                case EMoveDirection.Left:
                    return GetLeftBlock(currentBlock);

                case EMoveDirection.Right:
                    return GetRightBlock(currentBlock);

                case EMoveDirection.UpperLeft:
                    return GetUpperLeftBlock(currentBlock);

                case EMoveDirection.UpperRight:
                    return GetUpperRightBlock(currentBlock);

                case EMoveDirection.LowerLeft:
                    return GetLowerLeftBlock(currentBlock);

                case EMoveDirection.LowerRight:
                    return GetLowerRightBlock(currentBlock);
            }

            // No block can move
            return null;
        }

        public FieldBlock GetBlock(ETeam team, int index, BlockType Type)
        {
            if (index < 0 || index >= (Type == BlockType.Player ? _PlayerFieldCount : _EnemyFieldCount))
            {
                return null;
            }

            return blocks[(int)team][index];
        }

        public FieldBlock GetUpBlock(in FieldBlock currentBlock)
        {
            if(!currentBlock)
            {
                return null;
            }

            int upBlockIndex = currentBlock.Index - _PlayerWidthCount;
            return GetBlock(currentBlock.Team, upBlockIndex, currentBlock.FieldBlockType);
        }

        public FieldBlock GetDownBlock(in FieldBlock currentBlock)
        {
            if (!currentBlock)
            {
                return null;
            }

            int downBlockIndex = currentBlock.Index + _PlayerWidthCount;
            return GetBlock(currentBlock.Team, downBlockIndex, currentBlock.FieldBlockType);
        }

        public FieldBlock GetLeftBlock(in FieldBlock currentBlock)
        {
            if (!currentBlock)
            {
                return null;
            }

            int modIndex = currentBlock.Index % _PlayerWidthCount;
            if (modIndex == 0)
            {
                return null;
            }

            int leftBlockIndex = currentBlock.Index - 1;
            return GetBlock(currentBlock.Team, leftBlockIndex, currentBlock.FieldBlockType);
        }

        public FieldBlock GetRightBlock(in FieldBlock currentBlock)
        {
            if (!currentBlock)
            {
                return null;
            }

            int modIndex = currentBlock.Index % _PlayerWidthCount;
            if (modIndex == (_PlayerWidthCount - 1))
            {
                return null;
            }

            int rightBlockIndex = currentBlock.Index + 1;
            return GetBlock(currentBlock.Team, rightBlockIndex, currentBlock.FieldBlockType);
        }

        public FieldBlock GetUpperLeftBlock(in FieldBlock currentBlock)
        {
            return GetUpBlock(GetLeftBlock(currentBlock));
        }

        public FieldBlock GetUpperRightBlock(in FieldBlock currentBlock)
        {
            return GetUpBlock(GetRightBlock(currentBlock));
        }

        public FieldBlock GetLowerLeftBlock(in FieldBlock currentBlock)
        {
            return GetDownBlock(GetLeftBlock(currentBlock));
        }

        public FieldBlock GetLowerRightBlock(in FieldBlock currentBlock)
        {
            return GetDownBlock(GetRightBlock(currentBlock));
        }

        public int GetAvailableBlockCount(ETeam team)
        {
            if (team != ETeam.none && team != ETeam.count)
            {
                return availableBlocks[(int)team].Count;
            }
            return 0;
        }

        public List<FieldBlock> GetAvailaleBlocks(ETeam team)
        {
            if (team != ETeam.none && team != ETeam.count)
            {
                return availableBlocks[(int)team].ToList();
            }
            return default;
        }

        public FieldBlock PickOneAvailableBlockRandomly(ETeam team)
        {
            List<FieldBlock> blocks = GetAvailaleBlocks(team);
            if (blocks != null && blocks.Count > 0)
            {
                int blockNo = Random.Range(0, blocks.Count - 1);
                return blocks[blockNo];
            }
            return null;
        }

        public void OnObjectEnteredBlock(FieldBlock block)
        {
            if (block.Team != ETeam.none && block.Team != ETeam.count)
            {
                availableBlocks[(int)block.Team].Remove(block);
            }
        }

        public void OnObjectExitedBlock(FieldBlock block)
        {
            if (block.Team != ETeam.none && block.Team != ETeam.count)
            {
                availableBlocks[(int)block.Team].Add(block);
            }
        }

        public void DisableAllBlock() {
            foreach (var block in blocks) {
                foreach(var obj in block) {
                    obj.gameObject.SetActive(false);
                }
            }
        }

        public void ShowAllBlock() {
            foreach (var block in blocks)
            {
                foreach (var obj in block)
                {
                    obj.gameObject.SetActive(true);
                }
            }
        }
    }
}
