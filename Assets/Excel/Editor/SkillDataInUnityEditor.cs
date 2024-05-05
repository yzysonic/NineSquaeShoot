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
[CustomEditor(typeof(SkillDataInUnity))]
public class SkillDataInUnityEditor : BaseExcelEditor<SkillDataInUnity>
{	    
    public override bool Load()
    {
        SkillDataInUnity targetData = target as SkillDataInUnity;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<SkillDataInUnityData>().ToArray();
            targetData.dataList = query.Deserialize<SkillDataInUnityData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
