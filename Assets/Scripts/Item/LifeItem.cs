using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class LifeItem : ItemBase
    {
        [SerializeField]
        private uint addLifeValue = 10;

        public override void ApplyToCharacter(Character character)
        {
            if (character && character.Life)
            {
                character.Life.Recover(addLifeValue);
            }
            base.ApplyToCharacter(character);
        }
    }
}
