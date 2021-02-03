﻿using System.Collections;
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
            shape = SpawnShape.TRIANGLE;
            radius = 3;
            fillerPoints = 2;
            angleOffset = 0;
            viewPercentage = 100;
            towardsCenter = false;
        }

        //looks better???
        public string name;

        //creator position
        public Vector3 relativePosition;
        public Vector3 rotation;

        public SpawnShape shape;
        public float radius;
        public int fillerPoints;
        [Range(0, 360)] public float angleOffset;
        [Range(0,100)] public float viewPercentage;

        public bool towardsCenter;
    }
}