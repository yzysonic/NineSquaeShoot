using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanelController : MonoBehaviour
{

    [SerializeField] private int CurrentLabelCount;

    [SerializeField] private WeaponPanelController WeaponPanelObj;

    [SerializeField] private SkillPanelController SkillPanelObj;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnUILabelChangeButtonClicked(OnUILabelChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnCancelButtonClicked(OnCancelButtonClicked);
        CurrentLabelCount = 0;
        ShowPanel();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCancelButtonClicked() {
        gameObject.SetActive(false);
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
    }

    void OnUILabelChangeButtonClicked(int Number) {
        CurrentLabelCount += Number;
        if (CurrentLabelCount < 0 || CurrentLabelCount > 1) {
            CurrentLabelCount = (CurrentLabelCount < 0) ? 0 : 1;
        }
        LobbyUIController.Instance.SendLabelChangedEvent(CurrentLabelCount);
        ShowPanel();
    }

    void ShowPanel() {
        WeaponPanelObj.gameObject.SetActive((CurrentLabelCount == 0) ? true : false);
        SkillPanelObj.gameObject.SetActive((CurrentLabelCount == 1) ? true : false);
    }
}
