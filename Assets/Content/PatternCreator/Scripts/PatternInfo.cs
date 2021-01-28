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
        public PatternInfo(string name, Vector3 relativePosition)
        {
            this.name = name;
            this.relativePosition = relativePosition;
            rotation = Vector3.zero;
            amountOfPoints = 12;
            radius = 3;
            angleOffset = 0;
            percentage = 100;
            shape = SpawnShape.CIRCLE;
            towardsCenter = false;
        }

        //looks better???
        public string name;

        //creator position
        public Vector3 relativePosition;
        public Vector3 rotation;

        public int amountOfPoints;
        public float radius;
        [Range(0, 360)] public float angleOffset;
        [Range(0,100)] public float percentage;
        public SpawnShape shape;

        public bool towardsCenter;
    }
}