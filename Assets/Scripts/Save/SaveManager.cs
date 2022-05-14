using UnityEngine;
using System.IO;

namespace NSS
{
    public static class SaveManager
    {
        public static readonly string SaveDataPath = Application.persistentDataPath + "/SaveData.json";

        public static SaveData SaveData { get; private set; }

        public static void SaveChanges()
        {
            if (SaveData == null)
            {
                return;
            }

            using (StreamWriter writer = new(SaveDataPath))
            {
                string data = JsonUtility.ToJson(SaveData);
                writer.Write(data);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadData()
        {
            if (!File.Exists(SaveDataPath))
            {
                SaveData = new SaveData();
                return;
            }

            using (StreamReader reader = new(SaveDataPath))
            {
                string data = reader.ReadToEnd();
                SaveData = JsonUtility.FromJson<SaveData>(data);
            }
        }
    }
}
