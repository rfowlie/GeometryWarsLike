using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace PatternCreator
{
    [CustomEditor(typeof(Drawer))]
    public class DrawerEditor : Editor
    {
        //Drawer o;

        //private void OnEnable()
        //{
        //    o = (Drawer)target;
        //    o.p = new SO_PatternArray[0];
        //    o.c = new Color[0];
        //    o.toggles = new bool[0];
        //}

        //public override void OnInspectorGUI()
        //{
        //    //base.OnInspectorGUI();
        //    GUIStyle s = new GUIStyle();
        //    s.padding = new RectOffset(0, 0, 4, 4);
        //    s.stretchWidth = true;

        //    EditorGUILayout.BeginVertical();
        //    EditorGUILayout.LabelField("Drawer", EditorStyles.boldLabel);
        //    GUILayout.Box("Place already created SO_Patterns by adding them to the list.\n Use the remove button to eliminate specific patterns.");
        //    EditorGUILayout.Space(10);
        //    EditorGUILayout.BeginHorizontal();
        //    EditorGUILayout.LabelField("Size", GUILayout.ExpandWidth(false), GUILayout.MaxWidth(80));
        //    o.size = EditorGUILayout.IntField(o.size, GUILayout.ExpandWidth(false), GUILayout.MaxWidth(100));
        //    EditorGUILayout.EndHorizontal();

        //    if(o.p.Length != o.size)
        //    {
        //        o.p = ArrayEX.Resize(o.p, o.size);
        //    }
        //    if(o.c.Length != o.size)
        //    {
        //        o.c = ArrayEX.Resize(o.c, o.size, Color.white);
        //    }
        //    if(o.toggles.Length != o.size)
        //    {
        //        o.toggles = ArrayEX.Resize(o.toggles, o.size);
        //    }

        //    for (int i = 0; i < o.size; i++)
        //    {
        //        EditorGUILayout.BeginHorizontal();
        //        o.toggles[i] = EditorGUILayout.Toggle(o.toggles[i], GUILayout.MaxWidth(50));
        //        o.p[i] = EditorGUILayout.ObjectField(o.p[i], typeof(SO_PatternArray), GUILayout.MaxWidth(200)) as SO_PatternArray;
        //        o.c[i] = EditorGUILayout.ColorField(o.c[i],  GUILayout.MaxWidth(80));
        //        if(GUILayout.Button("Remove"))
        //        {
        //            o.p = ArrayEX.Remove(o.p, i);
        //            o.c = ArrayEX.Remove(o.c, i);
        //            o.toggles = ArrayEX.Remove(o.toggles, i);
        //            o.size--;
        //            i--;
        //        }
        //        EditorGUILayout.EndHorizontal();
        //    }

        //    EditorGUILayout.EndVertical();
        //    EditorUtility.SetDirty(o);
        //}
    }

}
