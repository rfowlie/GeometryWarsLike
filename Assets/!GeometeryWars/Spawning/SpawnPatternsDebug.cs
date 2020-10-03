using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPatternsDebug : MonoBehaviour
{
    public bool isOn = true;
    public SO_SpawnPattern pattern;

    private void OnDrawGizmos()
    {
        if(pattern != null && isOn)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < pattern.points.Length; i++)
            {
                Gizmos.DrawSphere(pattern.points[i], 0.5f);
            }
        }
    }
}
