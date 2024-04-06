using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class WeaponStatusAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Excel/NineSquareShootStatus.xlsx";
    private static readonly string assetFilePath = "Assets/Excel/WeaponStatus.asset";
    private static readonly string sheetName = "WeaponStatus";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            WeaponStatus data = (WeaponStatus)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(WeaponStatus));
            if (data == null) {
                data = ScriptableObject.CreateInstance<WeaponStatus> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<WeaponStatusData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<WeaponStatusData>().ToArray();
                data.dataList = query.Deserialize<WeaponStatusData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
