using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//display gizmo debug for SO_SpawnPatterns created with SpawnCreator
public class SpawnPatternsDebugger : MonoBehaviour
{
    public bool isOn = true;
    public List<SO_SpawnPattern> patterns = new List<SO_SpawnPattern>();
    

    private void OnDrawGizmos()
    {
        if(patterns != null && isOn)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < patterns.Count; i++)
            {
                //skip null slots
                if(patterns[i].points == null) { continue; }
                for (int j = 0; j < patterns[i].points.Length; j++)
                {
                    Gizmos.DrawSphere(patterns[i].points[j], 0.5f);
                }
            }
        }
    }
}
