using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIPause : MonoBehaviour
    {
        [SerializeField]
        private GameObject background;

        [SerializeField]
        private Button mainMenuButton;

        [SerializeField]
        private Button resumeButton;

        private void Awake()
        {
            if (mainMenuButton)
            {
                mainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
            }
            if (resumeButton)
            {
                resumeButton.onClick.AddListener(OnResumeButtonPressed);
            }
        }

        private void OnEnable()
        {
            Time.timeScale = 0.0f;
            background.SetActive(true);
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
            background.SetActive(false);
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
