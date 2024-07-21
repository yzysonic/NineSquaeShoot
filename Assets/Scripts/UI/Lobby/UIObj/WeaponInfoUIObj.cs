using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUIObj : MonoBehaviour
{
    public WeaponUIType StatusType;

    [SerializeField] private Text StatusTitleText;
    [SerializeField] private Text StatusText;

    [Tooltip("0�N��Z����ܭ����������Z����T�A1�N��Z����ܭ������U�誺�ثe���b�ϥΪ��Z����T�A2�N��j�U�W�誺�Z����T")]
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
                        StatusTitleText.text = "�Z���W��";
                    }
                    StatusText.text = StatusData.NameID.ToString();
                    break;

                case WeaponUIType.Description:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "�Z���y�z";
                    }
                    StatusText.text = StatusData.DescriptionID.ToString();
                    break;

                case WeaponUIType.Type:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "�Z������";
                    }
                    StatusText.text = StatusData.WeaponType.ToString();
                    break;

                case WeaponUIType.Damage:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "���O";
                    }
                    StatusText.text = StatusData.Damage.ToString();
                    gameObject.SetActive((StatusData.Damage == 0) ? false : true);
                    break;

                case WeaponUIType.Interval:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "�g�t";
                    }
                    StatusText.text = StatusData.Interval.ToString();
                    gameObject.SetActive((StatusData.Interval == 0) ? false : true);
                    break;

                case WeaponUIType.IntervalLimit:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "�̤j�g�t";
                    }
                    StatusText.text = StatusData.IntervalMin.ToString();
                    gameObject.SetActive((StatusData.IntervalMin == 0) ? false : true);
                    break;

                case WeaponUIType.Buff:
                    if (StatusTitleText != null) {
                        StatusTitleText.text = "�S��ĪG";
                    }
                    StatusText.text = StatusData.WeaponBuff.ToString();
                    gameObject.SetActive((StatusData.WeaponBuff == 0) ? false : true);
                    break;
            }
        }
    }
}
