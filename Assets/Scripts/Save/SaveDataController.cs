using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataController : MonoBehaviour, ISaveable
{
    public static SaveDataController Instance;

    public SaveData Data;

    private SaveSystem SaveController;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SaveController = GetComponent<SaveSystem>();
    }

    // Start is called before the first frame update
    void Start() {
        SaveController.Load();
    }

    // Update is called once per frame
    void Update() {

    }

    public object CaptureState() {
        return Data;
    }

    public void RestoreState(string State) {
        var saveData = JsonUtility.FromJson<SaveData>(State);
        Data.playerData = saveData.playerData;
    }
}
