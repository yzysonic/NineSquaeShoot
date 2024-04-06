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
[CustomEditor(typeof(CharacterStatus))]
public class CharacterStatusEditor : BaseExcelEditor<CharacterStatus>
{	    
    public override bool Load()
    {
        CharacterStatus targetData = target as CharacterStatus;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<CharacterStatusData>().ToArray();
            targetData.dataList = query.Deserialize<CharacterStatusData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
