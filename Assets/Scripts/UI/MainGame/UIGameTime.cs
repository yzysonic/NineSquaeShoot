using System;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIGameTime : MonoBehaviour
    {
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            if (text)
            {
                MainGameTimer.Instance.TimeUpdated += OnTimeUpdated;
            }
        }

        private void OnTimeUpdated(float time)
        {
            if (text)
            {
                text.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
            }
        }
    }
}
