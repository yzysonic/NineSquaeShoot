using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class SoundDataInUnityAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Excel/007_Sound.xlsx";
    private static readonly string assetFilePath = "Assets/Excel/SoundDataInUnity.asset";
    private static readonly string sheetName = "SoundDataInUnity";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            SoundDataInUnity data = (SoundDataInUnity)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(SoundDataInUnity));
            if (data == null) {
                data = ScriptableObject.CreateInstance<SoundDataInUnity> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<SoundDataInUnityData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<SoundDataInUnityData>().ToArray();
                data.dataList = query.Deserialize<SoundDataInUnityData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
