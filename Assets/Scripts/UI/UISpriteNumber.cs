using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UISpriteNumber : MonoBehaviour
    {
        public enum Alignment
        {
            Left,
            Center,
            Right
        }

        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();

        [SerializeField]
        private GameObject digitPrefab;

        [SerializeField, Range(1, 20)]
        private int maxDigitCount = 3;

        [SerializeField]
        private Alignment alignment = Alignment.Center;

        [SerializeField]
        private float spacing = 0;

        public int Value
        {
            get => value;
            set
            {
                if (this.value == value || value < 0)
                {
                    return;
                }

                int maxValue = (int)Mathf.Pow(10, maxDigitCount) - 1;
                value = Mathf.Min(value, maxValue);
                string strValue = value.ToString();
                SetSprites(strValue);
                this.value = value;
            }
        }

        private int value = -1;

        private int currentDisplayDigitCount = 0;

        private readonly List<Image> digitImages = new();

        private bool isInitialized = false;

        private void Awake()
        {
            if (!isInitialized)
            {
                AddDigitImages(maxDigitCount);
                UpdateDigitAlignment();
                isInitialized = true;
            }
        }

        private void AddDigitImages(int count)
        {
            if (!digitPrefab)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                GameObject digitObj = Instantiate(digitPrefab, transform);
                var image = digitObj.GetComponent<Image>();
                if (image)
                {
                    digitImages.Add(image);
                }
            }
        }

        private void UpdateDigitAlignment()
        {
            float anchorXMin = 0,
                anchorXMax = 0,
                pivotX = 0;

            switch (alignment)
            {
                case Alignment.Left:
                    anchorXMin = 0;
                    anchorXMax = 0;
                    pivotX = 0;
                    break;

                case Alignment.Center:
                    anchorXMin = 0.5f;
                    anchorXMax = 0.5f;
                    pivotX = 0.5f;
                    break;

                case Alignment.Right:
                    anchorXMin = 1;
                    anchorXMax = 1;
                    pivotX = 1;
                    break;
            }

            foreach (var digit in digitImages)
            {
                Vector2 anchorMin = digit.rectTransform.anchorMin;
                anchorMin.x = anchorXMin;
                digit.rectTransform.anchorMin = anchorMin;

                Vector2 anchorMax = digit.rectTransform.anchorMax;
                anchorMax.x = anchorXMax;
                digit.rectTransform.anchorMax = anchorMax;

                Vector2 pivot = digit.rectTransform.pivot;
                pivot.x = pivotX;
                digit.rectTransform.pivot = pivot;
            }
        }

        private void UpdateDigitLocation()
        {
            float width = spacing * (currentDisplayDigitCount - 1);
            float startX = 0;

            switch (alignment)
            {
                case Alignment.Left:
                    startX = 0;
                    break;

                case Alignment.Center:
                    startX = -0.5f * width;
                    break;

                case Alignment.Right:
                    startX = -width;
                    break;
            }

            for (int i = 0; i < currentDisplayDigitCount; i++)
            {
                digitImages[i].rectTransform.anchoredPosition = (startX + i * spacing) * Vector3.right;
            }
        }

        private void SetSprites(string value)
        {
            if (!isInitialized)
            {
                Awake();
            }

            int digitCount = value.Length;
            if (digitCount > maxDigitCount)
            {
                return;
            }

            for (int i = 0; i < maxDigitCount; i++)
            {
                if (i < digitCount)
                {
                    int spriteNo = value[i] - '0';
                    if(spriteNo >= 0 && spriteNo < sprites.Count)
                    {
                        digitImages[i].sprite = sprites[spriteNo];
                        digitImages[i].gameObject.SetActive(true);
                        continue;
                    }
                }
                digitImages[i].gameObject.SetActive(false);
            }

            bool shouldUpdateLocation = currentDisplayDigitCount != digitCount;

            currentDisplayDigitCount = digitCount;

            if (shouldUpdateLocation)
            {
                UpdateDigitLocation();
            }
        }
    }
}
