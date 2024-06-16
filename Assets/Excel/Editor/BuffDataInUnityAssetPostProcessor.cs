using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class BuffDataInUnityAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Excel/006_Buff.xlsx";
    private static readonly string assetFilePath = "Assets/Excel/BuffDataInUnity.asset";
    private static readonly string sheetName = "BuffDataInUnity";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            BuffDataInUnity data = (BuffDataInUnity)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(BuffDataInUnity));
            if (data == null) {
                data = ScriptableObject.CreateInstance<BuffDataInUnity> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<BuffDataInUnityData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<BuffDataInUnityData>().ToArray();
                data.dataList = query.Deserialize<BuffDataInUnityData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
