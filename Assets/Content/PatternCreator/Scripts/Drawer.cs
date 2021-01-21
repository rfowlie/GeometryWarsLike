using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PatternCreator
{
    //display gizmo debug for SO_SpawnPatterns created with SpawnCreator
    public class Drawer : MonoBehaviour
    {
        public bool isOn = true;

        public int size = 1;
        public SO_PatternArray[] p;
        public Color[] c;
        public bool[] toggles;

       
        private void OnDrawGizmos()
        {
            if (p != null && isOn)
            {                
                for (int i = 0; i < p.Length; i++)
                {
                    if(toggles[i])
                    {
                        Gizmos.color = c[i];
                        //skip null slots
                        if (p[i] == null) { continue; }
                        for (int j = 0; j < p[i].points.Length; j++)
                        {
                            Gizmos.DrawSphere(p[i].points[j], 0.5f);
                        }
                    }                    
                }
            }
        }
    }
}

