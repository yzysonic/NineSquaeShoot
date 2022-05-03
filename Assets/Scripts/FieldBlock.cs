using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class FieldBlock : MonoBehaviour
    {
        public int Index { get; set; } = 0;
        public ETeam Team { get; set; } = ETeam.none;
        public Character StayingCharacter { get; private set; }

        public bool TryEnter(Character character)
        {
            if (character == null || StayingCharacter != null)
            {
                return false;
            }

            StayingCharacter = character;
            return true;
        }

        public void Exit()
        {
            StayingCharacter = null;
        }
    }
}
