using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class CharacterDataInUnityAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Excel/002_角色表.xlsx";
    private static readonly string assetFilePath = "Assets/Excel/CharacterDataInUnity.asset";
    private static readonly string sheetName = "CharacterDataInUnity";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            CharacterDataInUnity data = (CharacterDataInUnity)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(CharacterDataInUnity));
            if (data == null) {
                data = ScriptableObject.CreateInstance<CharacterDataInUnity> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<CharacterDataInUnityData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<CharacterDataInUnityData>().ToArray();
                data.dataList = query.Deserialize<CharacterDataInUnityData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
