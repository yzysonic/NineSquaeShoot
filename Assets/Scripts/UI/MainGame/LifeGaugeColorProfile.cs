using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    [CreateAssetMenu(menuName = "NSS/Life Gauge Color Profile", fileName = "LifeGaugeColorProfile")]
    public class LifeGaugeColorProfile : ScriptableObject
    {
        [System.Serializable]
        struct ColorSet
        {
            [Range(0f, 1f)]
            public float thresholdRatio;
            public Color color;
        }

        [SerializeField]
        private List<ColorSet> colorSets;

        public Color FindColor(float lifeRatio)
        {
            Color outColor = Color.white;

            // None Color Sets
            if (colorSets.Count == 0)
            {
                return outColor;
            }

            // Only one Color Sets
            if (colorSets.Count == 1)
            {
                if (lifeRatio <= colorSets[0].thresholdRatio)
                {
                    outColor = colorSets[0].color;
                }
                return outColor;
            }

            // Multiple Color Sets
            for (int i = 0; i < colorSets.Count; i++)
            {
                if (lifeRatio <= colorSets[i].thresholdRatio)
                {
                    // Small than the left-most color
                    if (i == colorSets.Count - 1)
                    {
                        outColor = colorSets[i].color;
                        break;
                    }

                    continue;
                }

                // Greater than the right-most color
                if (i == 0)
                {
                    outColor = colorSets[0].color;
                }

                // Between any two color
                else
                {
                    ColorSet leftColorSet = colorSets[i];
                    ColorSet rightColorSet = colorSets[i-1];
                    float colorRate = (lifeRatio - leftColorSet.thresholdRatio) / (rightColorSet.thresholdRatio - leftColorSet.thresholdRatio);
                    outColor = Color.Lerp(leftColorSet.color, rightColorSet.color, colorRate);
                }

                break;
            }

            return outColor;
        }
    }
}