using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//example of custom editor that opens custom editor window
[CustomEditor(typeof(SO_Test))]
public class GameDataObjectCustomeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        if (GUILayout.Button("Open Editor"))
        {
            GameDataObjectEditorWindow.OpenWindow((SO_Test)target);
        }
    }
}
