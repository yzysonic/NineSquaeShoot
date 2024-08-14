using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using NSS;

public class StageController : Singleton<StageController>
{

    [SerializeField] private StageData LevelData;

    [SerializeField] private List<RoundGroupData> GroupDataList;

    private Dictionary<int, RoundGroupData> GroupDataDictionary;

    [Header("關卡ID")]
    [PropertyTooltip("輸入試算表StageDataInUnity裡面的第一個ID")]
    [SerializeField] private int StageDataID;

    protected override void Awake() {
        base.Awake();
        GroupDataList = new List<RoundGroupData>();
        GroupDataDictionary = new Dictionary<int, RoundGroupData>();
    }

    // Start is called before the first frame update
    void Start() {
        SetStageData();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void SetStageData() {
        var StageData = ScriptableObjectController.Instance.StageData.dataArray[StageDataID - 1];
        var RoundData = ScriptableObjectController.Instance.RoundGroupData.dataArray;
        LevelData.SetStageData(StageData.N_ID, StageData.N_Round, StageData.An_Roundgroup.ToList());
        foreach(var Round in RoundData) {
            RoundGroupData temp = new RoundGroupData(Round.N_ID, Round.An_Monsterid, Round.An_Monsterplace);
            GroupDataList.Add(temp);
            GroupDataDictionary.Add(Round.N_ID, temp);
        }
    }

    public RoundGroupData GetCurrentGroupData() {
        return GroupDataDictionary[LevelData.RoundGroupList[WaveController.Instance.CurrentWave - 1]];
    }

    public void CheckStageClear() {
        Invoke("CheckStage", 2);
    }

    void CheckStage() {
        if (WaveController.Instance.CurrentWave >= LevelData.MaxWave) {
            MainGameTimer.Instance.enabled = false;
            ScoreManager.Instance.IsScoreReadonly = true;
            EnemyManager.Instance.EnableSpawnEnemy = false;
            BGMPlayer.Instance.Stop();
            if (GameManager.Instance.CanPlayEndAni) {
                GameManager.Instance.Player.gameObject.SetActive(false);
                FieldManager.Instance.DisableAllBlock();
                GameManager.Instance.FakeClearAni.Play();
            }
            else {
                ResultManager.Instance.DisplayResult();
            }
        }
        else {
            WaveController.Instance.ChangeWave(1);
        }
    }
}
