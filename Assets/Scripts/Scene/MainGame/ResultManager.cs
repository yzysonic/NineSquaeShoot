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

        public void OnPlayerDefeated()
        {
            GameUIManager.Instance.SetResultActive(true);
        }
    }
}
