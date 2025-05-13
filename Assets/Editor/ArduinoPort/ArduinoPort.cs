using UnityEditor;
using UnityEditor.PackageManager;

public static class ArduinoPort
{
    /// <summary>
    /// 
    /// </summary>
    [InitializeOnLoadMethod]
    static void InitializeOnLoad()
    {
        var TargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

        PlayerSettings.SetApiCompatibilityLevel(TargetGroup, ApiCompatibilityLevel.NET_Unity_4_8);
    }
}
