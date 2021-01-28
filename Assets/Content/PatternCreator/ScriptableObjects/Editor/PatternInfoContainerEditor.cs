using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using PatternCreator;


[CustomEditor(typeof(SO_PatternInfoContainer))]
public class PatternInfoContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Window"))
        {
            PatternInfoContainerWindow.OpenWindow((SO_PatternInfoContainer)target);
        }

        //base.OnInspectorGUI();
    }
}
