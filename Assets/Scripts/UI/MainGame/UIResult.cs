using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NSS
{
    public class UIResult : UIMenu
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
        private Text timeValue;

        [SerializeField]
        private Button mainMenuButton;

        [SerializeField]
        private Button playAginButton;

        protected override void Awake()
        {
            base.Awake();

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
            timeValue.text = System.TimeSpan.FromSeconds(MainGameTimer.Instance.Elapsed).ToString(@"mm\:ss");
            playAginButton.Select();
        }

        private void OnDisable()
        {
            background.SetActive(false);
            scoreValue.text = string.Empty;
            rankValue.text = string.Empty;
            
            if (EventSystem.current)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
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
