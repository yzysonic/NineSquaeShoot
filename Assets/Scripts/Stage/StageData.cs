using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData
{
    [SerializeField] private int _ID;
    public int ID => _ID;

    [SerializeField] private int _MaxWave;
    public int MaxWave => _MaxWave;

    [SerializeField] private List<int> _RoundGroupList;
    public List<int> RoundGroupList => _RoundGroupList;

    public void SetStageData(int id, int maxwave, List<int> roundgrouplist) {
        _ID = id;
        _MaxWave = maxwave;
        _RoundGroupList = roundgrouplist;
    }
}

[Serializable]
public class RoundGroupData
{
    [SerializeField] private int _ID;
    public int ID => _ID;

    [SerializeField] private int _MonsterID;
    public int MonsterID => _MonsterID;

    [SerializeField] private List<int> _CreateGridList;
    public List<int> CreateGridList => _CreateGridList;

    public RoundGroupData(int id, int monsterid, int[] creategridarray) {
        _ID = id;
        _MonsterID = monsterid;
        _CreateGridList = creategridarray.ToList();
    }
}
