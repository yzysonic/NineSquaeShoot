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

        private float soundVolume = 1;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.outputAudioMixerGroup = GlobalAsset.Instance.SEMixerGroup;
            }
            DontDestroyOnLoad(gameObject);

            soundVolume = PlayerPrefs.GetFloat("EffectSoundVolume", 1);
        }

        public void PlayOneShot(AudioClip clip)
        {
            if (audioSource)
            {
                audioSource.volume = soundVolume;
                audioSource.PlayOneShot(clip);
            }
        }

        public void SetSoundValue(float value)
        {
            soundVolume = value;
            PlayerPrefs.SetFloat("EffectSoundVolume", value);
        }
    }
}
