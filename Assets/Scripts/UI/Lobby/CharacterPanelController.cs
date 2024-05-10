using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterUIType { Name, HP, InitialWeapon, MoveTime, MoveTimeCoolDown, HPRecoverValue, HPRecoverTime, Strength, WeaponType1Ratio, WeaponType2Ratio
        , WeaponType3Ratio, WeaponType4Ratio, CriticalRatio, CriticalPercent, Block, StealHPRatio, StealHeal, DodgeRatio, SkillCoolDownRatio, LuckValue, Buff };
public class CharacterPanelController : MonoBehaviour
{

    [SerializeField] private int LabelMaxCount;
    private int CurrentLabelCouunt;
    private int CurrentShowCharatcerInfoCount;

    [SerializeField] private Image CharacterImg;
    [SerializeField] private Image ChooseCharacterImg;

    [SerializeField] private CharacterInfoUIObj[] CharacterInfoUIObjArray;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.UILabelChangeButtonClicked += OnUILabelChangeButtonClicked;
        LobbyUIController.Instance.ConfirmButtonClicked += OnConfirmButtonClicked;
        LobbyUIController.Instance.CancelButttonClicked += OnCancelButtonClicked;
        CurrentShowCharatcerInfoCount = 1;
        CurrentLabelCouunt = 1;
        LobbyUIController.Instance.CharacterIconImg.sprite = ScriptableObjectController.Instance.SO_CharacterDataDic[CurrentLabelCouunt].CharacterIconSprite;
        for (int i = 0; i < CharacterInfoUIObjArray.Length; i++) {
            CharacterInfoUIObjArray[i].InitializeUI(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCouunt - 1], 1);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnConfirmButtonClicked() {
        if (CurrentShowCharatcerInfoCount != CurrentLabelCouunt) {
            LobbyUIController.Instance.SetLobbyCharacterInfo(CurrentLabelCouunt);
            CurrentShowCharatcerInfoCount = CurrentLabelCouunt;
            PlayerData.CurrentCharacterID = CurrentLabelCouunt;
        }
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
        gameObject.SetActive(false);
    }

    void OnCancelButtonClicked() {
        gameObject.SetActive(false);
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
    }

    void OnUILabelChangeButtonClicked(float Number) {
        CurrentLabelCouunt += (int)Number;
        if (CurrentLabelCouunt < 1 || CurrentLabelCouunt > LabelMaxCount) {
            CurrentLabelCouunt = (CurrentLabelCouunt < 1) ? 1 : LabelMaxCount;
        }
        else {
            SetCharacterInfoUI();
        }
    }

    void SetCharacterInfoUI() {
        if (ScriptableObjectController.Instance.SO_CharacterDataDic.ContainsKey(CurrentLabelCouunt)) {
            CharacterImg.sprite = ScriptableObjectController.Instance.SO_CharacterDataDic[CurrentLabelCouunt].CharacterSprite;
            LobbyUIController.Instance.SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[CurrentLabelCouunt - 1], 1);
        }
    }
}
