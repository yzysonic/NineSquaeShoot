using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSS;


public class LobbyUICollider : MonoBehaviour
{
    [SerializeField] private ColliderType _colliderType;
    public ColliderType ColliderType => _colliderType;

    [SerializeField] private RectTransform SelectedObj;
    [SerializeField] private RectTransform SelectedPopupUI;

    private bool _IsShowSelectedObj;
    public bool IsShowSelectedObj => _IsShowSelectedObj;

    void OnEnable() {
        
    }

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.UIColliderTriggered += OnUIControled;
        LobbyUIController.Instance.PopupUIButtonClicked += OnPopupUIButtonClicked;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ControlSelectedObj(ControlType Type) {
        switch (Type) {
            case ControlType.Enable:
                SelectedObj.gameObject.SetActive(true);
                break;

            case ControlType.Disable:
                SelectedObj.gameObject.SetActive(false);
                break;
        }
    }

    void OnUIControled(ColliderType Type, ControlType CType) {
        switch (CType) {
            case ControlType.Enable:
                SelectedObj.gameObject.SetActive((Type == _colliderType) ? true : false);
                _IsShowSelectedObj = (Type == _colliderType) ? true : false;
                break;

            case ControlType.Disable:
                if (Type == _colliderType) {
                    SelectedObj.gameObject.SetActive(false);
                    _IsShowSelectedObj = false;
                }
                break;
        }
    }

    void OnPopupUIButtonClicked() {
        if (_IsShowSelectedObj) {
            if (SelectedPopupUI != null) {
                LobbyUIController.Instance.ControlPopupUIObj(ControlType.Enable);
                SelectedPopupUI.gameObject.SetActive(true);
            }
            else {
                switch (_colliderType) {
                    case ColliderType.ReturnMainMenu:
                        LobbyUIController.Instance.ResetCharacter();
                        LobbyUIController.Instance.ResetCharacterBool();
                        _IsShowSelectedObj = false;
                        SelectedObj.gameObject.SetActive(false);
                        transform.parent.parent.transform.parent.gameObject.SetActive(false);
                        break;

                    case ColliderType.StartGame:
                        FadeManager.Instance.LoadSceneWithFade("MainGameScene");
                        break;
                }
            }
        }
    }
}
