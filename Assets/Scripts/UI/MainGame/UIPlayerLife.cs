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
        private UISpriteNumber currentValue;

        [SerializeField]
        private UISpriteNumber maxValue;

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
                }
            }
            
        }

        private void OnLifeChanged(uint value)
        {
            if (slider)
            {
                slider.value = value;
            }
            if (currentValue)
            {
                currentValue.Value = (int)value;
            }
        }
    }
}
