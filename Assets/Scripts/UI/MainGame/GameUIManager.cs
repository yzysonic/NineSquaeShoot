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

        [SerializeField]
        private UIPlayerCharacter playerCharacterUI;

        [SerializeField]
        private UIItemHandler itemHandler;

        private CharacterLifeUIPool characterLifeUIPool;

        public Canvas MainCanvas => mainCanvas;

        public UIPause Pause => pause;

        public UIItemHandler ItemHander => itemHandler;

        protected override void Awake()
        {
            base.Awake();
            characterLifeUIPool = GetComponent<CharacterLifeUIPool>();
        }

        public void BindCharacterLifeUI(Character character)
        {
            if(character is Player)
            {
                if (playerCharacterUI)
                {
                    playerCharacterUI.gameObject.SetActive(true);
                    playerCharacterUI.OwnerCharacter = character;
                }
            }
            else
            {
                if (characterLifeUIPool)
                {
                    characterLifeUIPool.BindCharacter(character);
                }
            }
        }

        public void UnbindCharacterLifeUI(Character character)
        {
            if (character is Player)
            {
                if (playerCharacterUI)
                {
                    playerCharacterUI.OwnerCharacter = null;
                    playerCharacterUI.gameObject.SetActive(false);
                }
            }
            else
            {
                if (characterLifeUIPool)
                {
                    characterLifeUIPool.UnbindCharacter(character);
                }
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
            if (result && result.gameObject.activeInHierarchy)
            {
                // Do not allow pause when result is displaying
                return;
            }

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

        public void OnNewGameStarted()
        {
            if (ItemHander)
            {
                ItemHander.ResetItemCount();
            }
        }
    }
}
