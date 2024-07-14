using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStatus
{
    [SerializeField] private int _ID;
    public int ID => _ID;

    [SerializeField] private int _NameID;
    public int NameID => _NameID;

    [SerializeField] private int _DescriptionID;
    public int DescriptionID => _DescriptionID;

    [SerializeField] private int _WeaponType;
    public int WeaponType => _WeaponType;

    [SerializeField] private int _Damage;
    public int Damage => _Damage;

    [SerializeField] private int _Interval;
    public int Interval => _Interval;

    [SerializeField] private int _IntervalMin;
    public int IntervalMin => _IntervalMin;

    [SerializeField] private int _WeaponBuff;
    public int WeaponBuff => _WeaponBuff;

    [SerializeField] private int _ProjectTileID;
    public int ProjectTileID => _ProjectTileID;

    [SerializeField] private int _WeaponSFX;
    public int WeaponSFX => _WeaponSFX;

    [SerializeField] private Sprite _WeaponSprite;
    public Sprite WeaponSprite => _WeaponSprite;

    public void SetStatus(int id, int nameid, int descriptionid, int weapontype, int damage, int interval, int intervalmin, int weaponbuff, int projecttileid, int weaponsfx, Sprite weaponsprite) {
        _ID = id;
        _NameID = nameid;
        _DescriptionID = descriptionid;
        _WeaponType = weapontype;
        _Damage = damage;
        _Interval = interval;
        _IntervalMin = intervalmin;
        _WeaponBuff = weaponbuff;
        _ProjectTileID = projecttileid;
        _WeaponSFX = weaponsfx;
        _WeaponSprite = weaponsprite;
    }
}
