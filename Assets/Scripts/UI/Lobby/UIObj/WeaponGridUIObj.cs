using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGridUIObj : MonoBehaviour
{
    [SerializeField] private int _GridID;
    public int GridID => _GridID;
    [SerializeField] private int _WeaponID;
    public int WeaponID => _WeaponID;

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

    public void SetGridID(int Value) {
        _GridID = Value;
    }

    public void SetWeaponID(int Value) {
        _WeaponID = Value;
    }

    void InitializeUI() {
        CurrentSelectedWeaponUIObj.gameObject.SetActive((_GridID == 1) ? true : false);
        WeaponImg.sprite = ScriptableObjectController.Instance.WeaponStatusDic[_WeaponID].WeaponSprite;
        CurrentUseWeaponUIObj.gameObject.SetActive((_WeaponID == SaveDataController.Instance.Data.playerData.CurrentWeaponID) ? true : false);
    }

    void OnSelectGridChanged(int Number) {
        CurrentSelectedWeaponUIObj.gameObject.SetActive((_GridID == Number) ? true : false);
    }

    void OnConfirmSelectGridChanged(int Number) {
        CurrentUseWeaponUIObj.gameObject.SetActive((_GridID == Number) ? true : false);
    }
}
