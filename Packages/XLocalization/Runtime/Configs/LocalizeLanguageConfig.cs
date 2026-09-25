using System.Collections.Generic;
using UnityEngine;
using XLocalization.Datas;

[CreateAssetMenu(fileName = "LocalizeLanguageConfig", menuName = "XLocalization/LocalizeLanguageConfig")]
public class LocalizeLanguageConfig : ScriptableObject
{
    [HideInInspector]
    public List<SystemLanguage> Languages = new()
    {
        SystemLanguage.ChineseSimplified,
    };
}
