using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneBackup : AssetModificationProcessor
{
    static string[] OnWillSaveAssets (string[] paths)
    {
        bool manualSave = AutoSave.isSave;
        if (manualSave) {
            AutoSave.Backup ();
        }

        return paths;
    }
}