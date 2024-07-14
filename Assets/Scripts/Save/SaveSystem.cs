using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    private string FileName = "SaveData.json";

    public void Save() {                                                                            //儲存
        var State = new DiciotnaryOfState();
        State = LoadFile();
        CaptureState(State);
        SaveFile(State);
    }

    public void Load() {                                                                            //讀取
        var State = new DiciotnaryOfState();
        State = LoadFile();
        RestoreState(State);
    }

    DiciotnaryOfState LoadFile() {                                                                  //將儲存的資料讀取後放進可序列化的Dictionary裡
        string JsonData = "";
        string path = Path.Combine(Application.persistentDataPath, FileName);
        if (File.Exists(path)) {
            JsonData = File.ReadAllText(path);
        }
        //DatabaseAccess.Instance.CatchFileFromDatabase(PlayerData.UserName, out JsonData);           //從資料庫裡取得資料
        if (JsonData == "") {
            return new DiciotnaryOfState();
        }
        return JsonUtility.FromJson<DiciotnaryOfState>(JsonData);                                   //將資料從Json檔裡還原出來
    }

    void SaveFile(object State) {                                                                   //儲存資料到資料庫裡
        var SerializedData = JsonUtility.ToJson(State);                                             //將資料轉成Json檔
        string path = Path.Combine(Application.persistentDataPath, FileName);
        File.WriteAllText(path, SerializedData);
        //DatabaseAccess.Instance.UpdateFileToDatabase(PlayerData.UserName, SerializedData);          //將資料存到資料庫裡
    }

    void CaptureState(DiciotnaryOfState State) {                                                    //抓取所有需要儲存的資料
        foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {
            if (State.ContainsKey(saveable.ID)) {                                                   //從所有有掛著SaveableEntity的物件上抓取資料丟進Dictionary裡
                State[saveable.ID] = saveable.CaptureState();
            }
            else {
                State.Add(saveable.ID, saveable.CaptureState());
            }
        }
    }

    void RestoreState(DiciotnaryOfState State) {                                                    //回復所有儲存的資料
        SaveableEntity.SaveData value;
        foreach (var saveable in FindObjectsOfType<SaveableEntity>()) {                        
            if (State.TryGetValue(saveable.ID, out value)) {                                        //從從所有有掛著SaveableEntity的物件上回復所有資料
                saveable.RestoreState(value);
            }
        }
    }
    
    [Serializable]
    public class DiciotnaryOfState : SerializeDictionary<string, SaveableEntity.SaveData> { }       //可序列化的Dictionary
}
