using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{

    private Text WaveText;

    void Awake() {
        WaveText = GetComponent<Text>();
        WaveText.text = "1";
        if (WaveController.IsCreated) {
            WaveController.Instance.RegisterWaveChanged(OnWaveChanged);
        }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnWaveChanged(int Wave) {
        WaveText.text = Wave.ToString();
    }
}
