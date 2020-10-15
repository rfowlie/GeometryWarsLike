using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnPatternsCreator))]
public class SpawnPatternsCreatorEditor : Editor
{
    SpawnPatternsCreator o;
    [SerializeField] public static string assetName;
    [SerializeField] public static string assetPath;
    [SerializeField] public static Object folder;

    private void OnEnable()
    {
        o = (SpawnPatternsCreator)target;
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
            if(assetPath != null && assetName != string.Empty)
            {
                SO_SpawnPattern temp = new SO_SpawnPattern(assetName, o.GetPoints());
                AssetDatabase.CreateAsset(temp, assetPath + "/" + assetName + ".asset");
                //remove name after creation, ensures another asset isn't created with same name
                assetName = string.Empty;

                if(o.debugger != null)
                {
                    o.debugger.patterns.Add(temp);
                }
            }
            else
            {
                Debug.LogError("Either folder path or asset name are undefined!");
            }
        }
        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(o);
    }
}