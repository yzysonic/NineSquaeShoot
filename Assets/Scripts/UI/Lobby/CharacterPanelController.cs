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

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnUILabelChangeButtonClicked(OnUILabelChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnChangeButtonClicked(OnChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnConfirmButtonClicked(OnConfirmButtonClicked);
        LobbyUIController.Instance.RegisterOnCanaelButtonClicked(OnCancelButtonClicked);
        CurrentShowCharatcerInfoCount = 1;
        CurrentLabelCount = 1;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnChangeButtonClicked() {
        if (CurrentShowCharatcerInfoCount != CurrentLabelCount) {
            CurrentShowCharatcerInfoCount = CurrentLabelCount;
            if (ScriptableObjectController.Instance.SO_CharacterDataDic.ContainsKey(CurrentLabelCount)) {
                ChooseCharacterImg.sprite = ScriptableObjectController.Instance.SO_CharacterDataDic[CurrentLabelCount].CharacterSprite;
                LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCount - 1], 1);
            }
        }
    }

    void OnConfirmButtonClicked() {
        if (ScriptableObjectController.Instance.SO_CharacterDataDic.ContainsKey(CurrentLabelCount)) {
            LobbyUIController.Instance.CharacterIconImg.sprite = ScriptableObjectController.Instance.SO_CharacterDataDic[CurrentLabelCount].CharacterSprite;
            LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCount - 1], 2);
        }
        PlayerData.CurrentCharacterID = CurrentLabelCount;
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
        if (ScriptableObjectController.Instance.SO_CharacterDataDic.ContainsKey(CurrentLabelCount)) {
            CharacterImg.sprite = ScriptableObjectController.Instance.SO_CharacterDataDic[CurrentLabelCount].CharacterSprite;
            LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCount - 1], 0);
        }
    }
}
