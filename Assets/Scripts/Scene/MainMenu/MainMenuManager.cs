using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class MainMenuManager : MonoBehaviour
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

        private void Awake()
        {
            if (startGameButton)
            {
                startGameButton.onClick.AddListener(OnStartGameButtonPressed);
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
        }

        private void OnStartGameButtonPressed()
        {
            FadeManager.Instance.LoadSceneWithFade("MainGameScene");
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
