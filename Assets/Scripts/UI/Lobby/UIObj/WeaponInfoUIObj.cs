using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUIObj : MonoBehaviour
{
    public WeaponUIType StatusType;

    [SerializeField] private Text StatusTitleText;
    [SerializeField] private Text StatusText;

    [Tooltip("0代表武器選擇頁面中間的武器資訊，1代表武器選擇頁面中下方的目前正在使用的武器資訊，2代表大廳上方的武器資訊")]
    [SerializeField] private int ObjValue;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnWeaponInfoChanged(OnWeaponInfoChanged);
        if (ScriptableObjectController.Instance.WeaponStatusDic.Count != 0) {
            OnWeaponInfoChanged(ScriptableObjectController.Instance.WeaponStatusDic[SaveDataController.Instance.Data.playerData.CurrentWeaponID], ObjValue);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void OnWeaponInfoChanged(WeaponStatus StatusData, int Value) {
        if (Value == ObjValue) {
            switch (StatusType) {
                case WeaponUIType.Name:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "武器名稱";
                    }
                    StatusText.text = StatusData.NameID.ToString();
                    break;

                case WeaponUIType.Description:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "武器描述";
                    }
                    StatusText.text = StatusData.DescriptionID.ToString();
                    break;

                case WeaponUIType.Type:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "武器類型";
                    }
                    StatusText.text = StatusData.WeaponType.ToString();
                    break;

                case WeaponUIType.Damage:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "火力";
                    }
                    StatusText.text = StatusData.Damage.ToString();
                    gameObject.SetActive((StatusData.Damage == 0) ? false : true);
                    break;

                case WeaponUIType.Interval:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "射速";
                    }
                    StatusText.text = StatusData.Interval.ToString();
                    gameObject.SetActive((StatusData.Interval == 0) ? false : true);
                    break;

                case WeaponUIType.IntervalLimit:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "最大射速";
                    }
                    StatusText.text = StatusData.IntervalMin.ToString();
                    gameObject.SetActive((StatusData.IntervalMin == 0) ? false : true);
                    break;

                case WeaponUIType.Buff:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "特殊效果";
                    }
                    StatusText.text = StatusData.WeaponBuff.ToString();
                    gameObject.SetActive((StatusData.WeaponBuff == 0) ? false : true);
                    break;
            }
        }
    }
}
