using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class ItemManager : Singleton<ItemManager>
    {
        [System.Serializable]
        struct DropWeightParm
        {
            public int itemID;
            public float weight;
        }

        [SerializeField]
        private List<GameObject> itemPrefabs;

        [SerializeField]
        private float itemDropTimeInterval = 30.0f;

        [SerializeField]
        private int itemDropScoreInterval = 500;

        [SerializeField]
        private List<DropWeightParm> timeDropWeightTable;

        [SerializeField]
        private List<DropWeightParm> scoreDropWeightTable;

        private Timer timer;

        private int nextDropItemScore = 0;

        private Dictionary<ItemBase, FieldBlock> itemFieldMap;

        protected override void Awake()
        {
            base.Awake();
            nextDropItemScore = itemDropScoreInterval;
            timer = new Timer(itemDropTimeInterval);
            itemFieldMap = new Dictionary<ItemBase, FieldBlock>();
            ScoreManager.Instance.CurrentScoreChanged += OnScoreChanged;
        }

        private void Update()
        {
            timer.Step();
            if (timer.IsComplete)
            {
                DropItem(timeDropWeightTable);
                timer.Reset();
            }
        }

        private void DropItem(List<DropWeightParm> weightParam)
        {
            // Find a available block to drop.
            FieldBlock block = FieldManager.Instance.PickOneAvailableBlockRandomly(ETeam.player);
            if (!block)
            {
                return;
            }

            // Determine what type of item to drop.
            int itemID = DrawItemType(weightParam);
            if (itemID >= itemPrefabs.Count)
            {
                return;
            }

            // Instantiate the item.
            GameObject itemObj = Instantiate(itemPrefabs[itemID]);
            ItemBase item = itemObj ? itemObj.GetComponent<ItemBase>() : null;

            // Drop the item to the block.
            if (item)
            {
                block.OnItemDropped(item);
                itemFieldMap.Add(item, block);
            }
        }

        private int DrawItemType(List<DropWeightParm> weightParam)
        {
            if (weightParam.Count == 0)
            {
                return 0;
            }

            float weightSum = 0;
            for (int i = 0; i < weightParam.Count; i++)
            {
                weightSum += weightParam[i].weight;
            }

            float random = UnityEngine.Random.Range(0, weightSum);
            float currentWeight = 0;
            for (int i = 0; i < weightParam.Count; i++)
            {
                currentWeight += weightParam[i].weight;
                if (random < currentWeight)
                {
                    return weightParam[i].itemID;
                }
            }

            return weightParam[^1].itemID;
        }

        public void OnItemApplied(ItemBase item)
        {
            if (item is AttackItem)
            {
                if (GameUIManager.IsCreated)
                {
                    GameUIManager.Instance.ItemHander.CountupAttackItem();
                }
            }
            else if (item is SpeedItem)
            {
                if (GameUIManager.IsCreated)
                {
                    GameUIManager.Instance.ItemHander.CountupSpeedItem();
                }
            }

            OnItemExitField(item);
        }

        private void OnScoreChanged(int newScore)
        {
            if (newScore >= nextDropItemScore)
            {
                DropItem(scoreDropWeightTable);
                nextDropItemScore += itemDropScoreInterval;
            }
        }

        public void OnItemExitField(ItemBase item, bool removeFormMap = true)
        {
            if (itemFieldMap.ContainsKey(item))
            {
                itemFieldMap[item].OnItemExit();
                if (removeFormMap)
                {
                    itemFieldMap.Remove(item);
                }
            }
        }

        public void OnNewGameStarted()
        {
            foreach (ItemBase item in itemFieldMap.Keys)
            {
                OnItemExitField(item, false);
                Destroy(item.gameObject);
            }

            itemFieldMap.Clear();
        }
    }
}
