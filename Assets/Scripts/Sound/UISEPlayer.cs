using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NSS
{
    [RequireComponent(typeof(AudioSource))]
    public class UISEPlayer : Singleton<UISEPlayer>
    {
        private AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.outputAudioMixerGroup = GlobalAsset.Instance.SEMixerGroup;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void PlayOneShot(AudioClip clip)
        {
            if (audioSource)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        void Update()
        {
            float effectVolume = PlayerPrefs.GetFloat("EffectSoundVolume", 5);

            GlobalAsset.Instance.MainAudioMixer.SetFloat("GameSE", effectVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("UISE", effectVolume);
        }
    }
}
