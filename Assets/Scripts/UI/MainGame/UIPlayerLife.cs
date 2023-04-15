using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIPlayerLife : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private Image fillImage;

        [SerializeField]
        private UISpriteNumber currentValue;

        [SerializeField]
        private UISpriteNumber maxValue;

        [SerializeField]
        private LifeGaugeColorProfile colorProfile;

        private Player player;

        private void Start()
        {
            player = GameManager.Instance.Player;
            if (player)
            {
                var lifeComp = player.Life;
                if (lifeComp)
                {
                    if (slider)
                    {
                        slider.maxValue = (int)lifeComp.MaxValue;
                        slider.value = (int)lifeComp.Value;
                    }

                    if (currentValue)
                    {
                        currentValue.Value = (int)lifeComp.Value;
                    }

                    if (maxValue)
                    {
                        maxValue.Value = (int)lifeComp.MaxValue;
                    }

                    lifeComp.ValueChanged += OnLifeChanged;
                    lifeComp.MaxValueChanged += OnMaxLifeChanged;
                }
            }
            
        }

        private void OnLifeChanged(uint value)
        {
            if (slider)
            {
                slider.value = value;

                // Update gauge color according to the color profile.
                if (colorProfile && fillImage)
                {
                    fillImage.color = colorProfile.FindColor(slider.normalizedValue);
                }
            }
            if (currentValue)
            {
                currentValue.Value = (int)value;
            }
        }

        private void OnMaxLifeChanged(uint value)
        {
            if (slider)
            {
                slider.maxValue = value;
            }
            if (maxValue)
            {
                maxValue.Value = (int)value;
            }
        }
    }
}
