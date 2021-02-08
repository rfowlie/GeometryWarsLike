using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using PatternCreator;


[CustomEditor(typeof(SO_PatternInfoContainer))]
public class PatternInfoContainerEditor : Editor
{
    SO_PatternInfoContainer o;

    private void OnEnable()
    {
        o = (SO_PatternInfoContainer)target;   
    }
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Window"))
        {
            PatternInfoContainerWindow.OpenWindow((SO_PatternInfoContainer)target);
        }

        //update changes 
        EditorUtility.SetDirty(o);

        //base.OnInspectorGUI();
    }
}
