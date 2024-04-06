using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class CharacterStatusAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Excel/NineSquareShootStatus.xlsx";
    private static readonly string assetFilePath = "Assets/Excel/CharacterStatus.asset";
    private static readonly string sheetName = "CharacterStatus";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            CharacterStatus data = (CharacterStatus)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(CharacterStatus));
            if (data == null) {
                data = ScriptableObject.CreateInstance<CharacterStatus> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<CharacterStatusData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<CharacterStatusData>().ToArray();
                data.dataList = query.Deserialize<CharacterStatusData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
