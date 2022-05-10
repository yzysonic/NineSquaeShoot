using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class UIPlayerScore : MonoBehaviour
    {
        [SerializeField]
        private UISpriteNumber number;

        private Player player;

        private void Start()
        {
            ScoreManager.Instance.CurrentScoreChanged += OnScoreChanged;
            if (number)
            {
                number.Value = ScoreManager.Instance.CurrentScore;
            }
        }

        private void OnDestroy()
        {
            if (ScoreManager.IsCreated)
            {
                ScoreManager.Instance.CurrentScoreChanged -= OnScoreChanged;
            }
        }

        private void OnScoreChanged(int value)
        {
            if (number)
            {
                number.Value = value;
            }
        }
    }
}
