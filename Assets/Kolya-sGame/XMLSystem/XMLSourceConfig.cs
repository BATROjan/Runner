using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XMLSourceConfig", menuName = "Configs/XMLSourceConfig")]
public class XMLSourceConfig : ScriptableObject
{
    public string SourcePath;
    public string SaveName;
}
