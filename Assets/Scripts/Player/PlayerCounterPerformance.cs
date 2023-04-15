using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace NSS
{
    public class PlayerCounterPerformance : MonoBehaviour
    {
        [SerializeField]
        private float slowScale = 0.3f;

        [SerializeField]
        private float slowTrasitionDuration = 0.3f;

        [SerializeField]
        private float BGMLowpassFreq = 450.0f;

        [SerializeField]
        private float BGMVolume = -10.0f;

        private bool isStartSlowPerformance = false;
        private Timer slowTransitionTimer = new();
        private Animator playerAnimator;
        private AudioSource slowEffectSound;

        private float originalBGMLowpassFreq = 0.0f;
        private float originalBGMVolume = 0.0f;


        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();

            if (VolumeManager.Instance.PlayerCounterEffectVolume)
            {
                slowEffectSound = VolumeManager.Instance.PlayerCounterEffectVolume.GetComponent<AudioSource>();
            }
        }

        public void StartSlowPerformance()
        {
            slowTransitionTimer.Reset(slowTrasitionDuration);
            isStartSlowPerformance = true;

            if (VolumeManager.Instance.PlayerCounterEffectVolume)
            {
                if (slowEffectSound)
                {
                    slowEffectSound.Play();
                }
            }

            GlobalAsset.Instance.MainAudioMixer.GetFloat("BGM", out originalBGMVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM", BGMVolume);
            GlobalAsset.Instance.MainAudioMixer.GetFloat("BGM_LowpassFreq", out originalBGMLowpassFreq);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM_LowpassFreq", BGMLowpassFreq);
            Debug.Log("StartSlowPerformance");
        }

        public void StopSlowPerformance()
        {
            isStartSlowPerformance = false;
            SetTimeScale(1.0f);
            SetPostEffects(0.0f);
            if (slowEffectSound)
            {
                slowEffectSound.Stop();
            }

            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM", originalBGMVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM_LowpassFreq", originalBGMLowpassFreq);
            Debug.Log("StopSlowPerformance");
        }

        private void Update()
        {
            if (GamePause.IsPaused)
            {
                return;
            }

            if (isStartSlowPerformance)
            {
                slowTransitionTimer.Step(Time.unscaledDeltaTime);
                SetTimeScale(Mathf.Lerp(1.0f, slowScale, slowTransitionTimer.Progress));
                SetPostEffects(slowTransitionTimer.Progress);

                if (slowTransitionTimer.IsComplete)
                {
                    isStartSlowPerformance = false;
                }
            }
        }

        private void SetTimeScale(float scale)
        {
            // Set Unity time scale.
            Time.timeScale = scale;

            // Set player animator speed to keep normal speed.
            if (playerAnimator)
            {
                playerAnimator.speed = 1.0f / scale;
            }

            // Set sound playback spped.
            //GlobalAsset.Instance.MainAudioMixer.SetFloat("GameSE_Pitch", scale);
        }

        private void SetPostEffects(float weight)
        {
            if (VolumeManager.Instance.PlayerCounterEffectVolume)
            {
                VolumeManager.Instance.PlayerCounterEffectVolume.weight = weight;
            }
        }
    }
}
