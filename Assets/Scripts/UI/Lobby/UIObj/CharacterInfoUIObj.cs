using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUIObj : MonoBehaviour
{

    public CharacterUIType StatusType;

    [SerializeField] private Text StatusTitleText;
    [SerializeField] private Text StatusText;

    [Tooltip("0�N�����ܭ��������������T�A1�N�����ܭ������U�誺�ثe���b�ϥΪ������T�A2�N��j�U�W�誺�����T")]
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
                    StatusTitleText.text = "����W��";
                    StatusText.text = StatusData.NameID.ToString();
                    break;

                case CharacterUIType.HP:
                    StatusTitleText.text = "��q";
                    StatusText.text = StatusData.HP.ToString();
                    break;

                case CharacterUIType.InitialWeapon:
                    StatusTitleText.text = "�w�]�Z��";
                    StatusText.text = StatusData.WeaponID.ToString();
                    gameObject.SetActive((StatusData.WeaponID == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTime:
                    StatusTitleText.text = "���ʮɶ�";
                    StatusText.text = StatusData.MoveTime.ToString();
                    gameObject.SetActive((StatusData.MoveTime == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTimeCoolDown:
                    StatusTitleText.text = "���ʧN�o�ɶ�";
                    StatusText.text = StatusData.MoveCoolDown.ToString();
                    gameObject.SetActive((StatusData.MoveCoolDown == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverValue:
                    StatusTitleText.text = "�A�ͦ^�_�q";
                    StatusText.text = StatusData.HPRecovery.ToString();
                    gameObject.SetActive((StatusData.HPRecovery == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverTime:
                    StatusTitleText.text = "�A�ͮɶ�";
                    StatusText.text = StatusData.HPRecoveryTime.ToString();
                    gameObject.SetActive((StatusData.HPRecoveryTime == 0) ? false : true);
                    break;

                case CharacterUIType.Strength:
                    StatusTitleText.text = "�O�q";
                    StatusText.text = StatusData.Strength.ToString();
                    gameObject.SetActive((StatusData.Strength == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType1Ratio:
                    StatusTitleText.text = "�����������v";
                    StatusText.text = StatusData.LightAttackRatio.ToString();
                    gameObject.SetActive((StatusData.LightAttackRatio == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType2Ratio:
                    StatusTitleText.text = "�����������v";
                    StatusText.text = StatusData.MiddleAttackRatio.ToString();
                    gameObject.SetActive((StatusData.MiddleAttackRatio == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType3Ratio:
                    StatusTitleText.text = "�����������v";
                    StatusText.text = StatusData.HeavyAttackRatio.ToString();
                    gameObject.SetActive((StatusData.HeavyAttackRatio == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalRatio:
                    StatusTitleText.text = "�����v(%)";
                    StatusText.text = StatusData.CriticalRatio.ToString();
                    gameObject.SetActive((StatusData.CriticalRatio == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalPercent:
                    StatusTitleText.text = "�����ˮ`(%)";
                    StatusText.text = StatusData.CriticalPercent.ToString();
                    gameObject.SetActive((StatusData.CriticalPercent == 0) ? false : true);
                    break;

                case CharacterUIType.Block:
                    StatusTitleText.text = "�@��";
                    StatusText.text = StatusData.Block.ToString();
                    gameObject.SetActive((StatusData.Block == 0) ? false : true);
                    break;

                case CharacterUIType.StealHPRatio:
                    StatusTitleText.text = "�l��v(%)";
                    StatusText.text = StatusData.StealHealPercent.ToString();
                    gameObject.SetActive((StatusData.StealHealPercent == 0) ? false : true);
                    break;

                case CharacterUIType.StealHeal:
                    StatusTitleText.text = "�l��q(%)";
                    StatusText.text = StatusData.StealHeal.ToString();
                    gameObject.SetActive((StatusData.StealHeal == 0) ? false : true);
                    break;

                case CharacterUIType.DodgeRatio:
                    StatusTitleText.text = "�j�ײv(%)";
                    StatusText.text = StatusData.DodgeRatio.ToString();
                    gameObject.SetActive((StatusData.DodgeRatio == 0) ? false : true);
                    break;

                case CharacterUIType.SkillCoolDownRatio:
                    StatusTitleText.text = "�ޯ�N�o(%)";
                    StatusText.text = StatusData.SkillCoolDownRatio.ToString();
                    gameObject.SetActive((StatusData.SkillCoolDownRatio == 0) ? false : true);
                    break;

                case CharacterUIType.LuckValue:
                    StatusTitleText.text = "�B��";
                    StatusText.text = StatusData.LuckValue.ToString();
                    gameObject.SetActive((StatusData.LuckValue == 0) ? false : true);
                    break;

                case CharacterUIType.Buff:
                    StatusTitleText.text = "Buff��O";
                    StatusText.text = StatusData.Buff.ToString();
                    gameObject.SetActive((StatusData.Buff == 0) ? false : true);
                    break;
            }
        }
    }
}
