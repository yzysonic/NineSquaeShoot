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
        SkillFire
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

        [SerializeField]
        private AudioClip skillFireAudio;

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

                case ECharacterAudio.SkillFire:
                    audioClip = skillFireAudio;
                    break;
            }

            if (audioSource && audioClip)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}
