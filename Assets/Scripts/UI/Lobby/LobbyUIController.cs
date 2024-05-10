using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ColliderType { ReturnMainMenu, StartGame, Equipment, Upgrade, Character };
public enum ControlType { Enable, Disable };
public class LobbyUIController : MonoBehaviour
{
    public static LobbyUIController Instance;

    [SerializeField] private RectTransform PopupUIObjParent;
    [SerializeField] private RectTransform Character;

    public bool IsShowPopupUI;

    public Action<ColliderType, ControlType> UIColliderTriggered;
    public Action PopupUIButtonClicked;
    public Action ConfirmButtonClicked;
    public Action CancelButttonClicked;
    public Action<float> UILabelChangeButtonClicked;
    public Action<int> LabelChanged;
    public Action<int> GridChanged;
    public Action<CharacterDataInUnityData, int> CharacterInfoChanged;
    public Action<WeaponDataInUnityData> WeaponInfoChanged;
    public Action<Vector2> GridMoveButtonClicked;

    public Image CharacterIconImg;
    public Image WeaponIconImg;

    [SerializeField] private CharacterInfoUIObj[] CharacterLobbyInfoUIObjArray;

    [SerializeField] private WeaponInfoUIObj[] WeaponLobbyInfoUIObjArray;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        CharacterIconImg.sprite = ScriptableObjectController.Instance.SO_CharacterDataDic[PlayerData.CurrentCharacterID].CharacterIconSprite;
        WeaponIconImg.sprite = ScriptableObjectController.Instance.SO_WeaponDataDic[PlayerData.CurrentWeaponID].WeaponSprite;
        for (int i = 0; i < CharacterLobbyInfoUIObjArray.Length; i++) {
            CharacterLobbyInfoUIObjArray[i].InitializeUI(ScriptableObjectController.Instance.CharacterStatusData.dataArray[PlayerData.CurrentCharacterID], 0);
        }

        for (int i = 0; i < WeaponLobbyInfoUIObjArray.Length; i++) {
            WeaponLobbyInfoUIObjArray[i].InitializeUI(ScriptableObjectController.Instance.WeaponStatusData.dataArray[PlayerData.CurrentWeaponID]);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetLobbyCharacterInfo(int Number) {
        CharacterIconImg.sprite = ScriptableObjectController.Instance.SO_CharacterDataDic[Number].CharacterIconSprite;
        SendCharacterInfoChangedEvent(ScriptableObjectController.Instance.CharacterStatusData.dataArray[Number - 1], 0);
    }

    public void ResetCharacter() {
        Character.anchoredPosition = new Vector2(-513, -133);
    }

    public void ResetCharacterBool() {
        Character.GetComponent<LobbyCharacter>().SetLockLeftFalse();
    }

    public void ControlPopupUIObj(ControlType Type) {
        PopupUIObjParent.gameObject.SetActive((Type == ControlType.Enable) ? true : false);
    }

    public void SendColliderTriggeredEvent(ColliderType Type, ControlType CType) {
        UIColliderTriggered?.Invoke(Type, CType);
    }

    public void SendPopupUIButtonClickedEvent() {
        PopupUIButtonClicked?.Invoke();
    }

    public void SendConfirmButtonClickedEvent() {
        ConfirmButtonClicked?.Invoke();
    }

    public void SendCanaelButtonClickedEvent() {
        CancelButttonClicked?.Invoke();
    }

    public void SendUILabelChangeButtonClickedEvent(float Number) {
        UILabelChangeButtonClicked?.Invoke(Number);
    }

    public void SendGridMoveButtonClickedEvent(Vector2 Vector) {
        GridMoveButtonClicked?.Invoke(Vector);
    }

    public void SendGridChangedEvent(int Number) {
        GridChanged?.Invoke(Number);
    }

    public void SendCharacterInfoChangedEvent(CharacterDataInUnityData Data, int Value) {
        CharacterInfoChanged?.Invoke(Data, Value);
    }

    public void SendWeaponInfoChangedEvent(WeaponDataInUnityData Data) {
        WeaponInfoChanged?.Invoke(Data);
    }

    public void SendLabelChangedEvent(int Number) {
        LabelChanged?.Invoke(Number);
    }
}
