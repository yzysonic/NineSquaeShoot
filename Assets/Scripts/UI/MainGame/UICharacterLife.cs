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

        public virtual Character OwnerCharacter
        {
            get => ownerCharacter;
            set
            {
                if (ownerCharacter == value)
                {
                    return;
                }

                var oldLifeComp = ownerCharacter ? ownerCharacter.Life : null;
                if (oldLifeComp)
                {
                    oldLifeComp.ValueChanged -= OnLifeChanged;
                }

                ownerCharacter = value;

                if (!ownerCharacter)
                {
                    return;
                }

                var newLifeComp = ownerCharacter.Life;
                if (newLifeComp)
                {
                    newLifeComp.ValueChanged += OnLifeChanged;
                    newLifeComp.MaxValueChanged += OnMaxLifeChanged;

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
                    transform.position = GameUIManager.Instance.MainCanvas.scaleFactor * (ownerCharacter.transform.position * 100 + (spriteRenderer.sprite.textureRect.yMax - spriteRenderer.sprite.pivot.y) * Vector3.up);
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
                var oldLifeComp = OwnerCharacter.Life;
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

        private void OnMaxLifeChanged(uint value)
        {
            if (lifeGauge)
            {
                lifeGauge.maxValue = value;
            }
        }
    }
}
