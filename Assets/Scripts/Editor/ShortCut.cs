using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

public class ShortCut
{
    [MenuItem("OpenDrive/Excel/字串表")]
    public static void OpenSrringSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\001_String.xlsx");
    }

    [MenuItem("OpenDrive/Excel/角色資料表")]
    public static void OpenCharacterSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\002_Character.xlsx");
    }

    [MenuItem("OpenDrive/Excel/武器資料表")]
    public static void OpenWeaponSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\003_Weapon.xlsx");
    }

    [MenuItem("OpenDrive/Excel/關卡資料表")]
    public static void OpenStageSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\004_Stage.xlsx");
    }

    [MenuItem("OpenDrive/Excel/技能資料表")]
    public static void OpenSkillSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\005_SKill.xlsx");
    }

    [MenuItem("OpenDrive/Excel/Buff資料表")]
    public static void OpenBuffSheet() {
        Process.Start(@"D:\Unity Project\NineSquareShoot\Assets\Excel\006_Buff.xlsx");
    }

    [MenuItem("OpenDrive/Excel/聲音資料表")]
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
