using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

public class ShortCut
{
    [MenuItem("OpenDrive/Excel/�r���")]
    public static void OpenSrringSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\001_String.xlsx");
    }

    [MenuItem("OpenDrive/Excel/�����ƪ�")]
    public static void OpenCharacterSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\002_Character.xlsx");
    }

    [MenuItem("OpenDrive/Excel/�Z����ƪ�")]
    public static void OpenWeaponSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\003_Weapon.xlsx");
    }

    [MenuItem("OpenDrive/Excel/���d��ƪ�")]
    public static void OpenStageSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\004_Stage.xlsx");
    }

    [MenuItem("OpenDrive/Excel/�ޯ��ƪ�")]
    public static void OpenSkillSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\005_SKill.xlsx");
    }

    [MenuItem("OpenDrive/Excel/Buff��ƪ�")]
    public static void OpenBuffSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\006_Buff.xlsx");
    }

    [MenuItem("OpenDrive/Excel/�n����ƪ�")]
    public static void OpenSoundSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\007_Sound.xlsx");
    }

    [MenuItem("OpenDrive/Jira")]
    public static void OpenJira() {
        string strCmdText;
        strCmdText = "/C start /b https://projectnine.atlassian.net/jira/software/projects/KAN/boards/1";
        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
    }
}
