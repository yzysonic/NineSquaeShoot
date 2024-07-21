using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponUIType { Name, Description, Type, Damage, Interval, IntervalLimit, Buff };
public class WeaponPanelController : MonoBehaviour
{

    [SerializeField] private int CurrentGridID;
    [SerializeField] private int CurrentSelectWeaponID;

    [SerializeField] private Image ChooseWeaponImg;
    [SerializeField] private Image CurrentShowWeaponImg;

    [SerializeField] private Transform WeaponGridParent;

    [SerializeField] private WeaponGridUIObj WeaponGridObj;

    private List<WeaponGridUIObj> WeaponGridObjList;

    void OnEnable() {
        SetChooseWeaponUI(SaveDataController.Instance.Data.playerData.CurrentWeaponID);
    }

    void Awake() {
        WeaponGridObjList = new List<WeaponGridUIObj>();
    }

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnChangeButtonClicked(OnChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnConfirmButtonClicked(OnConfirmButtonClicked);
        LobbyUIController.Instance.RegisterOnGridMoveButtonClicked(OnGridMoveButtonClicked);
        CurrentGridID = 1;
        CurrentSelectWeaponID = SaveDataController.Instance.Data.playerData.CurrentWeaponID;
        CreateGrid();
        SetSelectWeaponUI();
    }

    // Update is called once per frame
    void Update() {

    }

    void SetChooseWeaponUI(int Value) {
        var temp = ScriptableObjectController.Instance.WeaponStatusDic[Value];
        ChooseWeaponImg.sprite = temp.WeaponSprite;
        LobbyUIController.Instance.SendWeaponInfoChangedEvent(temp, 1);
    }

    void SetSelectWeaponUI() {
        var temp = ScriptableObjectController.Instance.WeaponStatusDic[CurrentGridID];
        CurrentShowWeaponImg.sprite = temp.WeaponSprite;
        LobbyUIController.Instance.SendWeaponInfoChangedEvent(temp, 0);
    }

    void CreateGrid() {
        int GridCount = WeaponGridParent.GetComponentsInChildren<WeaponGridUIObj>().Length;
        int WeaponCount = ScriptableObjectController.Instance.WeaponStatusDic.Count;
        if (GridCount == 0) {
            for (int i = 0; i < WeaponCount; i++) {
                var Grid = Instantiate(WeaponGridObj, WeaponGridParent);
                Grid.GetComponent<WeaponGridUIObj>().SetGridID(i + 1);
                Grid.SetWeaponID(ScriptableObjectController.Instance.WeaponStatusData.dataArray[i].N_ID);
                WeaponGridObjList.Add(Grid);
            }
        }
        else {
            if (GridCount < WeaponCount) {
                for (int i = 0; i < WeaponCount - GridCount; i++) {
                    var Grid = Instantiate(WeaponGridObj, WeaponGridParent);
                    Grid.GetComponent<WeaponGridUIObj>().SetGridID(i + 1);
                    Grid.SetWeaponID(ScriptableObjectController.Instance.WeaponStatusData.dataArray[i].N_ID);
                    WeaponGridObjList.Add(Grid);
                }
            }
            else {
                for (int i = 0; i < GridCount - WeaponCount; i++) {
                    var label = WeaponGridParent.GetChild(WeaponGridParent.childCount - 1 - i).gameObject;
                    label.SetActive(false);
                    WeaponGridObjList.Remove(label.GetComponent<WeaponGridUIObj>());
                }
            }
        }
    }

    void OnChangeButtonClicked() {
        LobbyUIController.Instance.SendConfirmSelectGridChangedEvent(CurrentGridID);
        CurrentSelectWeaponID = WeaponGridObjList[CurrentGridID - 1].WeaponID;
        SetChooseWeaponUI(CurrentSelectWeaponID);
    }

    void OnConfirmButtonClicked() {
        SaveDataController.Instance.Data.playerData.CurrentWeaponID = CurrentSelectWeaponID;
        LobbyUIController.Instance.SetLobbyIcon();
        LobbyUIController.Instance.SendWeaponInfoChangedEvent(ScriptableObjectController.Instance.WeaponStatusDic[SaveDataController.Instance.Data.playerData.CurrentWeaponID], 2);
        LobbyUIController.Instance.ControlPopupUIObj(ControlType.Disable);
    }

    void OnGridMoveButtonClicked(Vector2 Vector) {
        if (Vector.y != 0) {
            CurrentGridID = (Vector.y < 0) ? CurrentGridID + 1 : CurrentGridID - 1;
        }

        if (CurrentGridID < 1 || CurrentGridID > ScriptableObjectController.Instance.WeaponStatusDic.Count) {
            CurrentGridID = (CurrentGridID < 1) ? 1 : ScriptableObjectController.Instance.WeaponStatusDic.Count;
        }
        CurrentSelectWeaponID = WeaponGridObjList[CurrentGridID - 1].WeaponID;
        SetSelectWeaponUI();
        LobbyUIController.Instance.SendSelectGridChangedEvent(CurrentGridID);
    }
}
