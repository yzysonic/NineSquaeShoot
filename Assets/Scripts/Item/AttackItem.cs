using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class AttackItem : ItemBase
    {
        [SerializeField]
        private uint addAttack = 10;

        public override void ApplyToCharacter(Character character)
        {
            if (character && character.Weapon)
            {
                character.Weapon.AddDamage(addAttack);
            }
            base.ApplyToCharacter(character);
        }
    }
}
