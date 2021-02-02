using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PatternCreator
{
    //calculate points in world space for shapes
    //**ensure to associate each enum with its rotation degrees
    public enum SpawnShape { CIRCLE, TRIANGLE = 120, SQUARE = 90, PENTAGON = 72, HEXAGON = 60, OCTAGON = 45, DECAGON = 36 }
    public static class Shapes
    {
        //master method
        public static Vector3[] GetShape(SpawnShape shape, int amountOfPoints, float radius, Vector3 rotationAxis, Vector3 spawnAxis, float angleOffset = 0f)
        {
            float radians = 0;
            //circle is edge case
            if (shape == SpawnShape.CIRCLE)
            {
                amountOfPoints = amountOfPoints < 1 ? 1 : amountOfPoints;
                radians = Mathf.Deg2Rad * (360f / amountOfPoints);
            }
            else
            {
                radians = Mathf.Deg2Rad * (int)shape;
            }

            //gather all key points for shape
            List<Vector3> key = new List<Vector3>();
            key.Add(new Vector3(radius, 0, 0));
            while(true)
            {
                key.Add(RotateAroundOriginXY(key[key.Count - 1], radians));
                Debug.Log("Point: " + key[key.Count - 1]);
                if(key[key.Count - 1] == key[0]) { break; }
            }

            //setup array and prepare to add key points
            Vector3[] keyPoints = key.ToArray();
            amountOfPoints = amountOfPoints < keyPoints.Length ? keyPoints.Length : amountOfPoints;
            int fillerPoints = (amountOfPoints - keyPoints.Length) / keyPoints.Length;
            List<Vector3> points = new List<Vector3>();
            keyPoints[0] = new Vector3(radius, 0, 0);
            for (int i = 1; i < keyPoints.Length; i++)
            {
                keyPoints[i] = RotateAroundOriginXY(keyPoints[i - 1], radians);
            }

            //add all filler points
            for (int i = 0; i < keyPoints.Length - 1; i++)
            {
                points.Add(keyPoints[i]);
                Vector3[] temp = CalculateFillerPoints(keyPoints[i], keyPoints[i + 1], fillerPoints);
                for (int j = 0; j < temp.Length; j++)
                {
                    points.Add(temp[j]);
                }
            }

            //**get final loop from last point to first
            points.Add(keyPoints[keyPoints.Length - 1]);
            Vector3[] other = CalculateFillerPoints(keyPoints[keyPoints.Length - 1], keyPoints[0], fillerPoints);
            for (int j = 0; j < other.Length; j++)
            {
                points.Add(other[j]);
            }

            //setup rotation
            spawnAxis = Quaternion.AngleAxis(angleOffset, rotationAxis) * spawnAxis;
            Quaternion rot = Quaternion.LookRotation(rotationAxis, spawnAxis);


            //apply rotation to points
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = rot * points[i];
            }

            return points.ToArray();
        }

        //Utils
        private static Vector3 RotateAroundOriginXY(Vector3 point, float radians)
        {
            return new Vector3(point.x * Mathf.Cos(radians) - point.y * Mathf.Sin(radians), point.x * Mathf.Sin(radians) + point.y * Mathf.Cos(radians), 0f); 
        }

        private static Vector3[] CalculateFillerPoints(Vector3 start, Vector3 end, int amount)
        {
            Vector3[] temp = new Vector3[amount];
            for (int i = 0; i < amount; i++)
            {
                temp[i] = Vector3.Lerp(start, end, (float)(i + 1f) / (amount + 1f));
            }

            return temp;
        }
    }
}
