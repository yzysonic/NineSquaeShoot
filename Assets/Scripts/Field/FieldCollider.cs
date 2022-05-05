using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class FieldCollider : MonoBehaviour
    {
        private FieldBlock[] BlockMap { get; set; } = new FieldBlock[FieldManager.teamBlockSideCount];

        public void SetBlockMap(uint rowIndex, FieldBlock fieldBlock)
        {
            if (BlockMap != null && rowIndex < FieldManager.teamBlockSideCount)
            {
                BlockMap[rowIndex] = fieldBlock;
            }
        }

        public FieldBlock GetBlock(uint rowIndex)
        {
            if (rowIndex < FieldManager.teamBlockSideCount)
            {
                return BlockMap[rowIndex];
            }
            return null;
        }
    }
}
