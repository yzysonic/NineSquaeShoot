using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityQuickSheet;
using System.Linq;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(ProjecttileDataInUnity))]
public class ProjecttileDataInUnityEditor : BaseExcelEditor<ProjecttileDataInUnity>
{	    
    public override bool Load()
    {
        ProjecttileDataInUnity targetData = target as ProjecttileDataInUnity;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<ProjecttileDataInUnityData>().ToArray();
            targetData.dataList = query.Deserialize<ProjecttileDataInUnityData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
