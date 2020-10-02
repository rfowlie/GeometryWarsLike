using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//calculate points in world space for shapes
public static class ShapeCreator
{
    public static Vector3[] Triangle(int amountOfPoints, float radius, Vector3 rotationAxis, Vector3 spawnAxis, float angleOffset = 0f)
    {
        if (amountOfPoints <= 0) { return new Vector3[0]; }
        List<Vector3> points = new List<Vector3>();
        //create 3 lines and adjust
        Vector3 centroid = Vector3.zero;
        Vector3[] temp;
        temp = Line(amountOfPoints, Vector3.up + Vector3.right, radius);
        points.AddRange(temp);
        centroid += temp[0];
        temp = Line(amountOfPoints, Vector3.down + Vector3.right, radius);
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += (Vector3.up + Vector3.right).normalized * radius;
        }
        points.AddRange(temp);
        centroid += temp[0];
        temp = Line(amountOfPoints, Vector3.left, radius);
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += Vector3.right * radius;
        }
        points.AddRange(temp);
        centroid += temp[0];

        //find centroid
        centroid /= 3f;

        //final adjust to center all points
        temp = points.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += -centroid;
        }

        return temp;
    }

    public static Vector3[] Square(int amountOfPoints, float radius, Vector3 rotationAxis, Vector3 spawnAxis, float angleOffset = 0f)
    {
        if (amountOfPoints <= 0) { return new Vector3[0]; }
        List<Vector3> points = new List<Vector3>();
        //create 4 lines and adjust points
        Vector3[] temp;
        temp = Line(amountOfPoints, Vector3.up, radius);
        points.AddRange(temp);
        temp = Line(amountOfPoints, Vector3.right, radius);
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += Vector3.up * radius;
        }
        points.AddRange(temp);
        temp = Line(amountOfPoints, Vector3.down, radius);
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += (Vector3.up + Vector3.right) * radius;
        }
        points.AddRange(temp);
        temp = Line(amountOfPoints, Vector3.left, radius);
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += Vector3.right * radius;
        }
        points.AddRange(temp);

        //final adjust to center all points
        temp = points.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += (Vector3.left + Vector3.down) * radius / 2f;
        }

        return temp;
    }

    public static Vector3[] Circle(int amountOfPoints, float radius, Vector3 rotationAxis, Vector3 spawnAxis, float angleOffset = 0f)
    {
        if(amountOfPoints <= 0) { return new Vector3[0]; }
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
        if (amountOfPoints <= 0) { return new Vector3[0]; }
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
