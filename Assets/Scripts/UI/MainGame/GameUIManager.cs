using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class GameUIManager : Singleton<GameUIManager>
    {
        [SerializeField]
        private Canvas mainCanvas;

        [SerializeField]
        private UIPause pause;

        [SerializeField]
        private UIResult result;

        private CharacterLifeUIPool characterLifeUIPool;

        public Canvas MainCanvas => mainCanvas;

        public UIPause Pause => pause;

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

        public void SetPauseActive(bool value)
        {
            if (pause)
            {
                pause.gameObject.SetActive(value);
            }
        }

        public void TogglePause()
        {
            if (pause)
            {
                pause.gameObject.SetActive(!pause.gameObject.activeInHierarchy);
            }
        }

        public void SetResultActive(bool value)
        {
            if (result)
            {
                result.gameObject.SetActive(value);
            }
        }
    }
}
