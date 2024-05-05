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
[CustomEditor(typeof(CharacterDataInUnity))]
public class CharacterDataInUnityEditor : BaseExcelEditor<CharacterDataInUnity>
{	    
    public override bool Load()
    {
        CharacterDataInUnity targetData = target as CharacterDataInUnity;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<CharacterDataInUnityData>().ToArray();
            targetData.dataList = query.Deserialize<CharacterDataInUnityData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
