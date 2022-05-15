using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class MainGameTimer : Singleton<MainGameTimer>
    {
        public event Action<float> TimeUpdated;

        public float Elapsed
        {
            get => elapsed;
            private set
            {
                if (elapsed != value)
                {
                    elapsed = value;
                    TimeUpdated?.Invoke(elapsed);
                }
            }
        }

        private float elapsed = 0.0f;

        private void Update()
        {
            if (Time.timeScale > 0.0f)
            {
                Elapsed += Time.deltaTime;
            }
        }

        public void ResetTime()
        {
            Elapsed = 0.0f;
        }

        public void OnNewGameStarted()
        {
            ResetTime();
            enabled = true;
        }
    }
}
