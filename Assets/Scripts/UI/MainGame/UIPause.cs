using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NSS
{
    public class UIPause : UIMenu
    {
        [SerializeField]
        private GameObject background;
        
        [SerializeField]
        private Button mainMenuButton;

        [SerializeField]
        private Button resumeButton;

        public event Action<bool> PauseStateChanged;

        protected override void Awake()
        {
            if (mainMenuButton)
            {
                mainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
            }
            if (resumeButton)
            {
                resumeButton.onClick.AddListener(OnResumeButtonPressed);
                DefaultSelectedButton = (UIMenuButton)resumeButton;
            }

            base.Awake();
        }

        private void OnEnable()
        {
            Time.timeScale = 0.0f;
            resumeButton.Select();
            background.SetActive(true);
            PauseStateChanged?.Invoke(true);
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
            background.SetActive(false);
            PauseStateChanged?.Invoke(false);

            if (EventSystem.current)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        private void OnMainMenuButtonPressed()
        {
            FadeManager.Instance.LoadSceneWithFade("MainMenuScene");
        }

        private void OnResumeButtonPressed()
        {
            gameObject.SetActive(false);
        }

    }
}
