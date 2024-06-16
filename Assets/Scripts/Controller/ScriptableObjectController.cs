using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectController : MonoBehaviour
{

    public static ScriptableObjectController Instance;

    #region ExcelData
    [SerializeField] private BuffDataInUnity _BuffData;
    public BuffDataInUnity BuffData => _BuffData;

    [SerializeField] private CharacterDataInUnity _characterStatusData;
    public CharacterDataInUnity CharacterStatusData => _characterStatusData;

    [SerializeField] private MesDataInUnity _MesData;
    public MesDataInUnity MesData => _MesData;

    [SerializeField] private ProjecttileDataInUnity _ProjectTileData;
    public ProjecttileDataInUnity ProjectTileData => _ProjectTileData;

    [SerializeField] private RoundGroupInUnity _RoundGroupData;
    public RoundGroupInUnity RoundGroupData => _RoundGroupData;

    [SerializeField] private SkillDataInUnity _SkillData;
    public SkillDataInUnity SkillData => _SkillData;

    [SerializeField] private SoundDataInUnity _VFXData;
    public SoundDataInUnity VFXData => _VFXData;

    [SerializeField] private StageDataInUnity _StageData;
    public StageDataInUnity StageData => _StageData;

    [SerializeField] private WeaponDataInUnity _weaponStatusData;
    public WeaponDataInUnity WeaponStatusData => _weaponStatusData;
    #endregion

    [SerializeField] private CharacterData[] SO_CharacterDataArray;
    [SerializeField] private WeaponData[] SO_WeaponDataArray;

    public Dictionary<int, CharacterData> SO_CharacterDataDic;
    public Dictionary<int, WeaponData> SO_WeaponDataDic;

    void Awake() {
        Instance = this;
        SO_CharacterDataDic = new Dictionary<int, CharacterData>();
        SO_WeaponDataDic = new Dictionary<int, WeaponData>();
    }

    // Start is called before the first frame update
    void Start() {
        SetScriptableObjData();
        SetDictionary();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void SetScriptableObjData() {
        if (SO_CharacterDataArray.Length == _characterStatusData.dataArray.Length) {
            for (int i = 0; i < _characterStatusData.dataArray.Length; i++) {
                SO_CharacterDataArray[i].ID = _characterStatusData.dataArray[i].N_ID;
                SO_CharacterDataArray[i].HP = _characterStatusData.dataArray[i].N_Hp;
                SO_CharacterDataArray[i].InitialWeapon = _characterStatusData.dataArray[i].N_Weapon;
                SO_CharacterDataArray[i].MoveTime = _characterStatusData.dataArray[i].N_Movetime;
                SO_CharacterDataArray[i].MoveTimeCoolDown = _characterStatusData.dataArray[i].N_Colddown;
                SO_CharacterDataArray[i].HPRecoveryValue = _characterStatusData.dataArray[i].N_Hprecovervalue;
                SO_CharacterDataArray[i].HPRecoveryTime = _characterStatusData.dataArray[i].N_Hprecovertime;
                SO_CharacterDataArray[i].Strength = _characterStatusData.dataArray[i].N_Str;
                SO_CharacterDataArray[i].WeaponType2Ratio = _characterStatusData.dataArray[i].N_Weapontype2raito;
                SO_CharacterDataArray[i].WeaponType3Ratio = _characterStatusData.dataArray[i].N_Weapontype3raito;
                SO_CharacterDataArray[i].WeaponType4Ratio = _characterStatusData.dataArray[i].N_Weapontype4raito;
                SO_CharacterDataArray[i].CriticalPercent = _characterStatusData.dataArray[i].N_Critcalpercent;
                SO_CharacterDataArray[i].CriticalRatio = _characterStatusData.dataArray[i].N_Critcalratio;
                SO_CharacterDataArray[i].Block = _characterStatusData.dataArray[i].N_Block;
                SO_CharacterDataArray[i].StealHealRatio = _characterStatusData.dataArray[i].N_Stealhealratio;
                SO_CharacterDataArray[i].StealHeal = _characterStatusData.dataArray[i].N_Stealheal;
                SO_CharacterDataArray[i].DodgeRatio = _characterStatusData.dataArray[i].N_Dodgeratio;
                SO_CharacterDataArray[i].SkillCoolDownRatio = _characterStatusData.dataArray[i].N_Skillcolddownratio;
                SO_CharacterDataArray[i].LuckyValue = _characterStatusData.dataArray[i].N_Luckvalue;
                SO_CharacterDataArray[i].UniqueSkill = _characterStatusData.dataArray[i].N_Uniqueskill;
                SO_CharacterDataArray[i].Buff = _characterStatusData.dataArray[i].N_Buff;
                SO_CharacterDataArray[i].HitSFX = _characterStatusData.dataArray[i].N_Hitsfx;
                SO_CharacterDataArray[i].DeadSFX = _characterStatusData.dataArray[i].N_Deadsfx;
                SO_CharacterDataArray[i].VictorySFX = _characterStatusData.dataArray[i].N_Victorysfx;
                SO_CharacterDataArray[i].UnlockTrigger = _characterStatusData.dataArray[i].N_Unlocktriger;
                SO_CharacterDataArray[i].UnlockParam = _characterStatusData.dataArray[i].N_Unlockparam;
            }
        }
        else {
            Debug.LogError("CharacterDataArray長度與Excel表資料不一樣");
        }

        if (SO_WeaponDataArray.Length == _weaponStatusData.dataArray.Length) {
            for (int i = 0; i < _weaponStatusData.dataArray.Length; i++) {
                SO_WeaponDataArray[i].ID = _weaponStatusData.dataArray[i].N_ID;
                SO_WeaponDataArray[i].WeaponName = _weaponStatusData.dataArray[i].N_Weaponname;
                SO_WeaponDataArray[i].WeaponDescription = _weaponStatusData.dataArray[i].N_Weapondescription;
                SO_WeaponDataArray[i].WeaponType = _weaponStatusData.dataArray[i].N_Weapontype;
                SO_WeaponDataArray[i].Damage = _weaponStatusData.dataArray[i].N_Damage;
                SO_WeaponDataArray[i].Interval = _weaponStatusData.dataArray[i].N_Interval;
                SO_WeaponDataArray[i].IntervalMin = _weaponStatusData.dataArray[i].N_Intervalmin;
                SO_WeaponDataArray[i].WeaponBuff = _weaponStatusData.dataArray[i].N_Weaponbuff;
                SO_WeaponDataArray[i].ProjectileID = _weaponStatusData.dataArray[i].N_Projectileid;
                SO_WeaponDataArray[i].WeaponSFX = _weaponStatusData.dataArray[i].N_Weaponsfx;
            }
        }
        else {
            Debug.LogError("WeaponDataArray長度與Excel表資料不一樣");
        }
    }

    void SetDictionary() {
        for (int i = 0; i < SO_CharacterDataArray.Length; i++) {
            if (SO_CharacterDataArray[i] != null) {
                SO_CharacterDataDic.Add(SO_CharacterDataArray[i].ID, SO_CharacterDataArray[i]);
            }
        }

        for (int i = 0; i < SO_WeaponDataArray.Length; i++) {
            if (SO_WeaponDataArray[i] != null) {
                SO_WeaponDataDic.Add(SO_WeaponDataArray[i].ID, SO_WeaponDataArray[i]);
            }
        }
    }
}
