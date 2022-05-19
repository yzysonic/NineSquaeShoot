using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NSS
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField]
        private AudioClip onSelectAudioClip;

        [SerializeField]
        private AudioClip onPressAudioClip;

        public bool IsMute { get; set; } = true;

        protected UIMenuButton DefaultSelectedButton { get; set; }

        private UIMenuButton[] buttons;

        protected virtual void Awake()
        {
            buttons = GetComponentsInChildren<UIMenuButton>();
            foreach (UIMenuButton button in buttons)
            {
                button.OwnerMenu = this;
                if (button && button == DefaultSelectedButton)
                {
                    DefaultSelectedButton.Select();
                }
            }

            IsMute = false;
        }

        public void OnButtonHovered(UIMenuButton button)
        {
            if (button)
            {
                button.Select();
            }
        }

        public void OnButtonSelected(UIMenuButton button)
        {
            if (onSelectAudioClip)
            {
                PlayAudioClip(onSelectAudioClip);
            }
        }

        public void OnButtonPressed(UIMenuButton button)
        {
            if (onPressAudioClip)
            {
                PlayAudioClip(onPressAudioClip);
            }
        }

        private void PlayAudioClip(AudioClip audioClip)
        {
            if (!IsMute)
            {
                UISEPlayer.Instance.PlayOneShot(audioClip);
            }
        }
    }
}
