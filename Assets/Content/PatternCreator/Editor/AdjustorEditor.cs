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
            base.OnInspectorGUI();
        }
    }
}