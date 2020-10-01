using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//calculate points in world space for shapes
public static class ShapeCreator
{   
    public static Vector3[] Circle(int amountOfPoints, float radius, Vector3 rotationAxis, Vector3 spawnAxis, float angleOffset = 0f)
    {
        Vector3[] points = new Vector3[amountOfPoints];
        float angle = 360f / amountOfPoints;
        for (int i = 0; i < amountOfPoints; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(angle * i + angleOffset, rotationAxis);
            Vector3 pos = (rot * spawnAxis) * radius;
            points[i] = pos;
        }

        return points;
    }

    public static Vector3[] Line(int amountOfPoints, Vector3 direction, float length)
    {
        direction = direction.normalized;
        Vector3[] points = new Vector3[amountOfPoints];
        float spacing = length / amountOfPoints;
        for (int i = 0; i < amountOfPoints; i++)
        {
            points[i] = direction * spacing * i;
        }

        return points;
    }
}
