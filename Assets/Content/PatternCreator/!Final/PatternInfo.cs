using UnityEngine;


namespace PatternCreator
{
    //Gather all relevant information for spawning the pattern that DOESN'T include actually calculating the points

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
            radiusSecond = 2;
            fillerAmount = 2;
            circlePoints = 10;
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
        public float radiusSecond;
        public int fillerAmount;
        public int circlePoints;
        [Range(0, 360)] public float angleOffset;
        [Range(0,100)] public float viewPercentage;

        public bool towardsCenter;
    }
}