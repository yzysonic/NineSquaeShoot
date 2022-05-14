using System;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    [Serializable]
    public class ScoreRecord
    {
        public const int MaxCount = 5;

        [SerializeField]
        private List<int> list = new(MaxCount);

        public void TryAddScore(int score, out int placement)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int record = list[i];
                if (score < record)
                {
                    continue;
                }

                if (score > record)
                {
                    list.Insert(i, score);
                    if (list.Count > MaxCount)
                    {
                        list.RemoveAt(MaxCount);
                    }
                }

                placement = i;
                return;
            }

            if (list.Count < MaxCount)
            {
                placement = list.Count;
                list.Add(score);
                return;
            }

            placement = -1;
        }

        public List<int> GetRecordList()
        {
            return new(list);
        }
    }
}
