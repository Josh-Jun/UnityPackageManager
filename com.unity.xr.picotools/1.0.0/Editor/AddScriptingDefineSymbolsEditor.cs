using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AddScriptingDefineSymbolsEditor
{
    static AddScriptingDefineSymbolsEditor()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "PICO_XR_SETTING");
    }
}