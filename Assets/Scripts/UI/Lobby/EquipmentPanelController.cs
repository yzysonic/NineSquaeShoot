using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponUIType { Name, Type, Damage, Interval, IntervalLimit, Buff };
public class EquipmentPanelController : MonoBehaviour
{

    [SerializeField] private int LabelMaxCount;
    [SerializeField] private int CurrentLabelCount;
    private int CurrentGridCount;

    private bool IsInWeaponPanel;

    [SerializeField] private RectTransform[] PanelArray;

    [SerializeField] private Image SelectedWeaponImg;
    [SerializeField] private Image CurrentWeaponImg;

    [SerializeField] private Text SelectedWeaponName;
    [SerializeField] private Text SelectedWeaponDescription;
    [SerializeField] private Text SelectedWeaponType;
    [SerializeField] private Text SelectedWeaponDmage;
    [SerializeField] private Text SelectedWeaponInterval;
    [SerializeField] private Text SelectedWeaponBuff;
    [SerializeField] private Text CurrentWeaponName;
    [SerializeField] private Text CurrentWeaponType;
    [SerializeField] private Text CurrentWeaponDmage;
    [SerializeField] private Text CurrentWeaponInterval;

    void OnEnable() {
        if (ScriptableObjectController.Instance != null) {
            CurrentWeaponImg.sprite = ScriptableObjectController.Instance.SO_WeaponDataDic[PlayerData.CurrentWeaponID].WeaponSprite;
            CurrentWeaponName.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Weaponname.ToString();
            CurrentWeaponType.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Weapontype.ToString();
            CurrentWeaponDmage.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Damage.ToString();
            CurrentWeaponInterval.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Interval.ToString();
        }
    }

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.UILabelChangeButtonClicked += OnUILabelChangeButtonClicked;
        LobbyUIController.Instance.ConfirmButtonClicked += OnConfirmButtonClicked;
        LobbyUIController.Instance.CancelButttonClicked += OnCancelButtonClicked;
        LobbyUIController.Instance.GridMoveButtonClicked += OnGridMoveButtonClicked;
        IsInWeaponPanel = true;
        CurrentLabelCount = 1;
        CurrentGridCount = 1;
        CurrentWeaponImg.sprite = ScriptableObjectController.Instance.SO_WeaponDataDic[PlayerData.CurrentWeaponID].WeaponSprite;
        CurrentWeaponName.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Weaponname.ToString();
        CurrentWeaponType.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Weapontype.ToString();
        CurrentWeaponDmage.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Damage.ToString();
        CurrentWeaponInterval.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1].N_Interval.ToString();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnConfirmButtonClicked() {
        if (IsInWeaponPanel) {
            if (CurrentGridCount != PlayerData.CurrentWeaponID) {
                PlayerData.CurrentWeaponID = CurrentGridCount;
                LobbyUIController.Instance.WeaponIconImg.sprite = ScriptableObjectController.Instance.SO_WeaponDataDic[PlayerData.CurrentWeaponID].WeaponSprite;
                LobbyUIController.Instance.SendWeaponInfoChangedEvent(ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID - 1]);
            }
        }
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
        }
        IsInWeaponPanel = (CurrentLabelCount == 1) ? true : false;
        for (int i = 0; i < PanelArray.Length; i++) {
            PanelArray[i].gameObject.SetActive((CurrentLabelCount - 1 == i) ? true : false);
        }
    }

    void OnGridMoveButtonClicked(Vector2 Vector) {
        if (IsInWeaponPanel) {
            if (Vector.y != 0) {
                CurrentGridCount = (Vector.y < 0) ? CurrentGridCount + 1 : CurrentGridCount - 1;
            }

            if (CurrentGridCount < 1 || CurrentGridCount > ScriptableObjectController.Instance.SO_WeaponDataDic.Count) {
                CurrentGridCount = (CurrentGridCount < 1) ? CurrentGridCount = 1 : ScriptableObjectController.Instance.SO_WeaponDataDic.Count;
            }
            SelectedWeaponImg.sprite = ScriptableObjectController.Instance.SO_WeaponDataDic[CurrentGridCount].WeaponSprite;
            SelectedWeaponName.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[CurrentGridCount - 1].N_Weaponname.ToString();
            SelectedWeaponDescription.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[CurrentGridCount - 1].N_Weapondescription.ToString();
            SelectedWeaponType.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[CurrentGridCount - 1].N_Weapontype.ToString();
            SelectedWeaponDmage.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[CurrentGridCount - 1].N_Damage.ToString();
            SelectedWeaponInterval.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[CurrentGridCount - 1].N_Interval.ToString();
            SelectedWeaponBuff.text = ScriptableObjectController.Instance.WeaponStatusData.dataArray[CurrentGridCount - 1].N_Weaponbuff.ToString();
        }
        else {
            if (Vector.y != 0) {
                CurrentGridCount = (Vector.y < 0) ? CurrentGridCount + 3 : CurrentGridCount - 3;
                if (CurrentGridCount > 6 || CurrentGridCount < 1) {
                    CurrentGridCount = (CurrentGridCount > 6) ? CurrentGridCount - 3 : CurrentGridCount + 3;
                }
            }

            if (Vector.x != 0) {
                CurrentGridCount = (Vector.x < 0) ? CurrentGridCount - 1 : CurrentGridCount + 1;
                if (CurrentGridCount < 1 || CurrentGridCount > 6) {
                    CurrentGridCount = (CurrentGridCount < 1) ? CurrentGridCount = 1 : 6;
                }
            }
        }
        LobbyUIController.Instance.SendGridChangedEvent(CurrentGridCount);
    }
}
