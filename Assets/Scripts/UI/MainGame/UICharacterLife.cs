using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UICharacterLife : PooledMonoBehavior
    {
        [SerializeField]
        private Slider lifeGauge;

        [SerializeField]
        private UISpriteNumber lifeNumber;

        public Character OwnerCharacter
        {
            get => ownerCharacter;
            set
            {
                if (ownerCharacter == value)
                {
                    return;
                }

                var oldLifeComp = ownerCharacter ? ownerCharacter.GetComponent<LifeComponent>() : null;
                if (oldLifeComp)
                {
                    oldLifeComp.ValueChanged -= OnLifeChanged;
                }

                ownerCharacter = value;

                if (!ownerCharacter)
                {
                    return;
                }

                var newLifeComp = ownerCharacter.GetComponent<LifeComponent>();
                if (newLifeComp)
                {
                    newLifeComp.ValueChanged += OnLifeChanged;

                    if (lifeGauge)
                    {
                        lifeGauge.maxValue = newLifeComp.MaxValue;
                        lifeGauge.value = newLifeComp.Value;
                    }

                    if (lifeNumber)
                    {
                        lifeNumber.Value = (int)newLifeComp.Value;
                    }
                }
            }
        }

        private Character ownerCharacter;

        private void LateUpdate()
        {
            if (OwnerCharacter)
            {
                SpriteRenderer spriteRenderer = ownerCharacter.GetComponent<SpriteRenderer>();
                Sprite sprite = spriteRenderer ? spriteRenderer.sprite : null;
                if (sprite)
                {
                    transform.position = ownerCharacter.transform.position * 100 + (spriteRenderer.sprite.textureRect.yMax - spriteRenderer.sprite.pivot.y) * Vector3.up;
                }
            }
        }

        public override void OnDisabled()
        {
            if (lifeGauge)
            {
                lifeGauge.maxValue = 1;
                lifeGauge.value = 1;
            }

            if (lifeNumber)
            {
                lifeNumber.Value = 0;
            }

            base.OnDisabled();
        }

        private void OnDestroy()
        {
            if (OwnerCharacter)
            {
                var oldLifeComp = OwnerCharacter.GetComponent<LifeComponent>();
                if (oldLifeComp)
                {
                    oldLifeComp.ValueChanged -= OnLifeChanged;
                }
            }
        }

        private void OnLifeChanged(uint value)
        {
            if (lifeGauge)
            {
                lifeGauge.value = value;
            }
            if (lifeNumber)
            {
                lifeNumber.Value = (int)value;
            }
        }
    }
}
