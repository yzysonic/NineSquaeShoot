using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUIObj : MonoBehaviour
{

    public CharacterUIType StatusType;

    [SerializeField] private Text StatusTitleText;
    [SerializeField] private Text StatusText;

    [Tooltip("0代表角色選擇頁面中間的角色資訊，1代表角色選擇頁面中下方的目前正在使用的角色資訊，2代表大廳上方的角色資訊")]
    [SerializeField] private int ObjValue;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnCharacterInfoChanged(OnCharacterInfoChanged);
        OnCharacterInfoChanged(ScriptableObjectController.Instance.CharacterStatusData.dataArray[(ObjValue == 0) ? 0 : SaveDataController.Instance.Data.playerData.CurrentCharacterID - 1], ObjValue);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCharacterInfoChanged(CharacterDataInUnityData StatusData, int Value) {
        if (ObjValue == Value) {
            switch (StatusType) {
                case CharacterUIType.Name:
                    StatusTitleText.text = "角色名稱";
                    StatusText.text = StatusData.N_Name.ToString();
                    break;

                case CharacterUIType.HP:
                    StatusTitleText.text = "血量";
                    StatusText.text = StatusData.N_Hp.ToString();
                    break;

                case CharacterUIType.InitialWeapon:
                    StatusTitleText.text = "預設武器";
                    StatusText.text = StatusData.N_Weapon.ToString();
                    gameObject.SetActive((StatusData.N_Weapon == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTime:
                    StatusTitleText.text = "移動時間";
                    StatusText.text = StatusData.N_Movetime.ToString();
                    gameObject.SetActive((StatusData.N_Movetime == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTimeCoolDown:
                    StatusTitleText.text = "移動冷卻時間";
                    StatusText.text = StatusData.N_Colddown.ToString();
                    gameObject.SetActive((StatusData.N_Colddown == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverValue:
                    StatusTitleText.text = "再生回復量";
                    StatusText.text = StatusData.N_Hprecovervalue.ToString();
                    gameObject.SetActive((StatusData.N_Hprecovervalue == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverTime:
                    StatusTitleText.text = "再生時間";
                    StatusText.text = StatusData.N_Hprecovertime.ToString();
                    gameObject.SetActive((StatusData.N_Hprecovertime == 0) ? false : true);
                    break;

                case CharacterUIType.Strength:
                    StatusTitleText.text = "力量";
                    StatusText.text = StatusData.N_Str.ToString();
                    gameObject.SetActive((StatusData.N_Str == 0) ? false : true);
                    break;

                /*case CharacterUIType.WeaponType1Ratio:
                    StatusTitleText.text = "輕型攻擊倍率";
                    StatusText.text = StatusData.N_Weapontype1raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype1raito == 0) ? false : true);
                    break;*/

                case CharacterUIType.WeaponType2Ratio:
                    StatusTitleText.text = "中型攻擊倍率";
                    StatusText.text = StatusData.N_Weapontype2raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype2raito == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType3Ratio:
                    StatusTitleText.text = "重型攻擊倍率";
                    StatusText.text = StatusData.N_Weapontype3raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype3raito == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType4Ratio:
                    StatusTitleText.text = "第四武器攻擊倍率";
                    StatusText.text = StatusData.N_Weapontype4raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype4raito == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalRatio:
                    StatusTitleText.text = "暴擊率(%)";
                    StatusText.text = StatusData.N_Critcalratio.ToString();
                    gameObject.SetActive((StatusData.N_Critcalratio == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalPercent:
                    StatusTitleText.text = "暴擊傷害(%)";
                    StatusText.text = StatusData.N_Critcalpercent.ToString();
                    gameObject.SetActive((StatusData.N_Critcalpercent == 0) ? false : true);
                    break;

                case CharacterUIType.Block:
                    StatusTitleText.text = "護盾";
                    StatusText.text = StatusData.N_Block.ToString();
                    gameObject.SetActive((StatusData.N_Block == 0) ? false : true);
                    break;

                case CharacterUIType.StealHPRatio:
                    StatusTitleText.text = "吸血率(%)";
                    StatusText.text = StatusData.N_Stealhealratio.ToString();
                    gameObject.SetActive((StatusData.N_Stealhealratio == 0) ? false : true);
                    break;

                case CharacterUIType.StealHeal:
                    StatusTitleText.text = "吸血量(%)";
                    StatusText.text = StatusData.N_Stealheal.ToString();
                    gameObject.SetActive((StatusData.N_Stealheal == 0) ? false : true);
                    break;

                case CharacterUIType.DodgeRatio:
                    StatusTitleText.text = "迴避率(%)";
                    StatusText.text = StatusData.N_Dodgeratio.ToString();
                    gameObject.SetActive((StatusData.N_Dodgeratio == 0) ? false : true);
                    break;

                case CharacterUIType.SkillCoolDownRatio:
                    StatusTitleText.text = "技能冷卻(%)";
                    StatusText.text = StatusData.N_Skillcolddownratio.ToString();
                    gameObject.SetActive((StatusData.N_Skillcolddownratio == 0) ? false : true);
                    break;

                case CharacterUIType.LuckValue:
                    StatusTitleText.text = "運氣";
                    StatusText.text = StatusData.N_Luckvalue.ToString();
                    gameObject.SetActive((StatusData.N_Luckvalue == 0) ? false : true);
                    break;

                case CharacterUIType.Buff:
                    StatusTitleText.text = "Buff能力";
                    StatusText.text = StatusData.N_Buff.ToString();
                    gameObject.SetActive((StatusData.N_Buff == 0) ? false : true);
                    break;
            }
        }
    }
}
