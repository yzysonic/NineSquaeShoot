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

        [SerializeField, Range(1, 20)]
        private float counterGaugeEmisionBoost = 8.0f;

        [SerializeField]
        private Color counterNormalColor;

        [SerializeField]
        private Color counterFullColor;

        [SerializeField]
        private Color counterExecutingColor = Color.white;

        [SerializeField]
        private float counterColorFlashSpeed = 1.0f;

        private float counterColorFlashTime = 0.0f;

        private bool isCounterExecuting = false;

        private Material counterMaterial;

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
                        oldCounterAction.PreCounterExecuted -= OnPreCounterExecuted;
                    }
                }

                if (value)
                {
                    var counterAction = value.GetComponent<PlayerCounterAction>();
                    if (counterAction)
                    {
                        counterAction.CoolTimeProgressChanged += OnCounterCoolTimeChanged;
                        counterAction.PreCounterExecuted += OnPreCounterExecuted;

                        if (counterGauge)
                        {
                            counterGauge.value = 0.0f;
                        }
                    }
                }

                base.OwnerCharacter = value;
            }
        }

        private void Awake()
        {
            if (counterGaugeColorImage)
            {
                counterMaterial = Instantiate(counterGaugeColorImage.material);
            }
        }

        private void Update()
        {
            if (!isCounterExecuting && counterGauge && counterGauge.value >= 1.0f)
            {
                float animFactor = Mathf.Sin(counterColorFlashTime * counterColorFlashSpeed);
                UpdateCounterGaugeColor(animFactor);
                counterColorFlashTime += Time.deltaTime;
            }
        }

        private void OnDestroy()
        {
            if (counterMaterial)
            {
                Destroy(counterMaterial);
            }
        }

        private void OnCounterCoolTimeChanged(float progress)
        {
            if (counterGauge)
            {
                counterGauge.value = progress;
                if (progress < 1.0f)
                {
                    UpdateCounterGaugeColor(0.0f);
                }
                else
                {
                    counterColorFlashTime = 0.0f;
                    isCounterExecuting = false;
                }
            }
        }

        private void OnPreCounterExecuted()
        {
            isCounterExecuting = true;

            if (counterGaugeColorImage)
            {
                counterGaugeColorImage.color = counterExecutingColor;
                counterMaterial.SetColor("_EmisionColor", Color.white);
                counterMaterial.SetFloat("_Boost", counterGaugeEmisionBoost);
            }
        }

        private void UpdateCounterGaugeColor(float animFactor)
        {
            if (counterGaugeColorImage)
            {
                counterGaugeColorImage.color = Color.Lerp(counterNormalColor, counterFullColor, animFactor);
                counterMaterial.SetColor("_EmisionColor", Color.Lerp(Color.white, counterFullColor, animFactor));
                counterMaterial.SetFloat("_Boost", Mathf.Lerp(1.0f, counterGaugeEmisionBoost, animFactor));
            }
        }
    }
}
