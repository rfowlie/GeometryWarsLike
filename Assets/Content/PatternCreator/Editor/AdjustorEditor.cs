using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PatternCreator
{
    [CustomEditor(typeof(Adjustor))]
    public class AdjustorEditor : Editor
    {
        private Adjustor o;


        private void OnEnable()
        {
            o = (Adjustor)target;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            o.currentInfo.name = EditorGUILayout.TextField("Name", o.currentInfo.name);
            o.currentInfo.shape = (SpawnShape)EditorGUILayout.EnumPopup("Shape", o.currentInfo.shape);
            o.currentInfo.radius = EditorGUILayout.FloatField("Radius", o.currentInfo.radius);
            if(o.currentInfo.shape == SpawnShape.CIRCLE)
            {
                o.currentInfo.fillerPoints = EditorGUILayout.IntField("Total Points", o.currentInfo.fillerPoints);
            }
            else
            {
                o.currentInfo.fillerPoints = EditorGUILayout.IntField("FillerPoints", o.currentInfo.fillerPoints);
            }
            o.currentInfo.angleOffset = EditorGUILayout.Slider("AngleOffset", o.currentInfo.angleOffset, 0f, 360f);
            o.currentInfo.viewPercentage = EditorGUILayout.Slider("ViewPercentage", o.currentInfo.viewPercentage, 0f, 100f);
            EditorUtility.SetDirty(o);
        }
    }
}