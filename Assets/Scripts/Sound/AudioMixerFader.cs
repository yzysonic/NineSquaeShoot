using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NSS
{
    public class AudioMixerFader : Singleton<AudioMixerFader>
    {
        [SerializeField]
        private AudioMixer audioMixer;

        [SerializeField]
        private List<string> fadeGroups;

        [SerializeField, Range(0, 1)]
        private float maxVolume = 1.0f;

        [SerializeField, Range(0, 1)]
        private float minVolume = 0.0f;

        private AnimationCurve curve;
        private Timer timer;
        private float fadeTime;
        private bool isFadeIn;
        private bool isFadeOut;
        private event Action FadeCompleted;

        private void Reset()
        {
            enabled = false;
        }

        protected override void Awake()
        {
            base.Awake();
            timer = new Timer();
        }

        private void OnEnable()
        {
            if (!isFadeIn && !isFadeOut)
            {
                enabled = false;
                return;
            }
            if (isFadeIn)
            {
                curve = AnimationCurve.Linear(0.0f, minVolume, 1.0f, maxVolume);
            }
            else
            {
                curve = AnimationCurve.Linear(0.0f, maxVolume, 1.0f, minVolume);
            }

            timer.Reset(fadeTime);
        }

        void Update()
        {
            timer++;
            var volume = Mathf.Lerp(-80.0f, 0.0f, curve.Evaluate(Mathf.Min(timer.Progress, 1.0f)));
            Set(volume);

            if (timer)
            {
                enabled = false;
                isFadeIn = false;
                isFadeOut = false;
                FadeCompleted?.Invoke();
                FadeCompleted = null;
            }

        }

        public void In(float fadeTime = 1.0f, Action completed = null)
        {
            this.fadeTime = fadeTime;
            isFadeIn = true;
            enabled = true;
            FadeCompleted = completed;
        }

        public void Out(float fadeTime = 1.0f, Action completed = null)
        {
            this.fadeTime = fadeTime;
            isFadeOut = true;
            enabled = true;
            FadeCompleted = completed;
        }

        public void Set(float volume)
        {
            foreach (var group in fadeGroups)
            {
                audioMixer.SetFloat(group, volume);
            }
        }
    }

}
