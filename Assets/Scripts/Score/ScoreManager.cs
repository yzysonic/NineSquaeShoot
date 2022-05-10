using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NSS
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int CurrentScore
        {
            get => currentScore;
            set
            {
                value = Math.Max(0, value);
                if (currentScore != value)
                {
                    currentScore = value;
                    if (CurrentScoreChanged != null)
                    {
                        CurrentScoreChanged.Invoke(value);
                    }
                }
            }
        }

        public event Action<int> CurrentScoreChanged;

        private int currentScore = 0;
    }
}
