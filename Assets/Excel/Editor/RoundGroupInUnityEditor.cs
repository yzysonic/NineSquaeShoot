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
[CustomEditor(typeof(RoundGroupInUnity))]
public class RoundGroupInUnityEditor : BaseExcelEditor<RoundGroupInUnity>
{	    
    public override bool Load()
    {
        RoundGroupInUnity targetData = target as RoundGroupInUnity;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<RoundGroupInUnityData>().ToArray();
            targetData.dataList = query.Deserialize<RoundGroupInUnityData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
