using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterStatusData")]
public class CharacterData : ScriptableObject
{
    public Sprite CharacterSprite;
    public Sprite CharacterIconSprite;
    public Sprite Skill1IconSprite;
    public Sprite Skill2IconSprtie;
    public int ID;
    public int HP;
    public int InitialWeapon;
    public int MoveTime;
    public int MoveTimeCoolDown;
    public int HPRecoveryValue;
    public int HPRecoveryTime;
    public int Strength;
    public int WeaponType1Ratio;
    public int WeaponType2Ratio;
    public int WeaponType3Ratio;
    public int WeaponType4Ratio;
    public int CriticalPercent;
    public int CriticalRatio;
    public int Block;
    public int StealHealRatio;
    public int StealHeal;
    public int DodgeRatio;
    public int SkillCoolDownRatio;
    public int LuckyValue;
    public int UniqueSkill;
    public int Buff;
    public int HitSFX;
    public int DeadSFX;
    public int VictorySFX;
    public int UnlockTrigger;
    public int UnlockParam;
}
