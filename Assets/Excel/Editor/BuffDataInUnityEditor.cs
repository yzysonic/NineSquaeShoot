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
[CustomEditor(typeof(BuffDataInUnity))]
public class BuffDataInUnityEditor : BaseExcelEditor<BuffDataInUnity>
{	    
    public override bool Load()
    {
        BuffDataInUnity targetData = target as BuffDataInUnity;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<BuffDataInUnityData>().ToArray();
            targetData.dataList = query.Deserialize<BuffDataInUnityData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
