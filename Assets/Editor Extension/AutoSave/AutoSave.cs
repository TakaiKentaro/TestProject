using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public class AutoSave
{
    public static readonly string manualSaveKey = "autosave@manualSave";

    static double nextTime = 0;
    static bool isChangedHierarchy = false;

    static AutoSave()
    {
        IsManualSave = true;
    }

    public static bool IsManualSave
    {
        get { return EditorPrefs.GetBool(manualSaveKey); }
        private set { EditorPrefs.SetBool(manualSaveKey, value); }
    }
    
    [MenuItem("File/Backup/Backup")]
    public static void Backup ()
    {
        string expoertPath = "Backup/" + EditorApplication.currentScene;

        Directory.CreateDirectory (Path.GetDirectoryName (expoertPath));

        if( string.IsNullOrEmpty(EditorApplication.currentScene))
            return;

        byte[] data = File.ReadAllBytes (EditorApplication.currentScene);
        File.WriteAllBytes (expoertPath, data);
    }
}