using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGridUIObj : MonoBehaviour
{
    [SerializeField] private int SkillID;

    [SerializeField] private RectTransform CurrentUseSkillUIObj;
    [SerializeField] private RectTransform CurrentSelectedSkillUIObj;

    [SerializeField] private Image SkillImg;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.GridChanged += OnGridChanged;
        InitializeUI();
    }

    // Update is called once per frame
    void Update() {

    }

    void InitializeUI() {
        CurrentSelectedSkillUIObj.gameObject.SetActive((SkillID == 1) ? true : false);
        CurrentUseSkillUIObj.gameObject.SetActive((SkillID == PlayerData.CurrentWeaponID) ? true : false);
    }

    void OnGridChanged(int Number) {
        CurrentSelectedSkillUIObj.gameObject.SetActive((SkillID == Number) ? true : false);
    }
}
