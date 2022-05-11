using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public enum ERank
    { 
        None,
        D, 
        C, 
        B, 
        A, 
        S, 
        SS, 
        SSS,
        Count
    }

    [CreateAssetMenu(menuName = "NSS/Score Profile", fileName = "ScoreRankProfile")]
    public class ScoreRankProfile : ScriptableObject
    {
        [System.Serializable]
        public struct Row
        {
            public int scoreMax;
            public ERank rank;
        }

        [SerializeField]
        private List<Row> rankList;

        public ERank FindRank(int score)
        {
            return rankList.Find(row => score <= row.scoreMax).rank;
        }
    }
}
