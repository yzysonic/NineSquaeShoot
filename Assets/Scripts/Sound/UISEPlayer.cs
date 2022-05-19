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
            DontDestroyOnLoad(gameObject);
        }

        public void PlayOneShot(AudioClip clip)
        {
            if (audioSource)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
