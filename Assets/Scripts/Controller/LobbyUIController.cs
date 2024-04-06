using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Action<float> UILabelChangeButtonClicked;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
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

    public void SendUILabelChangeButtonClickedEvent(float Number) {
        UILabelChangeButtonClicked?.Invoke(Number);
    }
}
