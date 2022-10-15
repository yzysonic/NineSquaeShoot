using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    [ExecuteInEditMode]
    public class PlayerCounterEffect : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer sourceRenderer;

        [SerializeField]
        private Material outlineMaterial;

        [SerializeField]
        private bool enableBlurEffect = false;

        [SerializeField]
        private Material blurMaterial;

        [SerializeField, Range(1, 10)]
        private float offset = 1;

        [SerializeField, Range(1, 1000)]
        private float blurStrength = 100.0f;

        private float[] weights = new float[10];

        private SpriteRenderer effectRenderer;
        private Texture sourceTexture;
        private RenderTexture renderTexture1;
        private RenderTexture renderTexture2;

        private void Awake()
        {
            effectRenderer = GetComponent<SpriteRenderer>();

            if (sourceRenderer)
            {
                sourceTexture = sourceRenderer.sprite.texture;
            }
            
            if (sourceTexture)
            {
                renderTexture1 = new RenderTexture(sourceTexture.width, sourceTexture.height, 0, RenderTextureFormat.ARGB32);
                renderTexture2 = new RenderTexture(sourceTexture.width, sourceTexture.height, 0, RenderTextureFormat.ARGB32);
            }

            UpdateEffect();
        }

        private void Update()
        {
            if (sourceRenderer)
            {
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying || sourceTexture != sourceRenderer.sprite.texture)
#else
                if (sourceTexture != sourceRenderer.sprite.texture)
#endif
                {
                    sourceTexture = sourceRenderer.sprite.texture;
                    UpdateEffect();
                }
            }
        }

        private void UpdateEffect()
        {
            // Apply outline effect
            Graphics.Blit(sourceTexture, renderTexture1, outlineMaterial);

            // Apply gaussian blur.
            if (enableBlurEffect)
            {
                CaculateWeights();
                blurMaterial.SetFloatArray("_Weights", weights);

                float offsetX = offset / renderTexture1.width;
                float offsetY = offset / renderTexture1.height;

                // for horizontal blur.
                blurMaterial.SetVector("_Offsets", new Vector4(offsetX, 0, 0, 0));

                Graphics.Blit(renderTexture1, renderTexture2, blurMaterial);

                // for vertical blur.
                blurMaterial.SetVector("_Offsets", new Vector4(0, offsetY, 0, 0));

                Graphics.Blit(renderTexture2, renderTexture1, blurMaterial);
            }

            // Convert to sprite
            var rect = new Rect(0, 0, sourceTexture.width, sourceTexture.height);
            Texture2D texture = ConvertToTexture2D(renderTexture1, rect);

            effectRenderer.sprite = Sprite.Create(texture, rect, sourceRenderer.sprite.pivot / new Vector2(sourceTexture.width, sourceTexture.height));
        }

        private void CaculateWeights()
        {
            float total = 0;
            float d = blurStrength * blurStrength * 0.001f;

            for (int i = 0; i < weights.Length; i++)
            {
                float x = i * 2f;
                float w = Mathf.Exp(-0.5f * (x * x) / d);
                weights[i] = w;

                if (i > 0)
                {
                    w *= 2.0f;
                }

                total += w;
            }

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] /= total;
            }
        }

        private Texture2D ConvertToTexture2D(RenderTexture renderTexture, Rect rect)
        {
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);

            RenderTexture currentRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();

            RenderTexture.active = currentRenderTexture;

            return texture;
        }

    }
}
