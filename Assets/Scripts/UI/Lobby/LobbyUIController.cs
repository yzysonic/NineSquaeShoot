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
    public bool IsReturnMainMenu;

    Action<ColliderType, ControlType> UIColliderTriggered;                                          //UICollider碰撞時觸發
    Action PopupUIButtonClicked;                                                                    //PopUpUI顯示按鈕按下
    Action ChangeButtonClicked;                                                                     //切換選擇按鈕按下
    Action ConfirmButtonClicked;                                                                    //確認選擇按鈕按下
    Action CancelButttonClicked;                                                                    //取消選擇按鈕按下            
    Action<int> UILabelChangeButtonClicked;                                                         //PopUp標籤切換按鈕按下
    Action<int> LabelChanged;                                                                       //PopUp頁面上方標籤切換時觸發
    Action<int> SelectGridChanged;                                                                  //目前選擇格子切換時觸發
    Action<int> ConfirmSelectGridChanged;                                                           //目前選定格子切換時觸發
    Action<CharacterStatus, int> CharacterInfoChanged;                                              //目標角色資訊更動時觸發
    Action<WeaponStatus, int> WeaponInfoChanged;                                                    //目標武器資訊更動時觸發
    Action<Vector2> GridMoveButtonClicked;                                                          //移動選擇格子按鈕按下

    [SerializeField] private Image CharacterIconImg;
    [SerializeField] private Image WeaponIconImg;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    public void CloseButtonClicked() {
        Character.GetComponent<LobbyCharacter>().SetLockMove(false);
        Instance.IsShowPopupUI = false;
        Instance.SendCanaelButtonClickedEvent();
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

    public void SetLobbyIcon() {
        CharacterIconImg.sprite = ScriptableObjectController.Instance.CharacterStatusDic[SaveDataController.Instance.Data.playerData.CurrentCharacterID].CharacterIconSprite;
        WeaponIconImg.sprite = ScriptableObjectController.Instance.WeaponStatusDic[SaveDataController.Instance.Data.playerData.CurrentWeaponID].WeaponSprite;
    }

    #region UIColliderTriggered
    public void SendColliderTriggeredEvent(ColliderType Type, ControlType CType) {
        UIColliderTriggered?.Invoke(Type, CType);
    }

    public void RegisterOnUIColliderTriggered(Action<ColliderType, ControlType> callback) {
        UIColliderTriggered += callback;
    }
    public void UnregisterOnUIColliderTriggered(Action<ColliderType, ControlType> callback) {
        UIColliderTriggered -= callback;
    }
    #endregion

    #region PopUIButtonClicked
    public void SendPopupUIButtonClickedEvent() {
        PopupUIButtonClicked?.Invoke();
    }

    public void RegisterOnPopupUIButtonClicked(Action callback) {
        PopupUIButtonClicked += callback;
    }

    public void UnregisterOnPopupUIButtonClicked(Action callback) {
        PopupUIButtonClicked -= callback;
    }
    #endregion

    #region ChangeButtonClicked
    public void SendChangeButtonClickedEvent() {
        ChangeButtonClicked?.Invoke();
    }

    public void RegisterOnChangeButtonClicked(Action callback) {
        ChangeButtonClicked += callback;
    }
    public void UnregisterOnChangeButtonClicked(Action callback) {
        ChangeButtonClicked -= callback;
    }
    #endregion

    #region ConfirmButtonClicked
    public void SendConfirmButtonClickedEvent() {
        ConfirmButtonClicked?.Invoke();
    }

    public void RegisterOnConfirmButtonClicked(Action callback) {
        ConfirmButtonClicked += callback;
    }
    public void UnregisterOnConfirmButtonClicked(Action callback) {
        ConfirmButtonClicked -= callback;
    }
    #endregion

    #region CanaelButtonClicked
    public void SendCanaelButtonClickedEvent() {
        CancelButttonClicked?.Invoke();
    }

    public void RegisterOnCancelButtonClicked(Action callback) {
        CancelButttonClicked += callback;
    }
    public void UnRegisterOnCancelButtonClicked(Action callback) {
        CancelButttonClicked -= callback;
    }
    #endregion

    #region UILabelChangeButtonClicked
    public void SendUILabelChangeButtonClickedEvent(int Number) {
        UILabelChangeButtonClicked?.Invoke(Number);
    }

    public void RegisterOnUILabelChangeButtonClicked(Action<int> callback) {
        UILabelChangeButtonClicked += callback;
    }
    public void UnregisterOnUILabelChangeButtonClicked(Action<int> callback) {
        UILabelChangeButtonClicked -= callback;
    }
    #endregion

    #region GridMoveButtonClicked
    public void SendGridMoveButtonClickedEvent(Vector2 Vector) {
        GridMoveButtonClicked?.Invoke(Vector);
    }

    public void RegisterOnGridMoveButtonClicked(Action<Vector2> callback) {
        GridMoveButtonClicked += callback;
    }
    public void UnregisterOnGridMoveButtonClicked(Action<Vector2> callback) {
        GridMoveButtonClicked -= callback;
    }
    #endregion

    #region SelectGridChanged
    public void SendSelectGridChangedEvent(int Number) {
        SelectGridChanged?.Invoke(Number);
    }

    public void RegisterOnSelectGridChanged(Action<int> callback) {
        SelectGridChanged += callback;
    }
    public void UnregisterOnSelectGridChanged(Action<int> callback) {
        SelectGridChanged -= callback;
    }
    #endregion

    #region ConfirmSelectGridChanged
    public void SendConfirmSelectGridChangedEvent(int Number) {
        ConfirmSelectGridChanged?.Invoke(Number);
    }

    public void RegisterOnConfirmSelectGridChanged(Action<int> callback) {
        ConfirmSelectGridChanged += callback;
    }
    public void UnregisterOnConfirmSelectGridChanged(Action<int> callback) {
        ConfirmSelectGridChanged -= callback;
    }
    #endregion

    #region CharacterInfoChanged
    public void SendCharacterInfoChangedEvent(CharacterStatus Data, int Value) {
        CharacterInfoChanged?.Invoke(Data, Value);
    }

    public void RegisterOnCharacterInfoChanged(Action<CharacterStatus, int> callback) {
        CharacterInfoChanged += callback;
    }
    public void UnregisterOnCharacterInfoChanged(Action<CharacterStatus, int> callback) {
        CharacterInfoChanged -= callback;
    }
    #endregion

    #region WeaponInfoChanged
    public void SendWeaponInfoChangedEvent(WeaponStatus Data, int Value) {
        WeaponInfoChanged?.Invoke(Data, Value);
    }

    public void RegisterOnWeaponInfoChanged(Action<WeaponStatus, int> callback) {
        WeaponInfoChanged += callback;
    }
    public void UnregisterOnWeaponInfoChanged(Action<WeaponStatus, int> callback) {
        WeaponInfoChanged -= callback;
    }
    #endregion

    #region LabelChanged
    public void SendLabelChangedEvent(int Number) {
        LabelChanged?.Invoke(Number);
    }

    public void RegisterOnLabelChanged(Action<int> callback) {
        LabelChanged += callback;
    }
    public void UnregisterOnLabelChanged(Action<int> callback) {
        LabelChanged -= callback;
    }
    #endregion
}
