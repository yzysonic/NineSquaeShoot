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

        protected override void Awake()
        {
            base.Awake();

            AudioSources = new(audioList.Count);

            foreach (var info in audioList)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.clip = info.clip;
                source.volume = info.volume;
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
                CurrentAudioSource.Stop();
                fade.Set(1.0f);
                CurrentAudioSource = AudioSources[nextAudio];
                CurrentAudioSource.Play();
            });
        }

        public void Stop()
        {
            if (CurrentAudioSource)
            {
                CurrentAudioSource.Stop();
            }
        }

        void Update()
        {
            float MasterVolume = PlayerPrefs.GetFloat("MasterSoundVolume", -15);
            float BGMVolume = PlayerPrefs.GetFloat("BGMSoundVolume", 0);

            GlobalAsset.Instance.MainAudioMixer.SetFloat("Master", MasterVolume);
            GlobalAsset.Instance.MainAudioMixer.SetFloat("BGM", BGMVolume);  
        }
    }
}
