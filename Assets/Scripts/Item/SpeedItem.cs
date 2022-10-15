using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class SpeedItem : ItemBase
    {
        [SerializeField, Range(0, 100)]
        private float addFireSpeed = 10.0f;

        public override void ApplyToCharacter(Character character)
        {
            if (character && character.Weapon)
            {
                character.Weapon.DecreaseFireInterval(addFireSpeed / 100.0f);
            }
            base.ApplyToCharacter(character);
        }
    }
}
