using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public enum ECharacterAudio
    {
        Fire,
        Damage,
        Defeat,
    }

    [RequireComponent(typeof(AudioSource))]
    public class CharacterAudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip fireAudio;

        [SerializeField]
        private AudioClip damageAudio;

        [SerializeField]
        private AudioClip defeatAudio;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(ECharacterAudio audio)
        {
            AudioClip audioClip = null;

            switch (audio)
            {
                case ECharacterAudio.Fire:
                    audioClip = fireAudio;
                    break;

                case ECharacterAudio.Damage:
                    audioClip = damageAudio;
                    break;

                case ECharacterAudio.Defeat:
                    audioClip = defeatAudio;
                    break;
            }

            if (audioSource && audioClip)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}
