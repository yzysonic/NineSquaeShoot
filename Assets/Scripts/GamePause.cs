using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    static public class GamePause
    {
        static public bool IsPaused => isPaused;

        static private bool isPaused = false;
        static private float timeScaleBeforePause = 1.0f;

        static public void SetPause(bool flag)
        {
            if (flag)
            {
                timeScaleBeforePause = Time.timeScale;
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = timeScaleBeforePause;
            }

            isPaused = flag;
        }
    }
}
