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
                if (IsScoreReadonly)
                {
                    return;
                }

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

        public bool IsScoreReadonly { get; set; } = false;

        public ScoreRecord ScoreRecord { get; private set; }

        public int LastPlacement { get; private set; } = -1;

        private int currentScore = 0;

        protected override void Awake()
        {
            base.Awake();
            ScoreRecord = SaveManager.SaveData.scoreRecord;
        }

        public void OnNewGameStarted()
        {
            IsScoreReadonly = false;
            CurrentScore = 0;
        }

        public void UpdateRecord()
        {
            ScoreRecord.TryAddScore(CurrentScore, out int placement);
            LastPlacement = placement;
            SaveManager.SaveChanges();
        }
    }
}
