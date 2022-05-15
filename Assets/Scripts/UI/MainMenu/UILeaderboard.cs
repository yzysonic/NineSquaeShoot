using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UILeaderboard : MonoBehaviour
    {
        private class RecordInfo
        {
            public GameObject gameObject;
            public Text placement;
            public Text score;
            public Text rank;

            public RecordInfo(GameObject gameObject, Text[] texts)
            {
                this.gameObject = gameObject;
                placement = texts[0];
                score = texts[1];
                rank = texts[2];
            }
        }

        [SerializeField]
        private ScoreRankProfile scoreRankProfile;

        [SerializeField]
        private GameObject background;

        [SerializeField]
        private GameObject noRecordSignObject;

        [SerializeField]
        private GameObject recordInfoListObject;

        [SerializeField]
        private Button closeButton;

        private readonly List<RecordInfo> recordInfoList = new(ScoreRecord.MaxCount);

        private void Awake()
        {
            if (closeButton)
            {
                closeButton.onClick.AddListener(OnCloseButtonPressed);
            }

            if (recordInfoListObject)
            {
                for (int i = 0; i < ScoreRecord.MaxCount; i++)
                {
                    GameObject gameObject = recordInfoListObject.transform.GetChild(i).gameObject;
                    var infoTexts = gameObject.GetComponentsInChildren<Text>();
                    recordInfoList.Add(new(gameObject, infoTexts));
                }
            }
        }

        private void OnEnable()
        {
            background.SetActive(true);

            List<int> savedRecordList = SaveManager.SaveData.scoreRecord.GetRecordList();
            if (savedRecordList == null || savedRecordList.Count == 0)
            {
                noRecordSignObject.SetActive(true);
                return;
            }

            for (int i = 0; i < savedRecordList.Count; i++)
            {
                var info = recordInfoList[i];
                int score = savedRecordList[i];
                info.placement.text = Utility.GetPlacementName(i);
                info.score.text = score.ToString("N0");
                info.rank.text = scoreRankProfile.FindRank(score).ToString();
                info.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            background.SetActive(false);
            noRecordSignObject.SetActive(false);
            foreach (var info in recordInfoList)
            {
                info.gameObject.SetActive(false);
            }
        }

        private void OnCloseButtonPressed()
        {
            gameObject.SetActive(false);
        }
    }
}
