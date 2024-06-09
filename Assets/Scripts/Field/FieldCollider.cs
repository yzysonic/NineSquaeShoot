using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class FieldCollider : MonoBehaviour
    {
        private FieldBlock[] PlayerBlockMap { get; set; }/* = new FieldBlock[FieldManager.Instance.PlayerFieldCount];*/

        private FieldBlock[] EnemyBlockMap { get; set; }/* = new FieldBlock[FieldManager.Instance.EnemyFieldCount];*/

        void OnEnable() {
            PlayerBlockMap = new FieldBlock[FieldManager.Instance.PlayerFieldCount];
            EnemyBlockMap = new FieldBlock[FieldManager.Instance.EnemyFieldCount];
        }

        public void SetBlockMap(uint rowIndex, FieldBlock fieldBlock) {
            switch (fieldBlock.FieldBlockType) {
                case BlockType.Player:
                    if (PlayerBlockMap != null && rowIndex < FieldManager.Instance.PlayerWidthCount) {
                        PlayerBlockMap[rowIndex] = fieldBlock;
                    }
                    break;

                case BlockType.Enemy:
                    if (EnemyBlockMap != null && rowIndex < FieldManager.Instance.EnemyWidthCount) {
                        EnemyBlockMap[rowIndex] = fieldBlock;
                    }
                    break;
            }
        }

        public FieldBlock GetBlock(uint rowIndex)
        {
            if (PlayerBlockMap[0] != null) {
                if (rowIndex < FieldManager.Instance.PlayerWidthCount) {
                    return PlayerBlockMap[rowIndex];
                }
            }

            if (EnemyBlockMap[0] != null) {
                if (rowIndex < FieldManager.Instance.EnemyWidthCount) {
                    return EnemyBlockMap[rowIndex];
                }
            }
            return null;
        }
    }
}
