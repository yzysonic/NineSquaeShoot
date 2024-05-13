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
                StatusTitleText.text = "武器名稱";
                StatusText.text = StatusData.N_Weaponname.ToString();
                break;

            case WeaponUIType.Type:
                StatusTitleText.text = "武器類型";
                StatusText.text = StatusData.N_Weapontype.ToString();
                break;

            case WeaponUIType.Damage:
                StatusTitleText.text = "火力";
                StatusText.text = StatusData.N_Damage.ToString();
                gameObject.SetActive((StatusData.N_Damage == 0) ? false : true);
                break;

            case WeaponUIType.Interval:
                StatusTitleText.text = "射速";
                StatusText.text = StatusData.N_Interval.ToString();
                gameObject.SetActive((StatusData.N_Interval == 0) ? false : true);
                break;

            case WeaponUIType.IntervalLimit:
                StatusTitleText.text = "最大射速";
                StatusText.text = StatusData.N_Intervalmin.ToString();
                gameObject.SetActive((StatusData.N_Intervalmin == 0) ? false : true);
                break;

            case WeaponUIType.Buff:
                StatusTitleText.text = "特殊效果";
                StatusText.text = StatusData.N_Weaponbuff.ToString();
                gameObject.SetActive((StatusData.N_Weaponbuff == 0) ? false : true);
                break;
        }
    }
}
