using System.IO;
using UnityEditor;

public class SceneBackup : AssetModificationProcessor
{
    static string[] OnWillSaveAssets (string[] paths)
    {
        bool manualSave = AutoSave.IsManualSave;
        if (manualSave) {
            AutoSave.Backup ();
        }

        return paths;
    }
    
    
}
