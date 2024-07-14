using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData playerData;
}

[Serializable]
public class PlayerData
{
    public int CurrentWeaponID = 1;
    public int CurrentCharacterID = 1;
}
