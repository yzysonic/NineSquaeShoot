using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIResult : MonoBehaviour
    {
        [SerializeField]
        private Text scoreValue;

        [SerializeField]
        private Text rankValue;

        [SerializeField]
        private Button mainMenuButton;

        [SerializeField]
        private Button playAginButton;

        private void Awake()
        {
            if (mainMenuButton)
            {
                mainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
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

        private void OnMainMenuButtonPressed()
        {
            FadeManager.Instance.LoadSceneWithFade("MainMenuScene");
        }

        private void OnPlayAgainButtonPressed()
        {
            GameUIManager.Instance.SetResultActive(false);
            GameManager.Instance.StartNewGame();
        }
    }
}
