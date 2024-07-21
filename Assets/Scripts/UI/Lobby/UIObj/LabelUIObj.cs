using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelUIObj : MonoBehaviour
{

    public enum LabelType { Character, Weapon, Skill };

    [SerializeField] private LabelType labelType;

    [SerializeField] private int LabelCount;

    [SerializeField] private RectTransform ActiveLabelImgObj;

    [SerializeField] private Text LabelText;
    [SerializeField] private Text ActiveLabelText;

    [SerializeField] private int _CharacterID;
    public int CharacterID => _CharacterID;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnLabelChanged(OnLabelChanged);
        ActiveLabelImgObj.gameObject.SetActive((LabelCount == 0) ? true : false);
        switch (labelType) {
            case LabelType.Character:
                LabelText.text = "角色" + (LabelCount + 1).ToString();
                ActiveLabelText.text = "角色" + (LabelCount + 1).ToString();
                break;

            case LabelType.Weapon:
                LabelText.text = "武器";
                ActiveLabelText.text = "武器";
                break;

            case LabelType.Skill:
                LabelText.text = "技能飾品";
                ActiveLabelText.text = "技能飾品";
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetLabelCount(int Count) {
        LabelCount = Count;
    }

    public void SetCharacterID(int ID) {
        _CharacterID = ID;
    }

    void OnLabelChanged(int Number) {
        ActiveLabelImgObj.gameObject.SetActive((Number == LabelCount) ? true : false);
    } 
}
