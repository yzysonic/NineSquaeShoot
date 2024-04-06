using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.UILabelChangeButtonClicked += OnUILabelChangeButtonClicked;
        LobbyUIController.Instance.ConfirmButtonClicked += OnConfirmButtonClicked;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnConfirmButtonClicked() {
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
        gameObject.SetActive(false);
    }

    void OnUILabelChangeButtonClicked(float Number) {

    }
}
