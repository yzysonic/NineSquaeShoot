using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NSS
{
    [CreateAssetMenu(menuName = "NSS/GlobalAsset", fileName = "GlobalAsset")]
    public class GlobalAsset : ScriptableObject
    {
        public AudioMixer MainAudioMixer;
        public AudioMixerGroup SEMixerGroup;

        public static GlobalAsset Instance { get; private set; }

        //原始音量大小
        private static float originalMasterVolume;

        private static float originalBGMVolume;

        private static float originalGameSEVolume;

        private static float originalUISEVolume;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Load()
        {
            Instance = (GlobalAsset)Resources.Load("GlobalAsset");

            originalMasterVolume = PlayerPrefs.GetFloat("MasterSoundVolume", -15);
            originalBGMVolume = PlayerPrefs.GetFloat("BGMSoundVolume", 0);
            originalGameSEVolume = PlayerPrefs.GetFloat("EffectSoundVolume", 5);
            originalUISEVolume = PlayerPrefs.GetFloat("EffectSoundVolume", 5);


            GlobalAsset.Instance.MainAudioMixer.SetFloat("Master", originalMasterVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM", originalBGMVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("GameSE", originalGameSEVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("UISE", originalUISEVolume);

        }

        public void SetMasterVolume(float value)
        {
            GlobalAsset.Instance.MainAudioMixer.SetFloat("Master", value);

            PlayerPrefs.SetFloat("MasterSoundVolume", value);
        }

        public void SetBGMVolume(float value)
        {
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM", value);

            PlayerPrefs.SetFloat("BGMSoundVolume", value);
        }

        public void SetEffectVolume(float value)
        {
            GlobalAsset.Instance.MainAudioMixer.SetFloat("GameSE", value);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("UISE", value);

            PlayerPrefs.SetFloat("EffectSoundVolume", value);
        }
    }
}
