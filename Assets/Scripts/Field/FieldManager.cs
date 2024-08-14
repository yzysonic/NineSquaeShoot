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
        [Header("�O�_�۰ʥͦ����a�԰��ϰ�")]
        [PropertyTooltip("���Ī�ܹC���}�l�ɷ|�۰ʥͦ��A�p�G�S���h��ܻݭn�w���b���������p���a�԰��ϰ쪺��l")]
        [SerializeField] private bool AutoCreatePlayerBlock;

        [ShowIf("AutoCreatePlayerBlock")]
        [Header("���a�԰��ϰ����")]
        [PropertyTooltip("��V����l���X��")]
        [SerializeField] private int _PlayerWidthCount;
        public int PlayerWidthCount => _PlayerWidthCount;

        [ShowIf("AutoCreatePlayerBlock")]
        [Header("���a�԰��ϰ�e��")]
        [PropertyTooltip("���V����l���X��")]
        [SerializeField] private int _PlayerHeightCount;
        public int PlayerHeightCount => _PlayerHeightCount;

        [ShowIf("AutoCreatePlayerBlock")]
        [PropertyTooltip("���ī�N�i�H�]�w�۰ʥͦ����a�԰��ϰ쪺�U���Ѽ�")]
        [Header("�O�_�ۭq�ͦ����a�԰��ϰ�")]
        [SerializeField] private bool CustomCreatePlayerBlock;

        [ShowIf("CustomCreatePlayerBlock")]
        [Header("���a�԰��ϰ�_�l��m")]
        [PropertyTooltip("��l���_�l��m, �쥻����l�ȬOX:1.22, Y:3.88")]
        [SerializeField] private Vector2 PlayerBlockStartPosition = new Vector2(-1.22f, 3.88f);

        [ShowIf("CustomCreatePlayerBlock")]
        [Header("�O�_�ۭq�԰��ϰ춡�j")]
        [PropertyTooltip("���ī�N�i�H�ۤv�]�w��l���������j")]
        [SerializeField] private bool CustomPlayerPadding;

        [ShowIf("@CustomPlayerPadding && CustomCreatePlayerBlock")]
        [Header("���a�԰��ϰ�e�׶��j")]
        [PropertyTooltip("��l���������k���j, �쥻����l�ȬO2.08")]
        [SerializeField] private float PlayerWidthPadding = 2.08f;

        [ShowIf("@CustomPlayerPadding && CustomCreatePlayerBlock")]
        [Header("���a�԰��ϰ찪�׶��j")]
        [PropertyTooltip("��l�������W�U���j, �쥻����l�ȬO0.9")]
        [SerializeField] private float PlayerHeightPadding = 0.9f;

        [ShowIf("CustomCreatePlayerBlock")]
        [Header("�O�_�ۭq���a�԰��ϰ�j�p")]
        [PropertyTooltip("���ī�N�i�H�ۤv�]�w�C�Ӯ�l���j�p")]
        [SerializeField] private bool CustomPlayerScaling;

        [ShowIf("@CustomPlayerScaling && CustomCreatePlayerBlock")]
        [Header("���a�԰��ϰ���e��")]
        [PropertyTooltip("��@��l���e")]
        [SerializeField] private float PlayerBlockWidth;

        [ShowIf("@CustomPlayerScaling && CustomCreatePlayerBlock")]
        [Header("���a�԰��ϰ��氪��")]
        [PropertyTooltip("��@��l����")]
        [SerializeField] private float PlayerBlockHeight;
        #endregion

        #region EnemyField
        [Header("�O�_�۰ʥͦ��ĤH�԰��ϰ�")]
        [PropertyTooltip("���Ī�ܹC���}�l�ɷ|�۰ʥͦ��A�p�G�S���h��ܻݭn�w���b���������p�ĤH�԰��ϰ쪺��l")]
        [SerializeField] private bool AutoCreateEnemyBlock;

        [ShowIf("AutoCreateEnemyBlock")]
        [Header("�ĤH�԰��ϰ����")]
        [PropertyTooltip("��V����l���X��")]
        [SerializeField] private int _EnemyWidthCount;
        public int EnemyWidthCount => _EnemyWidthCount;

        [ShowIf("AutoCreateEnemyBlock")]
        [Header("�ĤH�԰��ϰ�e��")]
        [PropertyTooltip("���V����l���X��")]
        [SerializeField] private int _EnemyHeightCount;
        public int EnemyHeightCount => _EnemyHeightCount;

        [ShowIf("AutoCreateEnemyBlock")]
        [PropertyTooltip("���ī�N�i�H�]�w�۰ʥͦ��ĤH�԰��ϰ쪺�U���Ѽ�")]
        [Header("�O�_�ۭq�ͦ��ĤH�԰��ϰ�")]
        [SerializeField] private bool CustomCreateEnemyBlock;

        [ShowIf("CustomCreateEnemyBlock")]
        [Header("�ĤH�԰��ϰ�_�l��m")]
        [PropertyTooltip("��l���_�l��m, �쥻����l�ȬOX:1.22, Y:3.88")]
        [SerializeField] private Vector2 EnemyBlockStartPosition = new Vector2(-1.22f, 3.88f);

        [ShowIf("CustomCreateEnemyBlock")]
        [Header("�O�_�ۭq�ĤH�԰��ϰ춡�j")]
        [PropertyTooltip("���ī�N�i�H�ۤv�]�w��l���������j")]
        [SerializeField] private bool CustomEnemyPadding;

        [ShowIf("@CustomEnemyPadding && CustomCreateEnemyBlock")]
        [Header("�ĤH�԰��ϰ�e�׶��j")]
        [PropertyTooltip("��l���������k���j, �쥻����l�ȬO2.08")]
        [SerializeField] private float EnemyWidthPadding = 2.08f;

        [ShowIf("@CustomEnemyPadding && CustomCreateEnemyBlock")]
        [Header("�ĤH�԰��ϰ찪�׶��j")]
        [PropertyTooltip("��l�������W�U���j, �쥻����l�ȬO0.9")]
        [SerializeField] private float EnemyHeightPadding = 0.9f;

        [ShowIf("CustomCreateEnemyBlock")]
        [Header("�O�_�ۭq�ĤH�԰��ϰ�j�p")]
        [PropertyTooltip("���ī�N�i�H�ۤv�]�w�C�Ӯ�l���j�p")]
        [SerializeField] private bool CustomEnemyScaling;

        [ShowIf("@CustomEnemyScaling && CustomCreateEnemyBlock")]
        [Header("�ĤH�԰��ϰ���e��")]
        [PropertyTooltip("��@��l���e")]
        [SerializeField] private float EnemyBlockWidth;

        [ShowIf("@CustomEnemyScaling && CustomCreateEnemyBlock")]
        [Header("�ĤH�԰��ϰ��氪��")]
        [PropertyTooltip("��@��l����")]
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
                Debug.LogError("�S���۰ʩΤ�ʧG�p���a��ĤH���԰��ϰ�A���ˬd��A���s�}�l�C��");
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
