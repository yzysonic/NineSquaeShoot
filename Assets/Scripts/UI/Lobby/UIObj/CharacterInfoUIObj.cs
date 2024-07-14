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
        OnCharacterInfoChanged(ScriptableObjectController.Instance.CharacterStatusData.dataArray[(ObjValue == 0) ? 0 : SaveDataController.Instance.Data.playerData.CurrentCharacterID - 1], ObjValue);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCharacterInfoChanged(CharacterDataInUnityData StatusData, int Value) {
        if (ObjValue == Value) {
            switch (StatusType) {
                case CharacterUIType.Name:
                    StatusTitleText.text = "����W��";
                    StatusText.text = StatusData.N_Name.ToString();
                    break;

                case CharacterUIType.HP:
                    StatusTitleText.text = "��q";
                    StatusText.text = StatusData.N_Hp.ToString();
                    break;

                case CharacterUIType.InitialWeapon:
                    StatusTitleText.text = "�w�]�Z��";
                    StatusText.text = StatusData.N_Weapon.ToString();
                    gameObject.SetActive((StatusData.N_Weapon == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTime:
                    StatusTitleText.text = "���ʮɶ�";
                    StatusText.text = StatusData.N_Movetime.ToString();
                    gameObject.SetActive((StatusData.N_Movetime == 0) ? false : true);
                    break;

                case CharacterUIType.MoveTimeCoolDown:
                    StatusTitleText.text = "���ʧN�o�ɶ�";
                    StatusText.text = StatusData.N_Colddown.ToString();
                    gameObject.SetActive((StatusData.N_Colddown == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverValue:
                    StatusTitleText.text = "�A�ͦ^�_�q";
                    StatusText.text = StatusData.N_Hprecovervalue.ToString();
                    gameObject.SetActive((StatusData.N_Hprecovervalue == 0) ? false : true);
                    break;

                case CharacterUIType.HPRecoverTime:
                    StatusTitleText.text = "�A�ͮɶ�";
                    StatusText.text = StatusData.N_Hprecovertime.ToString();
                    gameObject.SetActive((StatusData.N_Hprecovertime == 0) ? false : true);
                    break;

                case CharacterUIType.Strength:
                    StatusTitleText.text = "�O�q";
                    StatusText.text = StatusData.N_Str.ToString();
                    gameObject.SetActive((StatusData.N_Str == 0) ? false : true);
                    break;

                /*case CharacterUIType.WeaponType1Ratio:
                    StatusTitleText.text = "�����������v";
                    StatusText.text = StatusData.N_Weapontype1raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype1raito == 0) ? false : true);
                    break;*/

                case CharacterUIType.WeaponType2Ratio:
                    StatusTitleText.text = "�����������v";
                    StatusText.text = StatusData.N_Weapontype2raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype2raito == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType3Ratio:
                    StatusTitleText.text = "�����������v";
                    StatusText.text = StatusData.N_Weapontype3raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype3raito == 0) ? false : true);
                    break;

                case CharacterUIType.WeaponType4Ratio:
                    StatusTitleText.text = "�ĥ|�Z���������v";
                    StatusText.text = StatusData.N_Weapontype4raito.ToString();
                    gameObject.SetActive((StatusData.N_Weapontype4raito == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalRatio:
                    StatusTitleText.text = "�����v(%)";
                    StatusText.text = StatusData.N_Critcalratio.ToString();
                    gameObject.SetActive((StatusData.N_Critcalratio == 0) ? false : true);
                    break;

                case CharacterUIType.CriticalPercent:
                    StatusTitleText.text = "�����ˮ`(%)";
                    StatusText.text = StatusData.N_Critcalpercent.ToString();
                    gameObject.SetActive((StatusData.N_Critcalpercent == 0) ? false : true);
                    break;

                case CharacterUIType.Block:
                    StatusTitleText.text = "�@��";
                    StatusText.text = StatusData.N_Block.ToString();
                    gameObject.SetActive((StatusData.N_Block == 0) ? false : true);
                    break;

                case CharacterUIType.StealHPRatio:
                    StatusTitleText.text = "�l��v(%)";
                    StatusText.text = StatusData.N_Stealhealratio.ToString();
                    gameObject.SetActive((StatusData.N_Stealhealratio == 0) ? false : true);
                    break;

                case CharacterUIType.StealHeal:
                    StatusTitleText.text = "�l��q(%)";
                    StatusText.text = StatusData.N_Stealheal.ToString();
                    gameObject.SetActive((StatusData.N_Stealheal == 0) ? false : true);
                    break;

                case CharacterUIType.DodgeRatio:
                    StatusTitleText.text = "�j�ײv(%)";
                    StatusText.text = StatusData.N_Dodgeratio.ToString();
                    gameObject.SetActive((StatusData.N_Dodgeratio == 0) ? false : true);
                    break;

                case CharacterUIType.SkillCoolDownRatio:
                    StatusTitleText.text = "�ޯ�N�o(%)";
                    StatusText.text = StatusData.N_Skillcolddownratio.ToString();
                    gameObject.SetActive((StatusData.N_Skillcolddownratio == 0) ? false : true);
                    break;

                case CharacterUIType.LuckValue:
                    StatusTitleText.text = "�B��";
                    StatusText.text = StatusData.N_Luckvalue.ToString();
                    gameObject.SetActive((StatusData.N_Luckvalue == 0) ? false : true);
                    break;

                case CharacterUIType.Buff:
                    StatusTitleText.text = "Buff��O";
                    StatusText.text = StatusData.N_Buff.ToString();
                    gameObject.SetActive((StatusData.N_Buff == 0) ? false : true);
                    break;
            }
        }
    }
}
