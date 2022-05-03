using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public enum ETeam
    {
        none = -1,
        player,
        enemy,
        count
    }

    public class Character : MonoBehaviour
    {
        public FieldBlock StayingBlock
        {
            get => stayingBlock;
            set
            {
                if (stayingBlock != null)
                {
                    stayingBlock.Exit();
                }
                stayingBlock = value;
            }
        }

        private FieldBlock stayingBlock;
    }

}