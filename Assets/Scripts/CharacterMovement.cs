using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class CharacterMovement : MonoBehaviour
    {
        private Character character;

        private void Awake()
        {
            character = GetComponent<Character>();
        }

        public bool TryMove(Vector2 direction)
        {
            FieldBlock targetBlock = FieldManager.Instance.GetAdjacentBlock(character.StayingBlock, direction);
            if(targetBlock == null)
            {
                return false;
            }

            return TryEnterBlock(targetBlock);
        }

        public bool TryEnterBlock(FieldBlock block)
        {
            if (block == null)
            {
                return false;
            }

            if (block.TryEnter(character))
            {
                character.StayingBlock = block;
                transform.position = block.transform.position;
                return true;
            }

            return false;
        }
    }
}
