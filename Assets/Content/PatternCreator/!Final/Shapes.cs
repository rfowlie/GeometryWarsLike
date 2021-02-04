﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//store the required angles with each shape for calculating the points using rotations
public enum SpawnShape { TRIANGLE = 3, SQUARE = 4, PENTAGON = 5, HEXAGON = 6, OCTAGON = 8, DECAGON = 10,
                         CIRCLE, STAR, X, CheckMark }

public static class Shapes
{
    //all basic shapes can be created with this
    public static Vector3[] Simple(SpawnShape shape, float radius, float angleOffset = 0f, int fillerAmount = 0)
    {
        Vector3[] keyPoints = PrimaryPoints((int)shape, radius, angleOffset);

        //add filler
        return Filler(keyPoints, fillerAmount);
    }

    //circle is a special case, rotation dependant on amount of points
    public static Vector3[] Circle(float radius, float angleOffset, int amountOfPoints)
    {
        return PrimaryPoints(amountOfPoints, radius, angleOffset);
    }

    //odd or unique shapes
    public static Vector3[] Star(float outerRadius, float innerRadius, int fillerAmount)
    {
        //a star is two pentagons rotated 180 from each other...
        Vector3[] a = Simple(SpawnShape.PENTAGON, outerRadius);
        Vector3[] b = Simple(SpawnShape.PENTAGON, innerRadius, 36f);
        List<Vector3> keyPoints = new List<Vector3>();
        for (int i = 0; i < 5; i++)
        {
            keyPoints.Add(a[i]);
            keyPoints.Add(b[i]);
        }

        //add filler points
        return Filler(keyPoints.ToArray(), fillerAmount);           
    }

    public static Vector3[] Cross(float radius, float angleOffset = 0f, int fillerAmount = 0)
    {
        //prevent errors
        fillerAmount = fillerAmount < 0 ? 0 : fillerAmount;

        //can't make points from usual method
        List<Vector3> points = new List<Vector3>();
        //add all filler points
        //Vector3 main = RotateAroundOriginXY(Vector3.up * radius, Mathf.Deg2Rad * angleOffset);
        Vector3 main = Vector3.up * radius;
        points.Add(Vector3.zero);
        Vector3[] temp = CalculateFillerPoints(Vector3.zero, main, fillerAmount);
        for (int i = 0; i < temp.Length; i++)
        {
            points.Add(temp[i]);
        }
        points.Add(main);
        Vector3[] line = points.ToArray();
        points.Clear();
        points.Add(Vector3.zero);
        //rotate and duplicate the points
        for (int j = 1; j < line.Length; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                points.Add(RotationMatrixXY(line[j], Mathf.Deg2Rad * (angleOffset + (i * 90))));
            }
        }

        return points.ToArray();
    }

    public static Vector3[] CheckMark(float radius, float radius2, float angleOffset = 0f, int fillerAmount = 0)
    {
        Vector3[] keyPoints = new Vector3[3];
        keyPoints[0] = RotationMatrixXY(Vector3.up * radius2, Mathf.Deg2Rad * (angleOffset + 45));
        keyPoints[1] = Vector3.zero;
        keyPoints[2] = RotationMatrixXY(Vector3.up * radius, Mathf.Deg2Rad * (angleOffset - 45));

        return Filler(keyPoints, fillerAmount, false);
    }



    //UTILS
    //**************
    public static Vector3[] PrimaryPoints(int numberOfPoints, float radius, float angleOffset)
    {
        radius = radius <= 0 ? 0.1f : radius;
        float radians = Mathf.Deg2Rad * (360f / numberOfPoints);
        List<Vector3> keyPoints = new List<Vector3>();
        for (int i = 0; i < numberOfPoints; i++)
        {
            keyPoints.Add(RotationMatrixXY(Vector3.right * radius, (i * radians) + Mathf.Deg2Rad * angleOffset));
        }

        return keyPoints.ToArray();
    }
    public static Vector3 RotationMatrixXY(Vector3 point, float radians)
    {
        return new Vector3(point.x * Mathf.Cos(radians) - point.y * Mathf.Sin(radians),
                           point.x * Mathf.Sin(radians) + point.y * Mathf.Cos(radians),
                           0f);
    }
    public static Vector3[] CalculateFillerPoints(Vector3 start, Vector3 end, int amount)
    {        
        Vector3[] temp = new Vector3[amount];
        for (int i = 0; i < amount; i++)
        {
            temp[i] = Vector3.Lerp(start, end, (float)(i + 1f) / (amount + 1f));
        }

        return temp;
    }

    //return list of points including both keyPoints and calculated filler points
    public static Vector3[] Filler(Vector3[] keyPoints, int fillerAmount, bool loop = true)
    {
        //check and also don't run if not needed
        if(fillerAmount <= 0) { return keyPoints; }

        List<Vector3> points = new List<Vector3>();
        //add all filler points
        for (int i = 0; i < keyPoints.Length - 1; i++)
        {
            points.Add(keyPoints[i]);
            Vector3[] temp = CalculateFillerPoints(keyPoints[i], keyPoints[i + 1], fillerAmount);
            for (int j = 0; j < temp.Length; j++)
            {
                points.Add(temp[j]);
            }
        }
        
        //add last point
        points.Add(keyPoints[keyPoints.Length - 1]);

        if (loop)
        {
            //connect last to first            
            Vector3[] other = CalculateFillerPoints(keyPoints[keyPoints.Length - 1], keyPoints[0], fillerAmount);
            for (int j = 0; j < other.Length; j++)
            {
                points.Add(other[j]);
            }
        }        

        return points.ToArray();
    }
}
