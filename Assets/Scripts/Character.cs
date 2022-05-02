using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public enum ETeam
    {
        player,
        enemy,
        count
    }

    public class Character : MonoBehaviour
    {
        public FieldBlock StayingBlock
        {
            get
            {
                return stayingBlock;
            }
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