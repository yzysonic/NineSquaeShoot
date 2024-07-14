using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGridUIObj : MonoBehaviour
{

    [SerializeField] private int WeaponID;

    [SerializeField] private RectTransform CurrentUseWeaponUIObj;
    [SerializeField] private RectTransform CurrentSelectedWeaponUIObj;

    [SerializeField] private Image WeaponImg;

    void OnEnable() {
        CurrentUseWeaponUIObj.gameObject.SetActive((WeaponID == SaveDataController.Instance.Data.playerData.CurrentWeaponID) ? true : false);
    }

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnSelectGridChanged(OnSelectGridChanged);
        LobbyUIController.Instance.RegisterOnConfirmSelectGridChanged(OnConfirmSelectGridChanged);
        InitializeUI();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void InitializeUI() {
        CurrentSelectedWeaponUIObj.gameObject.SetActive((WeaponID == 1) ? true : false);
        WeaponImg.sprite = ScriptableObjectController.Instance.WeaponStatusDic[WeaponID].WeaponSprite;
        CurrentUseWeaponUIObj.gameObject.SetActive((WeaponID == SaveDataController.Instance.Data.playerData.CurrentWeaponID) ? true : false);
    }

    void OnSelectGridChanged(int Number) {
        CurrentSelectedWeaponUIObj.gameObject.SetActive((WeaponID == Number) ? true : false);
    }

    void OnConfirmSelectGridChanged(int Number) {
        CurrentUseWeaponUIObj.gameObject.SetActive((WeaponID == Number) ? true : false);
    }
}
