using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PatternCreator
{
    [CustomEditor(typeof(Pointer))]
    public class PointerEditor : Editor
    {
        private Pointer o;

        private void OnEnable()
        {
            o = (Pointer)target;
        }

        public override void OnInspectorGUI()
        {
            switch(o.currentInfo.shape)
            {
                case SpawnShape.CIRCLE:
                    Circle();
                    break;
                case SpawnShape.STAR:
                    Star();
                    break;
                case SpawnShape.X:
                    Cross();
                    break;
                case SpawnShape.CheckMark:
                    CheckMark();
                    break;
                default:
                    DefaultView();
                    break;
            }

            EditorUtility.SetDirty(o);
        }

        //create unique layouts for certain shapes
        private void DefaultView()
        {
            o.currentInfo.name = EditorGUILayout.TextField("Name", o.currentInfo.name);
            o.currentInfo.shape = (SpawnShape)EditorGUILayout.EnumPopup("Shape", o.currentInfo.shape);
            o.currentInfo.radius = EditorGUILayout.FloatField("Radius", o.currentInfo.radius);
            o.currentInfo.fillerAmount = EditorGUILayout.IntField("FillerAmount", o.currentInfo.fillerAmount);
            o.currentInfo.angleOffset = EditorGUILayout.Slider("AngleOffset", o.currentInfo.angleOffset, 0f, 360f);
            o.currentInfo.viewPercentage = EditorGUILayout.Slider("ViewPercentage", o.currentInfo.viewPercentage, 0f, 100f);
        }
        private void Circle()
        {
            o.currentInfo.name = EditorGUILayout.TextField("Name", o.currentInfo.name);
            o.currentInfo.shape = (SpawnShape)EditorGUILayout.EnumPopup("Shape", o.currentInfo.shape);
            o.currentInfo.radius = EditorGUILayout.FloatField("Radius", o.currentInfo.radius);
            o.currentInfo.circlePoints = EditorGUILayout.IntField("Total Points", o.currentInfo.circlePoints);
            o.currentInfo.angleOffset = EditorGUILayout.Slider("AngleOffset", o.currentInfo.angleOffset, 0f, 360f);
            o.currentInfo.viewPercentage = EditorGUILayout.Slider("ViewPercentage", o.currentInfo.viewPercentage, 0f, 100f);
        }
        private void Star()
        {
            o.currentInfo.name = EditorGUILayout.TextField("Name", o.currentInfo.name);
            o.currentInfo.shape = (SpawnShape)EditorGUILayout.EnumPopup("Shape", o.currentInfo.shape);
            o.currentInfo.radius = EditorGUILayout.FloatField("Radius", o.currentInfo.radius);
            o.currentInfo.radiusSecond = EditorGUILayout.FloatField("Radius", o.currentInfo.radiusSecond);
            o.currentInfo.fillerAmount = EditorGUILayout.IntField("FillerAmount", o.currentInfo.fillerAmount);
            o.currentInfo.angleOffset = EditorGUILayout.Slider("AngleOffset", o.currentInfo.angleOffset, 0f, 360f);
            o.currentInfo.viewPercentage = EditorGUILayout.Slider("ViewPercentage", o.currentInfo.viewPercentage, 0f, 100f);
        }
        private void Cross()
        {
            o.currentInfo.name = EditorGUILayout.TextField("Name", o.currentInfo.name);
            o.currentInfo.shape = (SpawnShape)EditorGUILayout.EnumPopup("Shape", o.currentInfo.shape);
            o.currentInfo.radius = EditorGUILayout.FloatField("Radius", o.currentInfo.radius);
            o.currentInfo.fillerAmount = EditorGUILayout.IntField("FillerAmount", o.currentInfo.fillerAmount);
            o.currentInfo.angleOffset = EditorGUILayout.Slider("AngleOffset", o.currentInfo.angleOffset, 0f, 360f);
        }

        private void CheckMark()
        {
            o.currentInfo.name = EditorGUILayout.TextField("Name", o.currentInfo.name);
            o.currentInfo.shape = (SpawnShape)EditorGUILayout.EnumPopup("Shape", o.currentInfo.shape);
            o.currentInfo.radius = EditorGUILayout.FloatField("Radius left", o.currentInfo.radius);
            o.currentInfo.radiusSecond = EditorGUILayout.FloatField("Radius Right", o.currentInfo.radiusSecond);
            o.currentInfo.fillerAmount = EditorGUILayout.IntField("FillerAmount", o.currentInfo.fillerAmount);
            o.currentInfo.angleOffset = EditorGUILayout.Slider("AngleOffset", o.currentInfo.angleOffset, 0f, 360f);
            //o.currentInfo.viewPercentage = EditorGUILayout.Slider("ViewPercentage", o.currentInfo.viewPercentage, 0f, 100f);
        }
    }
}