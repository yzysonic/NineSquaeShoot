using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class GameUIManager : Singleton<GameUIManager>
    {
        [SerializeField]
        private Canvas mainCanvas;

        private CharacterLifeUIPool characterLifeUIPool;

        public Canvas MainCanvas => mainCanvas;

        protected override void Awake()
        {
            base.Awake();
            characterLifeUIPool = GetComponent<CharacterLifeUIPool>();
        }

        public void BindCharacterLifeUI(Character character)
        {
            if (characterLifeUIPool)
            {
                characterLifeUIPool.BindCharacter(character);
            }
        }

        public void UnbindCharacterLifeUI(Character character)
        {
            if (characterLifeUIPool)
            {
                characterLifeUIPool.UnbindCharacter(character);
            }
        }
    }
}
