using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradPanelController : MonoBehaviour
{

    [SerializeField] private int LabelMaxCount;
    private int CurrentLabelCouunt;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.UILabelChangeButtonClicked += OnUILabelChangeButtonClicked;
        LobbyUIController.Instance.ConfirmButtonClicked += OnConfirmButtonClicked;
        LobbyUIController.Instance.CancelButttonClicked += OnCancelButtonClicked;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnConfirmButtonClicked() {
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
            LobbyUIController.Instance.SendLabelChangedEvent(CurrentLabelCouunt);
        }
    }
}
