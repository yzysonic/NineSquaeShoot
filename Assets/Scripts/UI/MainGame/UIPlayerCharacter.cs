using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIPlayerCharacter : UICharacterLife
    {
        [SerializeField]
        private Slider counterGauge;

        [SerializeField]
        private Image counterGaugeColorImage;

        [SerializeField]
        private Color counterNormalColor;

        [SerializeField]
        private Color counterFullColor;

        [SerializeField]
        private float counterColorFlashSpeed = 1.0f;

        private float counterColorFlashTime = 0.0f;

        public override Character OwnerCharacter
        {
            set
            {
                if (value == OwnerCharacter)
                {
                    return;
                }

                if (OwnerCharacter)
                {
                    var oldCounterAction = OwnerCharacter.GetComponent<PlayerCounterAction>();
                    if (oldCounterAction)
                    {
                        oldCounterAction.CoolTimeProgressChanged -= OnCounterCoolTimeChanged;
                    }
                }

                if (value)
                {
                    var counterAction = value.GetComponent<PlayerCounterAction>();
                    if (counterAction)
                    {
                        counterAction.CoolTimeProgressChanged += OnCounterCoolTimeChanged;

                        if (counterGauge)
                        {
                            counterGauge.value = 0.0f;
                        }
                    }
                }

                base.OwnerCharacter = value;
            }
        }

        private void Update()
        {
            if (counterGauge && counterGauge.value >= 1.0f)
            {
                SetCounterGaugeColor(Color.Lerp(counterFullColor, counterNormalColor, Mathf.Sin(counterColorFlashTime * counterColorFlashSpeed)));
                counterColorFlashTime += Time.deltaTime;
            }
        }

        private void OnCounterCoolTimeChanged(float progress)
        {
            if (counterGauge)
            {
                counterGauge.value = progress;
                if (progress < 1.0f)
                {
                    SetCounterGaugeColor(counterNormalColor);
                }
                else
                {
                    counterColorFlashTime = 0.0f;
                }
            }
        }

        private void SetCounterGaugeColor(Color color)
        {
            if (counterGaugeColorImage)
            {
                counterGaugeColorImage.color = color;
            }
        }
    }
}
