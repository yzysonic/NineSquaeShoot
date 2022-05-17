using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace NSS
{
    [RequireComponent(typeof(RawImage))]
    public class FadeManager : Singleton<FadeManager>
    {

        public const float fadeTime = 0.3f;

        public event System.Action FadeCompleted;

        private RawImage image;
        private bool FI, FO;
        private Timer timer;

        public float Alpha
        {
            get
            {
                return image.color.a;
            }
            set
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, value);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;

            image = GetComponent<RawImage>();
            if (!image)
            {
                image = gameObject.AddComponent<RawImage>();
            }
            image.color = Color.black;

            var rectTransform = image.rectTransform;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;

            FI = false;
            FO = false;

            timer = new Timer(fadeTime);
            timer.IsStepEvenWhenPause = true;

            enabled = false;

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            timer++;

            if (FI == true && FO == false)
            {
                Alpha = Mathf.Max(0.0f, 1.0f - timer.Progress);

                if (Alpha <= 0f)
                {
                    FI = false;
                    enabled = false;
                    image.enabled = false;

                    FadeCompleted?.Invoke();
                }

            }
            else if (FI == false && FO == true)
            {
                Alpha = Mathf.Min(1.0f, timer.Progress);

                if (Alpha >= 1f)
                {
                    FO = false;
                    enabled = false;

                    FadeCompleted?.Invoke();
                }

            }
        }

        public void FadeIn(float fadeTime, System.Action callBack = null)
        {
            FI = true;
            this.FadeCompleted = callBack;
            timer.Reset(fadeTime);
            enabled = true;
            image.enabled = true;
            image.color = Color.black;
        }

        public void FadeIn(System.Action callBack = null)
        {
            FadeIn(fadeTime, callBack);
        }

        public void FadeOut(float fadeTime, System.Action callBack = null)
        {
            FO = true;
            this.FadeCompleted = callBack;
            timer.Reset(fadeTime);
            enabled = true;
            image.color = Color.clear;
            image.enabled = true;
        }

        public void FadeOut(System.Action callBack = null)
        {
            FadeOut(fadeTime, callBack);
        }

        public void LoadSceneWithFade(string sceneName)
        {
            // TODO: Redesign BGMPlayer and AudioMixerFader
            AudioMixerFader.Instance.Out(fadeTime);

            FadeOut(() =>
            {
                AudioMixerFader.Instance.Set(1);
                Destroy(BGMPlayer.Instance.gameObject);
                Destroy(AudioMixerFader.Instance.gameObject);

                SceneManager.LoadSceneAsync(sceneName).completed += _ => FadeIn();
            });
        }

    }
}
