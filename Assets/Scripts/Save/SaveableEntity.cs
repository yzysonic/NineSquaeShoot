using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class SaveableEntity : MonoBehaviour
{

    [ReadOnly]
    [SerializeField] private string id = string.Empty;

    public string ID => id;                                                                         //用來辨別的數字

    [ContextMenu("Generate ID")]
    void GenerateID() {                                                                             //產生ID
        id = Guid.NewGuid().ToString();
    }

    public SaveData CaptureState() {                                                                //將所要儲存的資料抓取後先轉成Json字串
        return new SaveData {
            data = JsonUtility.ToJson(gameObject.GetComponent<ISaveable>().CaptureState())
        };
    }

    public void RestoreState(SaveData State) {                                                      //回復資料
        var saveData = State.data;
        gameObject.GetComponent<ISaveable>().RestoreState(saveData);
    }

    [Serializable]
    public class SaveData {                                                                         //要儲存的資料
        public string data;
    }
}
