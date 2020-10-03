    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPatter" , menuName = "ScriptableObjects/Spawn/Pattern")]
public class SO_SpawnPattern : ScriptableObject
{
    public SO_SpawnPattern(string iD, Vector3[] points)
    {
        this.iD = iD;
        this.points = points;
    }

    public string iD = "Empty";
    public Vector3[] points;
}
