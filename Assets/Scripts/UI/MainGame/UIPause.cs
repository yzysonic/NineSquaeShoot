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

        [SerializeField]
        private Slider MasterSlider;

        [SerializeField]
        private Slider BGMSlider;

        [SerializeField]
        private Slider EffectSlider;

        [SerializeField]
        private float overrideBGMVolume = -10;

        private float originalBGMVolume = 0.0f;

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

            if (MasterSlider)
            {
                MasterSlider.value = PlayerPrefs.GetFloat("MasterSoundVolume", -15);
                MasterSlider.onValueChanged.AddListener(OnMasterSliderValueChange);
            }

            if (BGMSlider)
            {
                BGMSlider.value = PlayerPrefs.GetFloat("BGMSoundVolume", -15);
                BGMSlider.onValueChanged.AddListener(OnBGMSliderValueChange);
            }

            if (EffectSlider)
            {
                EffectSlider.value = PlayerPrefs.GetFloat("EffectSoundVolume", -15);
                EffectSlider.onValueChanged.AddListener(OnEffectSliderrValueChange);
            }

            base.Awake();
        }

        private void OnEnable()
        {
            GamePause.SetPause(true);
            resumeButton.Select();
            background.SetActive(true);
            PauseStateChanged?.Invoke(true);


            GlobalAsset.Instance.MainAudioMixer.GetFloat("BGM", out originalBGMVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM", overrideBGMVolume);
        }

        private void OnDisable()
        {
            GamePause.SetPause(false);
            background.SetActive(false);
            PauseStateChanged?.Invoke(false);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM", originalBGMVolume);

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

        private void OnMasterSliderValueChange(float value)
        {
            GlobalAsset.Instance.SetMasterVolume(value);
        }

        private void OnBGMSliderValueChange(float value)
        {
            GlobalAsset.Instance.SetBGMVolume(value);
        }

        private void OnEffectSliderrValueChange(float value)
        {
            GlobalAsset.Instance.SetEffectVolume(value);
        }
    }
}
