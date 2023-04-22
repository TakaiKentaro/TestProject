using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public class AutoSave
{
    public static readonly string saveKey = "autosave@manualSave";

    static double timeInterval = 0;
    static bool isChangedHierarchy = false;

    static AutoSave()
    {
        isSave = true;
        EditorApplication.playmodeStateChanged += () =>
        {
            if (IsAutoSave && !EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                isSave = false;

                if (IsSavePrefab)
                {
                    AssetDatabase.SaveAssets();
                }

                if (IsSaveScene)
                {
                    Debug.Log("save scene " + System.DateTime.Now);
                    EditorApplication.SaveScene();
                }

                isSave = true;
            }

            isChangedHierarchy = false;
        };

        timeInterval = EditorApplication.timeSinceStartup + Interval;
        EditorApplication.update += () =>
        {
            if (isChangedHierarchy && timeInterval < EditorApplication.timeSinceStartup)
            {
                timeInterval = EditorApplication.timeSinceStartup + Interval;

                isSave = false;

                if (IsSaveSceneTimer && IsAutoSave && !EditorApplication.isPlaying)
                {
                    if (IsSavePrefab)
                    {
                        AssetDatabase.SaveAssets();
                    }
                    if (IsSaveScene)
                    {
                        Debug.Log("save scene " + System.DateTime.Now);
                        EditorApplication.SaveScene();
                    }
                }

                isChangedHierarchy = false;
                isSave = true;
            }
        };

        EditorApplication.hierarchyWindowChanged += () =>
        {
            if (!EditorApplication.isPlaying)
                isChangedHierarchy = true;
        };
    }

    public static bool isSave
    {
        get { return EditorPrefs.GetBool(saveKey); }
        private set { EditorPrefs.SetBool(saveKey, value); }
    }


    private static readonly string autoSave = "auto save";

    static bool IsAutoSave
    {
        get
        {
            string value = EditorUserSettings.GetConfigValue(autoSave);
            return !string.IsNullOrEmpty(value) && value.Equals("True");
        }
        set { EditorUserSettings.SetConfigValue(autoSave, value.ToString()); }
    }

    private static readonly string autoSavePrefab = "auto save prefab";

    static bool IsSavePrefab
    {
        get
        {
            string value = EditorUserSettings.GetConfigValue(autoSavePrefab);
            return !string.IsNullOrEmpty(value) && value.Equals("True");
        }
        set { EditorUserSettings.SetConfigValue(autoSavePrefab, value.ToString()); }
    }

    private static readonly string autoSaveScene = "auto save scene";

    static bool IsSaveScene
    {
        get
        {
            string value = EditorUserSettings.GetConfigValue(autoSaveScene);
            return !string.IsNullOrEmpty(value) && value.Equals("True");
        }
        set { EditorUserSettings.SetConfigValue(autoSaveScene, value.ToString()); }
    }

    private static readonly string autoSaveSceneTimer = "auto save scene timer";

    static bool IsSaveSceneTimer
    {
        get
        {
            string value = EditorUserSettings.GetConfigValue(autoSaveSceneTimer);
            return !string.IsNullOrEmpty(value) && value.Equals("True");
        }
        set { EditorUserSettings.SetConfigValue(autoSaveSceneTimer, value.ToString()); }
    }

    private static readonly string autoSaveInterval = "save scene interval";

    static int Interval
    {
        get
        {
            string value = EditorUserSettings.GetConfigValue(autoSaveInterval);
            if (value == null)
            {
                value = "60";
            }

            return int.Parse(value);
        }
        set
        {
            if (value < 60)
                value = 60;
            EditorUserSettings.SetConfigValue(autoSaveInterval, value.ToString());
        }
    }


    [PreferenceItem("Auto Save")]
    static void ExampleOnGUI()
    {
        bool isAutoSave = EditorGUILayout.BeginToggleGroup("auto save", IsAutoSave);

        IsAutoSave = isAutoSave;
        EditorGUILayout.Space();

        IsSavePrefab = EditorGUILayout.ToggleLeft("save prefab", IsSavePrefab);
        IsSaveScene = EditorGUILayout.ToggleLeft("save scene", IsSaveScene);
        IsSaveSceneTimer = EditorGUILayout.BeginToggleGroup("save scene interval", IsSaveSceneTimer);
        Interval = EditorGUILayout.IntField("interval(sec) min60sec", Interval);
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.EndToggleGroup();
    }

    [MenuItem("File/Backup/Backup")]
    public static void Backup()
    {
        string expoertPath = "Backup/" + EditorApplication.currentScene;

        Directory.CreateDirectory(Path.GetDirectoryName(expoertPath));

        if (string.IsNullOrEmpty(EditorApplication.currentScene))
            return;

        byte[] data = File.ReadAllBytes(EditorApplication.currentScene);
        File.WriteAllBytes(expoertPath, data);
    }

    [MenuItem("File/Backup/Rollback")]
    public static void RollBack()
    {
        string expoertPath = "Backup/" + EditorApplication.currentScene;

        byte[] data = File.ReadAllBytes(expoertPath);
        File.WriteAllBytes(EditorApplication.currentScene, data);
        AssetDatabase.Refresh(ImportAssetOptions.Default);
    }
}