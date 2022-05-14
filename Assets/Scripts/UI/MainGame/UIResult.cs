using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIResult : MonoBehaviour
    {
        [SerializeField]
        private GameObject background;

        [SerializeField]
        private Text scoreValue;

        [SerializeField]
        private Text rankValue;

        [SerializeField]
        private Text placeValue;

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
            background.SetActive(true);
            int score = ScoreManager.Instance.CurrentScore;
            scoreValue.text = score.ToString("N0");
            placeValue.text = Utility.GetPlacementName(ScoreManager.Instance.LastPlacement);
            rankValue.text = ResultManager.Instance.ScoreRankProfile.FindRank(score).ToString();
        }

        private void OnDisable()
        {
            background.SetActive(false);
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
