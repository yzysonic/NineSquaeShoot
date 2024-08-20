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

    [SerializeField] private float _HPRecoveryTime;
    public float HPRecoveryTime => _HPRecoveryTime;

    [SerializeField] private int _Strength;
    public int Strength => _Strength;

    [SerializeField] private float _LightAttackRatio;
    public float LightAttackRatio => _LightAttackRatio;

    [SerializeField] private float _MiddleAttackRatio;
    public float MiddleAttackRatio => _MiddleAttackRatio;

    [SerializeField] private float _HeavyAttackRatio;
    public float HeavyAttackRatio => _HeavyAttackRatio;

    [SerializeField] private float _CriticalPercent;
    public float CriticalPercent => _CriticalPercent;

    [SerializeField] private float _CriticalRatio;
    public float CriticalRatio => _CriticalRatio;

    [SerializeField] private int _Block;
    public int Block => _Block;

    [SerializeField] private int _StealHeal;
    public int StealHeal => _StealHeal;

    [SerializeField] private float _StealHealPercent;
    public float StealHealPercent => _StealHealPercent;

    [SerializeField] private float _DodgeRatio;
    public float DodgeRatio => _DodgeRatio;

    [SerializeField] private float _SkillCoolDownRatio;
    public float SkillCoolDownRatio => _SkillCoolDownRatio;

    [SerializeField] private int _LuckValue;
    public int LuckValue => _LuckValue;

    [SerializeField] private int _Buff;
    public int Buff => _Buff;

    [SerializeField] private int _UniqueSkill;
    public int UniqueSkill => _UniqueSkill;

    [SerializeField] private int _UnlockTrigger;
    public int UnlockTrigger => _UnlockTrigger;

    [SerializeField] private int _UnlockParam;
    public int UnlockParam => _UnlockParam;

    [SerializeField] private int _AttackSkillSFX;
    public int AttackSkillSFX => _AttackSkillSFX;

    [SerializeField] private int _DefendSkillSFX;
    public int DefendSkillSFX => _DefendSkillSFX;

    [SerializeField] private int _HitSFX;
    public int HitSFX => _HitSFX;

    [SerializeField] private int _DeadSFX;
    public int DeadSFX => _DeadSFX;

    [SerializeField] private int _VictorySFX;
    public int VictorySFX => _VictorySFX;

    [SerializeField] private string _PrefabName;
    public string PrefabName => _PrefabName;

    [SerializeField] private Sprite _CharacterSprite;
    public Sprite CharacterSprite => _CharacterSprite;

    [SerializeField] private Sprite _CharacterIconSprite;
    public Sprite CharacterIconSprite => _CharacterIconSprite;

    public void SetStatus(int id, int nameid, int descriptionid, int hp, int weaponid, int movetime, int movecooldown, int hprecovery, int hprecoverytime, int strength, int lightattackratio,
        int middleattackratio, int heavyattackratio, int criticalpercent, int criticalratio, int block, int stealheal, int stealhealpercent, int dodgeratio, int skillcooldownratio,
        int luckvalue, int buff, int uniqueskill, int unlocktrigger, int unlockparam, int attackskillsfx, int defendskillsfx, int hitsfx, int deadsfx, int victorysfx
        , string prefabname, Sprite charactersprite, Sprite charactericonsprite) {
        _ID = id;
        _NameID = nameid;
        _DescriptionID = descriptionid;
        _HP = hp;
        _WeaponID = weaponid;
        _MoveTime = movetime / 100;
        _MoveCoolDown = movecooldown / 100;
        _HPRecovery = hprecovery;
        _HPRecoveryTime = hprecoverytime / 100;
        _Strength = strength;
        _LightAttackRatio = lightattackratio / 100;
        _MiddleAttackRatio = middleattackratio / 100;
        _HeavyAttackRatio = heavyattackratio / 100;
        _CriticalPercent = criticalpercent / 100;
        _CriticalRatio = criticalratio / 100;
        _Block = block;
        _StealHeal = stealheal;
        _StealHealPercent = stealhealpercent / 100;
        _DodgeRatio = dodgeratio / 100;
        _SkillCoolDownRatio = skillcooldownratio / 100;
        _LuckValue = luckvalue;
        _Buff = buff;
        _UniqueSkill = uniqueskill;
        _UnlockTrigger = unlocktrigger;
        _UnlockParam = unlockparam;
        _AttackSkillSFX = attackskillsfx;
        _DefendSkillSFX = defendskillsfx;
        _HitSFX = hitsfx;
        _DeadSFX = deadsfx;
        _VictorySFX = victorysfx;
        _PrefabName = prefabname;
        _CharacterSprite = charactersprite;
        _CharacterIconSprite = charactericonsprite;
    }
}
