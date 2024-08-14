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

    public Dictionary<int, CharacterStatus> EnemyStatusDic;
    public Dictionary<int, CharacterStatus> CharacterStatusDic;
    public Dictionary<int, WeaponStatus> WeaponStatusDic;

    void Awake() {
        Instance = this;
        EnemyStatusDic = new Dictionary<int, CharacterStatus>();
        CharacterStatusDic = new Dictionary<int, CharacterStatus>();
        WeaponStatusDic = new Dictionary<int, WeaponStatus>();
    }

    // Start is called before the first frame update
    void Start() {
        AddressableLoader.Instance.RegisterAssetLoaded(OnAssetLoadCompleted);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void SetDictionary() {
        foreach (var status in _weaponStatusData.dataArray) {
            WeaponStatus temp = new WeaponStatus();
            temp.SetStatus(status.N_ID, status.N_Weaponname, status.N_Weapondescription, status.N_Weapontype, status.N_Damage, status.N_Interval, status.N_Intervalmin, status.N_Weaponbuff, status.N_Projectileid, status.N_Weaponsfx, AddressableLoader.Instance.GetSprite(AddressableLoader.GetSpriteType.Weapon, status.S_Weapontexturename));
            WeaponStatusDic.Add(temp.ID, temp);
        }

        foreach (var status in _characterStatusData.dataArray) {
            CharacterStatus temp = new CharacterStatus();
            temp.SetStatus(status.N_ID, status.N_Name, status.N_Description, status.N_Hp, status.N_Weapon, status.N_Movetime, status.N_Colddown, status.N_Hprecovervalue, status.N_Hprecovertime
                , status.N_Str, status.N_Weapontype2raito, status.N_Weapontype3raito, status.N_Weapontype4raito, status.N_Critcalpercent, status.N_Critcalratio, status.N_Block
                , status.N_Stealheal, status.N_Stealhealratio, status.N_Dodgeratio, status.N_Skillcolddownratio, status.N_Luckvalue, status.N_Buff, status.N_Uniqueskill, status.N_Unlocktriger
                , status.N_Unlockparam, status.N_Attackskillsfx, status.N_Defendskillsfx, status.N_Hitsfx, status.N_Deadsfx, status.N_Victorysfx, status.S_Prefabname
                , AddressableLoader.Instance.GetSprite(AddressableLoader.GetSpriteType.Character, status.S_Texturename), AddressableLoader.Instance.GetSprite(AddressableLoader.GetSpriteType.CharacterIcon, status.S_Icontexturename));
            if (status.N_ID < 100) {
                CharacterStatusDic.Add(temp.ID, temp);
            }
            else {
                EnemyStatusDic.Add(temp.ID, temp);
            }
        }
    }

    void OnAssetLoadCompleted() {
        SetDictionary();
    }
}
