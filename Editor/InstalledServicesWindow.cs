using UnityEditor;
using UnityEngine;

public class InstalledServicesWindow : EditorWindow
{
    [MenuItem("Tools/Installed services")]
    public static void ShowWindow()
    {
        GetWindow(typeof(InstalledServicesWindow));
    }
    
    void OnGUI()
    {
        if (GUILayout.Button("Clear all"))
        {
            ServiceLocator.Clear();
        }
        GUILayout.Label ("Installed services:", EditorStyles.boldLabel);

        foreach (var kvp in ServiceLocator.AllInstalledServices)
        {
            EditorGUILayout.LabelField(kvp.Key.FullName,
                kvp.Value.ToString());
        }
        this.Repaint();
    }
}
