using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Sprite WeaponSprite;
    public int ID;
    public int WeaponName;
    public int WeaponDescription;
    public int WeaponType;
    public int Damage;
    public int Interval;
    public int IntervalMin;
    public int WeaponBuff;
    public int ProjectileID;
    public int WeaponSFX;
}
