using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public abstract class EasyEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;

    private string selectedPropertyPath;
    protected SerializedProperty selectedProperty;

    protected abstract void DrawSelectedProperyPanel();
    protected abstract SerializedProperty FirstProperty();
    protected virtual void OnGUI()
    {
        currentProperty = FirstProperty();
    }

    //NEEDS WORK
    //Displays it just like it was in an inspector
    protected void DrawProperties(SerializedProperty prop, bool drawChildren)
    {
        string lastPropPath = string.Empty;
        foreach(SerializedProperty p in prop)
        {
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                if (p.isExpanded)
                {
                    //this addes indentation...
                    EditorGUI.indentLevel += 2;
                    //recursive call
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel -= 2;
                }
            }
            else
            {
                //WHAT DOES THIS DO???
                if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)) { continue; }
                lastPropPath = p.propertyPath;
                EditorGUILayout.PropertyField(p, drawChildren);
            }            
        }
    }

    protected void DrawArrayAsSideBar(string propertyName, System.Action elementsDisplay)
    {
        SerializedProperty prop = serializedObject.FindProperty(propertyName);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(100), GUILayout.ExpandHeight(false));
        foreach (SerializedProperty p in prop)
        {
            if (GUILayout.Button(p.displayName))
            {
                //cache selection
                selectedPropertyPath = p.propertyPath;
            }
            if (!string.IsNullOrEmpty(selectedPropertyPath))
            {
                selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));
        if (currentProperty != null)
        {
            elementsDisplay();
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    protected void DrawField(string propName, bool relative)
    {
        //not clear, but relative property I think means if it is inside of another property...
        if(relative && currentProperty != null)
        {
            EditorGUILayout.PropertyField(currentProperty.FindPropertyRelative(propName), true);
        }
        else if(serializedObject != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(propName), true);
        }
    }
    protected void DrawArray(string propName, bool includeChildren = true)
    {
        SerializedProperty prop = serializedObject.FindProperty(propName);
        if (prop.isArray)
        {            
            EditorGUILayout.BeginVertical("box");
            foreach(SerializedProperty p in prop)
            {
                EditorGUILayout.PropertyField(p, includeChildren);
            }
            EditorGUILayout.EndVertical();
        }
        else
        {
            Debug.LogError("Not an array");
        }
    }

    //this will ensure all changes made in window get save in SO
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
