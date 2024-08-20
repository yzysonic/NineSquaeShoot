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
        private Image fillImage;

        [SerializeField]
        private UISpriteNumber lifeNumber;

        [SerializeField]
        private LifeGaugeColorProfile colorProfile;

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
                    }

                    OnLifeChanged(newLifeComp.Value);
                }
            }
        }

        private Character ownerCharacter;

        private void LateUpdate()
        {
            if (OwnerCharacter)
            {
                SpriteRenderer spriteRenderer = ownerCharacter.GetComponent<SpriteRenderer>();
                if (spriteRenderer == null) {
                    spriteRenderer = ownerCharacter.gameObject.transform.GetComponentInChildren<SpriteRenderer>();
                }
                Sprite sprite = spriteRenderer ? spriteRenderer.sprite : null;
                if (sprite)
                {
                    var pos = ownerCharacter.transform.position * 100 + (spriteRenderer.sprite.textureRect.yMax - spriteRenderer.sprite.pivot.y) * Vector3.up;
                    pos.z = 0;
                    transform.localPosition = pos;
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

                // Update gauge color according to the color profile.
                if (colorProfile && fillImage)
                {
                    fillImage.color = colorProfile.FindColor(lifeGauge.normalizedValue);
                }
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
