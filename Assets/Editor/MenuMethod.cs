using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuMethod
{
    [MenuItem("Window/Delete save file")]
    private static void DeleteSaveFile()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("[SaveDataSystem] Save data delete");
    }
}
