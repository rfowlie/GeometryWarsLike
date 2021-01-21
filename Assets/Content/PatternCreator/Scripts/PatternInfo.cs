using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PatternCreator
{
    //Gather all relevant information for spawning the pattern that DOESN'T include actually calculating the points
    //TODO: probably don't need quaternion as vector will be map position - patternInfo position


    [System.Serializable]
    public struct PatternInfo
    {
        //creator position
        public Vector3 relativePosition;
        public Quaternion rotation;

        public int amountOfPoints;
        public float radius;
        public float angleOffset;
        public float percentage;
        public SpawnShape shape;

        public bool towardsCenter;
    }
}