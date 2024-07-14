using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUIObj : MonoBehaviour
{
    public WeaponUIType StatusType;

    [SerializeField] private Text StatusTitleText;
    [SerializeField] private Text StatusText;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnWeaponInfoChanged(OnWeaponInfoChanged);
    }

    // Update is called once per frame
    void Update() {

    }

    public void InitializeUI(WeaponDataInUnityData Data) {
        OnWeaponInfoChanged(Data);
    }

    void OnWeaponInfoChanged(WeaponDataInUnityData StatusData) {
        switch (StatusType) {
            case WeaponUIType.Name:
                StatusTitleText.text = "�Z���W��";
                StatusText.text = StatusData.N_Weaponname.ToString();
                break;

            case WeaponUIType.Type:
                StatusTitleText.text = "�Z������";
                StatusText.text = StatusData.N_Weapontype.ToString();
                break;

            case WeaponUIType.Damage:
                StatusTitleText.text = "���O";
                StatusText.text = StatusData.N_Damage.ToString();
                gameObject.SetActive((StatusData.N_Damage == 0) ? false : true);
                break;

            case WeaponUIType.Interval:
                StatusTitleText.text = "�g�t";
                StatusText.text = StatusData.N_Interval.ToString();
                gameObject.SetActive((StatusData.N_Interval == 0) ? false : true);
                break;

            case WeaponUIType.IntervalLimit:
                StatusTitleText.text = "�̤j�g�t";
                StatusText.text = StatusData.N_Intervalmin.ToString();
                gameObject.SetActive((StatusData.N_Intervalmin == 0) ? false : true);
                break;

            case WeaponUIType.Buff:
                StatusTitleText.text = "�S��ĪG";
                StatusText.text = StatusData.N_Weaponbuff.ToString();
                gameObject.SetActive((StatusData.N_Weaponbuff == 0) ? false : true);
                break;
        }
    }
}
