using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStatus
{
    [SerializeField] private int _ID;
    public int ID => _ID;

    [SerializeField] private int _NameID;
    public int NameID => _NameID;

    [SerializeField] private int _DescriptionID;
    public int DescriptionID => _DescriptionID;

    [SerializeField] private int _HP;
    public int HP => _HP;

    [SerializeField] private int _WeaponID;
    public int WeaponID => _WeaponID;

    [SerializeField] private int _MoveTime;
    public int MoveTime => _MoveTime;

    [SerializeField] private int _MoveCoolDown;
    public int MoveCoolDown => _MoveCoolDown;

    [SerializeField] private int _HPRecovery;
    public int HPRecovery => _HPRecovery;

    [SerializeField] private int _HPRecoveryTime;
    public int HPRecoveryTime => _HPRecoveryTime;

    [SerializeField] private int _Strength;
    public int Strength => _Strength;

    [SerializeField] private int _LightAttackRatio;
    public int LightAttackRatio => _LightAttackRatio;

    [SerializeField] private int _MiddleAttackRatio;
    public int MiddleAttackRatio => _MiddleAttackRatio;

    [SerializeField] private int _HeavyAttackRatio;
    public int HeavyAttackRatio => _HeavyAttackRatio;

    [SerializeField] private int _CriticalPercent;
    public int CriticalPercent => _CriticalPercent;

    [SerializeField] private int _CriticalRatio;
    public int CriticalRatio => _CriticalRatio;

    [SerializeField] private int _Block;
    public int Block => _Block;

    [SerializeField] private int _StealHeal;
    public int StealHeal => _StealHeal;

    [SerializeField] private int _StealHealPercent;
    public int StealHealPercent => _StealHealPercent;

    [SerializeField] private int _DodgeRatio;
    public int DodgeRatio => _DodgeRatio;

    [SerializeField] private int _SkillCoolDownRatio;
    public int SkillCoolDownRatio => _SkillCoolDownRatio;

    [SerializeField] private int _LuckValue;
    public int LuckValue => _LuckValue;

    [SerializeField] private string _PrefabName;
    public string PrefabName => _PrefabName;

    public void SetStatus(int id, int nameid, int descriptionid, int hp, int weaponid, int movetime, int movecooldown, int hprecovery, int hprecoverytime, int strength, int lightattackratio,
        int middleattackratio, int heavyattackratio, int criticalpercent, int criticalratio, int block, int stealheal, int stealhealpercent, int dodgeratio, int skillcooldownratio,
        int luckvalue, string prefabname) {
        _ID = id;
        _NameID = nameid;
        _DescriptionID = descriptionid;
        _HP = hp;
        _WeaponID = weaponid;
        _MoveTime = movetime;
        _MoveCoolDown = movecooldown;
        _HPRecovery = hprecovery;
        _HPRecoveryTime = hprecoverytime;
        _Strength = strength;
        _LightAttackRatio = lightattackratio;
        _MiddleAttackRatio = middleattackratio;
        _HeavyAttackRatio = heavyattackratio;
        _CriticalPercent = criticalpercent;
        _CriticalRatio = criticalratio;
        _Block = block;
        _StealHeal = stealheal;
        _StealHealPercent = stealhealpercent;
        _DodgeRatio = dodgeratio;
        _SkillCoolDownRatio = skillcooldownratio;
        _LuckValue = luckvalue;
        _PrefabName = prefabname;
    }
}
