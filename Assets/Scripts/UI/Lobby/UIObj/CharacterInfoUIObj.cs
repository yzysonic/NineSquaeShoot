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
        if (ScriptableObjectController.Instance.CharacterStatusDic.Count != 0) {
            OnCharacterInfoChanged(ScriptableObjectController.Instance.CharacterStatusDic[SaveDataController.Instance.Data.playerData.CurrentCharacterID], ObjValue);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCharacterInfoChanged(CharacterStatus StatusData, int Value) {
        if (ObjValue == Value) {
            switch (StatusType) {
                case CharacterUIType.Name:
                    StatusTitleText.text = "角色名稱";
                    StatusText.text = StatusData.NameID.ToString();
                    break;

                case CharacterUIType.HP:
                    StatusTitleText.text = "血量";
                    StatusText.text = StatusData.HP.ToString();
                    break;

                case CharacterUIType.InitialWeapon:
                    StatusTitleText.text = "預設武器";
                    StatusText.text = StatusData.WeaponID.ToString();
                    gameObject.SetActive((StatusData.WeaponID == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTime:
                    StatusTitleText.text = "移動時間";
                    StatusText.text = StatusData.MoveTime.ToString();
                    gameObject.SetActive((StatusData.MoveTime == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTimeCoolDown:
                    StatusTitleText.text = "移動冷卻時間";
                    StatusText.text = StatusData.MoveCoolDown.ToString();
                    gameObject.SetActive((StatusData.MoveCoolDown == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverValue:
                    StatusTitleText.text = "再生回復量";
                    StatusText.text = StatusData.HPRecovery.ToString();
                    gameObject.SetActive((StatusData.HPRecovery == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverTime:
                    StatusTitleText.text = "再生時間";
                    StatusText.text = StatusData.HPRecoveryTime.ToString();
                    gameObject.SetActive((StatusData.HPRecoveryTime == 0) ? false : true);
                    break;

                case CharacterUIType.Strength:
                    StatusTitleText.text = "力量";
                    StatusText.text = StatusData.Strength.ToString();
                    gameObject.SetActive((StatusData.Strength == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType1Ratio:
                    StatusTitleText.text = "輕型攻擊倍率";
                    StatusText.text = StatusData.LightAttackRatio.ToString();
                    gameObject.SetActive((StatusData.LightAttackRatio == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType2Ratio:
                    StatusTitleText.text = "中型攻擊倍率";
                    StatusText.text = StatusData.MiddleAttackRatio.ToString();
                    gameObject.SetActive((StatusData.MiddleAttackRatio == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType3Ratio:
                    StatusTitleText.text = "重型攻擊倍率";
                    StatusText.text = StatusData.HeavyAttackRatio.ToString();
                    gameObject.SetActive((StatusData.HeavyAttackRatio == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalRatio:
                    StatusTitleText.text = "暴擊率(%)";
                    StatusText.text = StatusData.CriticalRatio.ToString();
                    gameObject.SetActive((StatusData.CriticalRatio == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalPercent:
                    StatusTitleText.text = "暴擊傷害(%)";
                    StatusText.text = StatusData.CriticalPercent.ToString();
                    gameObject.SetActive((StatusData.CriticalPercent == 0) ? false : true);
                    break;

                case CharacterUIType.Block:
                    StatusTitleText.text = "護盾";
                    StatusText.text = StatusData.Block.ToString();
                    gameObject.SetActive((StatusData.Block == 0) ? false : true);
                    break;

                case CharacterUIType.StealHPRatio:
                    StatusTitleText.text = "吸血率(%)";
                    StatusText.text = StatusData.StealHealPercent.ToString();
                    gameObject.SetActive((StatusData.StealHealPercent == 0) ? false : true);
                    break;

                case CharacterUIType.StealHeal:
                    StatusTitleText.text = "吸血量(%)";
                    StatusText.text = StatusData.StealHeal.ToString();
                    gameObject.SetActive((StatusData.StealHeal == 0) ? false : true);
                    break;

                case CharacterUIType.DodgeRatio:
                    StatusTitleText.text = "迴避率(%)";
                    StatusText.text = StatusData.DodgeRatio.ToString();
                    gameObject.SetActive((StatusData.DodgeRatio == 0) ? false : true);
                    break;

                case CharacterUIType.SkillCoolDownRatio:
                    StatusTitleText.text = "技能冷卻(%)";
                    StatusText.text = StatusData.SkillCoolDownRatio.ToString();
                    gameObject.SetActive((StatusData.SkillCoolDownRatio == 0) ? false : true);
                    break;

                case CharacterUIType.LuckValue:
                    StatusTitleText.text = "運氣";
                    StatusText.text = StatusData.LuckValue.ToString();
                    gameObject.SetActive((StatusData.LuckValue == 0) ? false : true);
                    break;

                case CharacterUIType.Buff:
                    StatusTitleText.text = "Buff能力";
                    StatusText.text = StatusData.Buff.ToString();
                    gameObject.SetActive((StatusData.Buff == 0) ? false : true);
                    break;
            }
        }
    }
}
