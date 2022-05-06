using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class CharacterLifeUIPool : Pool<UICharacterLife>
    {
        [SerializeField]
        private GameObject CharacterLifeUIPrefab;

        private readonly Dictionary<Character, UICharacterLife> bindMap = new();

        protected override void Awake()
        {
            prefab = CharacterLifeUIPrefab;
            maxCount = FieldManager.teamBlockCount * (int)ETeam.count;
            attachTransform = GameUIManager.Instance.MainCanvas.transform;
            base.Awake();
        }

        public void BindCharacter(Character character)
        {
            if (!character)
            {
                return;
            }
            UICharacterLife ui = GetAvailablePoolObject();
            if (ui)
            {
                ui.OwnerCharacter = character;
                bindMap.Add(character, ui);
            }
        }

        public void UnbindCharacter(Character character)
        {
            if (!bindMap.ContainsKey(character))
            {
                return;
            }

            UICharacterLife ui = bindMap[character];
            if (ui)
            {
                ui.OwnerCharacter = null;
                ui.IsUsing = false;
            }
        }
    }
}
