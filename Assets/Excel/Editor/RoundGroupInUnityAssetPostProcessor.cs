using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class RoundGroupInUnityAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Excel/004_戰鬥關卡表.xlsx";
    private static readonly string assetFilePath = "Assets/Excel/RoundGroupInUnity.asset";
    private static readonly string sheetName = "RoundGroupInUnity";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            RoundGroupInUnity data = (RoundGroupInUnity)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(RoundGroupInUnity));
            if (data == null) {
                data = ScriptableObject.CreateInstance<RoundGroupInUnity> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<RoundGroupInUnityData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<RoundGroupInUnityData>().ToArray();
                data.dataList = query.Deserialize<RoundGroupInUnityData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
