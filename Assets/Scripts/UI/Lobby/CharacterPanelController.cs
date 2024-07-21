using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterUIType { Name, HP, InitialWeapon, MoveTime, MoveTimeCoolDown, HPRecoverValue, HPRecoverTime, Strength, WeaponType1Ratio, WeaponType2Ratio
        , WeaponType3Ratio, CriticalRatio, CriticalPercent, Block, StealHPRatio, StealHeal, DodgeRatio, SkillCoolDownRatio, LuckValue, Buff };
public class CharacterPanelController : MonoBehaviour
{
    [SerializeField] private int CurrentLabelCount;
    [SerializeField] private int CurrentShowCharatcerInfoID;

    [SerializeField] private Image CurrentShowCharacterImg;
    [SerializeField] private Image ChooseCharacterImg;

    [SerializeField] private LabelUIObj CharaterLabel;

    [SerializeField] private Transform LabelParent;

    private List<LabelUIObj> LabelObjList;

    void OnEnable() {
        SetChooseCharacterInfo(SaveDataController.Instance.Data.playerData.CurrentCharacterID);
    }

    void Awake() {
        LabelObjList = new List<LabelUIObj>();
    }

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnUILabelChangeButtonClicked(OnUILabelChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnChangeButtonClicked(OnChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnConfirmButtonClicked(OnConfirmButtonClicked);
        LobbyUIController.Instance.RegisterOnCancelButtonClicked(OnCancelButtonClicked);
        CreateLabel();
        CurrentShowCharatcerInfoID = LabelObjList[0].CharacterID;
        CurrentLabelCount = 0;
        SetChooseCharacterInfo(SaveDataController.Instance.Data.playerData.CurrentCharacterID);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void SetChooseCharacterInfo(int Value) {
        var Status = ScriptableObjectController.Instance.CharacterStatusDic[Value];
        ChooseCharacterImg.sprite = Status.CharacterIconSprite;
        LobbyUIController.Instance.SendCharacterInfoChangedEvent(Status, 1);
    }

    void SetCharacterInfoUI() {
        var Status = ScriptableObjectController.Instance.CharacterStatusDic[CurrentShowCharatcerInfoID];
        CurrentShowCharacterImg.sprite = Status.CharacterSprite;
        LobbyUIController.Instance.SendCharacterInfoChangedEvent(Status, 0);
    }

    void CreateLabel() {
        int temp = LabelParent.GetComponentsInChildren<LabelUIObj>().Length;
        int CharacterCount = ScriptableObjectController.Instance.CharacterStatusDic.Count;
        if (temp == 0) {
            for (int i = 0; i < CharacterCount; i++) {
                var label = Instantiate(CharaterLabel, LabelParent);
                label.transform.SetSiblingIndex(LabelParent.childCount - 2);
                label.GetComponent<LabelUIObj>().SetLabelCount(i);
                label.SetCharacterID(ScriptableObjectController.Instance.CharacterStatusData.dataArray[i].N_ID);
                LabelObjList.Add(label);
            }
        }
        else {
            if (temp < CharacterCount) {
                for (int i = 0; i < CharacterCount - temp; i++) {
                    var label = Instantiate(CharaterLabel, LabelParent);
                    label.transform.SetSiblingIndex(LabelParent.childCount - 2);
                    label.GetComponent<LabelUIObj>().SetLabelCount(CharacterCount - i - 1);
                    label.SetCharacterID(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CharacterCount - i - 2].N_ID);
                    LabelObjList.Add(label);
                }
            }
            else {
                for (int i = 0; i < temp - CharacterCount; i++) {
                    var label = LabelParent.GetChild(LabelParent.childCount - 2 - i).gameObject;
                    label.SetActive(false);
                    LabelObjList.Remove(label.GetComponent<LabelUIObj>());
                }
            }
        }
        LabelObjList.OrderBy(x => x.CharacterID).ToList();
    }

    void OnChangeButtonClicked() {
        if (CurrentShowCharatcerInfoID != SaveDataController.Instance.Data.playerData.CurrentCharacterID) {
            CurrentShowCharatcerInfoID = LabelObjList[CurrentLabelCount].CharacterID;
            SetChooseCharacterInfo(CurrentShowCharatcerInfoID);
        }
    }

    void OnConfirmButtonClicked() {
        LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusDic[CurrentShowCharatcerInfoID], 2);
        SaveDataController.Instance.Data.playerData.CurrentCharacterID = CurrentShowCharatcerInfoID;
        LobbyUIController.Instance.SetLobbyIcon();
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
        gameObject.SetActive(false);
    }

    void OnCancelButtonClicked() {
        gameObject.SetActive(false);
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
    }

    void OnUILabelChangeButtonClicked(int Number) {
        CurrentLabelCount += Number;
        if (CurrentLabelCount < 0 || CurrentLabelCount > LabelObjList.Count - 1) {
            CurrentLabelCount = (CurrentLabelCount < 0) ? 0 : LabelObjList.Count - 1;
        }
        else {
            LobbyUIController.Instance.SendLabelChangedEvent(CurrentLabelCount);
            CurrentShowCharatcerInfoID = LabelObjList[CurrentLabelCount].CharacterID;
            SetCharacterInfoUI();
        }
    }
}
