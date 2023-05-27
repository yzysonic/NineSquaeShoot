using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NSS
{
    public class BGMPlayer : Singleton<BGMPlayer>
    {
        [System.Serializable]
        private class AudioInfo
        {
            public string name;
            public AudioClip clip;

            [Range(0, 1)]
            public float volume = 1;
        }

        private const string StartAudioName = "Default";
        private const string AfterIntroAudioName = "DefaultLoop";

        [SerializeField]
        private AudioMixerGroup audioMixerGroup;

        [SerializeField]
        private List<AudioInfo> audioList;

        [SerializeField]
        private bool hasIntroduction;

        [SerializeField]
        private float startDelay = 0.5f;

        public Dictionary<string, AudioSource> AudioSources { get; private set; }

        public AudioSource CurrentAudioSource { get; private set; }

        private float soundVolume = 1;

        protected override void Awake()
        {
            base.Awake();

            AudioSources = new(audioList.Count);
            soundVolume = PlayerPrefs.GetFloat("BGMSoundVolume", 1);

            foreach (var info in audioList)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.clip = info.clip;
                source.volume = soundVolume;
                source.playOnAwake = false;
                source.outputAudioMixerGroup = audioMixerGroup;
                source.loop = true;
                AudioSources.Add(info.name, source);
            }

            AudioSources[StartAudioName].PlayScheduled(AudioSettings.dspTime + startDelay);

            if (hasIntroduction && AudioSources.Count >= 2)
            {
                AudioSources[StartAudioName].loop = false;
                AudioSources[AfterIntroAudioName].loop = true;
                AudioSources[AfterIntroAudioName].PlayScheduled(AudioSettings.dspTime + AudioSources[StartAudioName].clip.length + startDelay);
            }

            

            CurrentAudioSource = AudioSources[StartAudioName];
        }

        public void FadeOutToPlayNextBGM(string nextAudio, float fadeOutTime)
        {
            var fade = AudioMixerFader.Instance;
            fade.Out(fadeOutTime, () => 
            {
                CurrentAudioSource.volume = soundVolume;
                CurrentAudioSource.Stop();
                fade.Set(1.0f);
                CurrentAudioSource = AudioSources[nextAudio];
                CurrentAudioSource.Play();
            });
        }

        public void SetBGMValue(float value)
        {
            foreach (var source in AudioSources)
            {
                source.Value.volume = value;
            }

            PlayerPrefs.SetFloat("BGMSoundVolume", value);
        }
    }
}
