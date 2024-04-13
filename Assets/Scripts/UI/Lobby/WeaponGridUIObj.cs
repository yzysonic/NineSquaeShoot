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
        CurrentUseWeaponUIObj.gameObject.SetActive((WeaponID == PlayerData.CurrentWeaponID) ? true : false);
    }

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.GridChanged += OnGridChanged; 
        InitializeUI();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void InitializeUI() {
        CurrentSelectedWeaponUIObj.gameObject.SetActive((WeaponID == 1) ? true : false);
        WeaponImg.sprite = ScriptableObjectController.Instance.SO_WeaponDataDic[WeaponID].WeaponSprite;
        CurrentUseWeaponUIObj.gameObject.SetActive((WeaponID == PlayerData.CurrentWeaponID) ? true : false);
    }

    void OnGridChanged(int Number) {
        CurrentSelectedWeaponUIObj.gameObject.SetActive((WeaponID == Number) ? true : false);
    }
}
