using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class ResultManager : Singleton<ResultManager>
    {
        [SerializeField]
        private ScoreRankProfile scoreRankProfile;

        public ScoreRankProfile ScoreRankProfile { get => scoreRankProfile; }

        public void DisplayResult()
        {
            ScoreManager.Instance.UpdateRecord();
            GameUIManager.Instance.SetResultActive(true);
            BGMPlayer.Instance.FadeOutToPlayNextBGM("Result", 0.5f);
        }
    }
}
