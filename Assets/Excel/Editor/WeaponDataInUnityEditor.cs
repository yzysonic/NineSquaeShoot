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
[CustomEditor(typeof(WeaponDataInUnity))]
public class WeaponDataInUnityEditor : BaseExcelEditor<WeaponDataInUnity>
{	    
    public override bool Load()
    {
        WeaponDataInUnity targetData = target as WeaponDataInUnity;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<WeaponDataInUnityData>().ToArray();
            targetData.dataList = query.Deserialize<WeaponDataInUnityData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
