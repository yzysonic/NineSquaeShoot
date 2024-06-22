using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSS;

public class WaveController : Singleton<WaveController>
{

    [SerializeField] private int _CurrentWave;
    public int CurrentWave => _CurrentWave;

    Action<int> OnWaveChanged;

    protected override void Awake() {
        base.Awake();
        _CurrentWave = 1;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void RegisterWaveChanged(Action<int> callback) {
        OnWaveChanged += callback;
    }

    public void ChangeWave(int wave) {
        _CurrentWave += wave;
        OnWaveChanged(_CurrentWave);
    }

    public void ResetWave() {
        _CurrentWave = 1;
        OnWaveChanged(_CurrentWave);
    }
}
