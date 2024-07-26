using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

public class ShortCut
{
    [MenuItem("OpenDrive/Excel/001_String")]
    public static void OpenSrringSheet() {
        string path = Application.dataPath.Replace("/", "\\") + "\\Excel\\001_String.xlsx";
        Process.Start(@path);
    }

    [MenuItem("OpenDrive/Excel/002_Character")]
    public static void OpenCharacterSheet() {
        string path = Application.dataPath.Replace("/", "\\") + "\\Excel\\002_Character.xlsx";
        Process.Start(@path);
    }

    [MenuItem("OpenDrive/Excel/003_Weapon")]
    public static void OpenWeaponSheet() {
        string path = Application.dataPath.Replace("/", "\\") + "\\Excel\\003_Weapon.xlsx";
        Process.Start(@path);
    }

    [MenuItem("OpenDrive/Excel/004_Stage")]
    public static void OpenStageSheet() {
        string path = Application.dataPath.Replace("/", "\\") + "\\Excel\\004_Stage.xlsx";
        Process.Start(@path);
    }

    [MenuItem("OpenDrive/Excel/005_SKill")]
    public static void OpenSkillSheet() {
        string path = Application.dataPath.Replace("/", "\\") + "\\Excel\\005_SKill.xlsx";
        Process.Start(@path);
    }

    [MenuItem("OpenDrive/Excel/006_Buff")]
    public static void OpenBuffSheet() {
        string path = Application.dataPath.Replace("/", "\\") + "\\Excel\\006_Buff.xlsx";
        Process.Start(@path);
    }

    [MenuItem("OpenDrive/Excel/007_Sound")]
    public static void OpenSoundSheet() {
        string path = Application.dataPath.Replace("/", "\\") + "\\Excel\\007_Sound.xlsx";
        Process.Start(@path);
    }

    [MenuItem("OpenDrive/Jira")]
    public static void OpenJira() {
        string strCmdText;
        strCmdText = "/C start /b https://projectnine.atlassian.net/jira/software/projects/KAN/boards/1";
        Process.Start("CMD.exe", strCmdText);
    }
}
