using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Player : Character
    {
        protected override void Awake()
        {
            base.Awake();
            Team = ETeam.player;
        }

        public void OnNewGameStarted()
        {
            gameObject.SetActive(true);
            ResetStatus();
            GameUIManager.Instance.BindCharacterLifeUI(this);
        }

        protected override void OnDefeated()
        {
            base.OnDefeated();
            GameUIManager.Instance.UnbindCharacterLifeUI(this);
            gameObject.SetActive(false);
        }
    }
}
