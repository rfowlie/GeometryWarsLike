using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnPatterns))]
public class SpawnPatternsEditor : Editor
{
    SpawnPatterns spawnPatterns;
    [SerializeField] public static string assetName;
    [SerializeField] public static string assetPath;
    [SerializeField] public static Object folder;

    private void OnEnable()
    {
        spawnPatterns = (SpawnPatterns)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space(20f);
        GUILayout.Label("Editor", EditorStyles.boldLabel);
        assetName = EditorGUILayout.TextField("Name", assetName);
        folder = EditorGUILayout.ObjectField("Folder", folder, typeof(Object));
        assetPath = AssetDatabase.GetAssetPath(folder);
        if (GUILayout.Button("Create"))
        {
            //Debug.Log("Create Spawn Pattern");
            SO_SpawnPattern temp = new SO_SpawnPattern(assetName, spawnPatterns.GetPoints());
            AssetDatabase.CreateAsset(temp, assetPath + "/" + assetName + ".asset");
        }
        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(spawnPatterns);
    }
}