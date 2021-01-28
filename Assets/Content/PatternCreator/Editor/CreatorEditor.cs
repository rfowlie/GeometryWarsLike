using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace PatternCreator
{
    [CustomEditor(typeof(Creator))]
    public class CreatorEditor : Editor
    {
        Creator o;

        [SerializeField] public static string assetName = string.Empty;
        [SerializeField] public static string assetPath = string.Empty;
        [SerializeField] public static Object folder = null;

        private void OnEnable()
        {
            o = (Creator)target;
        }

        public override void OnInspectorGUI()
        {

            EditorGUILayout.BeginVertical();
            if(GUILayout.Button("Reset"))
            {
                o.ResetValues();
            }
            EditorGUILayout.Space(10f);

            base.OnInspectorGUI();

            //add to temp list
            if (GUILayout.Button("Store"))
            {
                o.isOn = EditorGUILayout.Toggle("Display Stored Patterns", o.isOn);
                o.Store();
            }

            //create style
            GUIStyle s = new GUIStyle();
            s.padding = new RectOffset(0, 0, 4, 4);
            s.stretchWidth = true;

            EditorGUILayout.Space(30f);
            EditorGUILayout.LabelField("Stored Patterns", EditorStyles.boldLabel);
            o.isOn = EditorGUILayout.Toggle("Show", o.isOn);
            EditorGUILayout.BeginHorizontal(s);
            if(GUILayout.Button("SelectAll"))
            {
                for (int i = 0; i < o.toggles.Count; i++)
                {
                    o.toggles[i] = true;
                }
            }
            if(GUILayout.Button("DecselectAll"))
            {
                for(int i = 0; i < o.toggles.Count; i++)
                {
                    o.toggles[i] = false;
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10f);
            GUILayout.Label("Patterns", EditorStyles.boldLabel);

            
            //display values in list
            for (int i = 0; i < o.patternNames.Count; i++)
            {
                //allow name to be adjustable after the fact...                
                EditorGUILayout.BeginHorizontal(s);
                o.toggles[i] = EditorGUILayout.Toggle(o.toggles[i]);
                o.patternNames[i] = EditorGUILayout.TextField(o.patternNames[i], GUILayout.MaxWidth(140));
                o.colors[i] = EditorGUILayout.ColorField(o.colors[i], GUILayout.MaxWidth(80));
                if (GUILayout.Button("Edit", GUILayout.MaxWidth(100)))
                {
                    o.Edit(i);
                }
                if (GUILayout.Button("Remove", GUILayout.MaxWidth(100)))
                {
                    if (o.patterns.Count > 0)
                    {
                        o.RemoveStored(i);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(5f);
            if (GUILayout.Button("Clear"))
            {
                o.ClearStored();
            }

            EditorGUILayout.Space(30f);
            GUILayout.Label("Save Pattern", EditorStyles.boldLabel);
            folder = EditorGUILayout.ObjectField("Folder", folder, typeof(Object));
            assetName = EditorGUILayout.TextField("Asset Name", assetName);
            assetPath = AssetDatabase.GetAssetPath(folder);
            if (GUILayout.Button("Save"))
            {
                if(assetPath == null) { Debug.LogError("Asset folder undefined!"); }
                else if(assetName == string.Empty) { Debug.LogError("AssetName undefined!"); }
                else
                {
                    //new version where we store the info rather than the points
                    SO_PatternInfoContainer container = new SO_PatternInfoContainer();
                    container.values = o.GetAllPatternInfo();
                    AssetDatabase.CreateAsset(container, assetPath + "/PatternInfoContainer_" + assetName + ".asset");

                    //remove name after creation, ensures another asset isn't created with same name
                    assetName = string.Empty;
                }
            }

            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(o);
        }
    }
}