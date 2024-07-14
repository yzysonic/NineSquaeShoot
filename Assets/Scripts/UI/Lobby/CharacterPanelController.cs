using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterUIType { Name, HP, InitialWeapon, MoveTime, MoveTimeCoolDown, HPRecoverValue, HPRecoverTime, Strength, WeaponType1Ratio, WeaponType2Ratio
        , WeaponType3Ratio, WeaponType4Ratio, CriticalRatio, CriticalPercent, Block, StealHPRatio, StealHeal, DodgeRatio, SkillCoolDownRatio, LuckValue, Buff };
public class CharacterPanelController : MonoBehaviour
{

    [SerializeField] private int LabelMaxCount;
    private int CurrentLabelCount;
    private int CurrentShowCharatcerInfoCount;

    [SerializeField] private Image CharacterImg;
    [SerializeField] private Image ChooseCharacterImg;

    [SerializeField] private LabelUIObj CharaterLabel;

    [SerializeField] private Transform LabelParent;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnUILabelChangeButtonClicked(OnUILabelChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnChangeButtonClicked(OnChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnConfirmButtonClicked(OnConfirmButtonClicked);
        LobbyUIController.Instance.RegisterOnCanaelButtonClicked(OnCancelButtonClicked);
        CreateLabel();
        CurrentShowCharatcerInfoCount = 1;
        CurrentLabelCount = 1;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void CreateLabel() {
        float temp = LabelParent.GetComponentsInChildren<LabelUIObj>().Length;
        float CharacterCount = ScriptableObjectController.Instance.CharacterStatusDic.Count;
        if (temp == 0) {
            for (int i = 0; i < CharacterCount; i++) {
                var label = Instantiate(CharaterLabel, LabelParent);
                label.transform.SetSiblingIndex(LabelParent.childCount - 2);
                label.GetComponent<LabelUIObj>().SetLabelCount(i + 1);
            }
        }

        if (temp < CharacterCount) {
            for (int i = 0; i < CharacterCount - temp; i++) {
                var label = Instantiate(CharaterLabel, LabelParent);
                label.transform.SetSiblingIndex(LabelParent.childCount - 2);
                label.GetComponent<LabelUIObj>().SetLabelCount((int)CharacterCount);
            }
        }
        else {
            for (int i = 0; i < temp - CharacterCount; i++) {
                LabelParent.GetChild(LabelParent.childCount - 2 - i).gameObject.SetActive(false);
            }
        }
    }

    void OnChangeButtonClicked() {
        if (CurrentShowCharatcerInfoCount != CurrentLabelCount) {
            CurrentShowCharatcerInfoCount = CurrentLabelCount;
            ChooseCharacterImg.sprite = ScriptableObjectController.Instance.CharacterStatusDic[CurrentLabelCount].CharacterSprite;
            LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCount - 1], 1);
        }
    }

    void OnConfirmButtonClicked() {
        LobbyUIController.Instance.SetLobbyIcon();
        LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCount - 1], 2);
        SaveDataController.Instance.Data.playerData.CurrentCharacterID = CurrentLabelCount;
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
        gameObject.SetActive(false);
    }

    void OnCancelButtonClicked() {
        gameObject.SetActive(false);
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
    }

    void OnUILabelChangeButtonClicked(float Number) {
        CurrentLabelCount += (int)Number;
        if (CurrentLabelCount < 1 || CurrentLabelCount > LabelMaxCount) {
            CurrentLabelCount = (CurrentLabelCount < 1) ? 1 : LabelMaxCount;
        }
        else {
            LobbyUIController.Instance.SendLabelChangedEvent(CurrentLabelCount);
            SetCharacterInfoUI();
        }
    }

    void SetCharacterInfoUI() {
        CharacterImg.sprite = ScriptableObjectController.Instance.CharacterStatusDic[CurrentLabelCount].CharacterSprite;
        LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCount - 1], 0);
    }
}
