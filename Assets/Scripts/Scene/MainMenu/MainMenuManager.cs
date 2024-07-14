using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NSS
{
    public class MainMenuManager : UIMenu
    {
        [SerializeField]
        private Button startGameButton;

        [SerializeField]
        private Button leaderboardButton;

        [SerializeField]
        private Button exitGameButton;

        [SerializeField]
        private UILeaderboard leaderboard;

        [SerializeField]
        private Text versionInfo;

        [SerializeField]
        private RectTransform LobbyCanvas;

        protected override void Awake()
        {
            if (startGameButton)
            {
                startGameButton.onClick.AddListener(OnStartGameButtonPressed);
                DefaultSelectedButton = (UIMenuButton)startGameButton;
            }
            if (leaderboardButton)
            {
                leaderboardButton.onClick.AddListener(OnLeaderboardButtonPressed);
            }
            if (exitGameButton)
            {
                exitGameButton.onClick.AddListener(OnExitGameButtonPressed);
            }
            if (versionInfo)
            {
                versionInfo.text = $"ver.{Application.version}";
            }

            base.Awake();
        }

        private void OnStartGameButtonPressed()
        {
            /* EventSystem.current.enabled = false;
             FadeManager.Instance.LoadSceneWithFade("MainGameScene");*/
            LobbyCanvas.gameObject.SetActive(true);
            LobbyUIController.Instance.SetLobbyIcon();
        }

        private void OnLeaderboardButtonPressed()
        {
            if (leaderboard)
            {
                leaderboard.gameObject.SetActive(true);
            }
        }

        private void OnExitGameButtonPressed()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
