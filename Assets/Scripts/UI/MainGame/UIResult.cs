using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace NSS
{
    public class UIResult : MonoBehaviour
    {
        [SerializeField]
        private Text scoreValue;

        [SerializeField]
        private Text rankValue;

        [SerializeField]
        private Button backToMainMenuButton;

        [SerializeField]
        private Button playAginButton;

        private void Awake()
        {
            if (backToMainMenuButton)
            {
                backToMainMenuButton.onClick.AddListener(OnBackToMainMenuButtonPressed);
            }
            if (playAginButton)
            {
                playAginButton.onClick.AddListener(OnPlayAgainButtonPressed);
            }
        }

        private void OnEnable()
        {
            int score = ScoreManager.Instance.CurrentScore;
            scoreValue.text = score.ToString("N0");
            rankValue.text = ResultManager.Instance.ScoreRankProfile.FindRank(score).ToString();
        }

        private void OnDisable()
        {
            scoreValue.text = string.Empty;
            rankValue.text = string.Empty;
        }

        private void OnBackToMainMenuButtonPressed()
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        private void OnPlayAgainButtonPressed()
        {
            GameUIManager.Instance.SetResultActive(false);
            GameManager.Instance.StartNewGame();
        }
    }
}
