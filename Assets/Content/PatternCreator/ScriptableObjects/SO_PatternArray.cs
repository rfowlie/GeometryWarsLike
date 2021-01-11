    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternArray" , menuName = "ScriptableObjects/PatternArray")]
public class SO_PatternArray : ScriptableObject
{
    public SO_PatternArray(string iD, Vector3[] points)
    {
        this.iD = iD;
        this.points = points;
    }

    public string iD = "Empty";
    public Vector3[] points;
}
