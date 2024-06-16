using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class ProjecttileDataInUnityAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/Excel/003_武器表.xlsx";
    private static readonly string assetFilePath = "Assets/Excel/ProjecttileDataInUnity.asset";
    private static readonly string sheetName = "ProjecttileDataInUnity";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            ProjecttileDataInUnity data = (ProjecttileDataInUnity)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ProjecttileDataInUnity));
            if (data == null) {
                data = ScriptableObject.CreateInstance<ProjecttileDataInUnity> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<ProjecttileDataInUnityData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<ProjecttileDataInUnityData>().ToArray();
                data.dataList = query.Deserialize<ProjecttileDataInUnityData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
