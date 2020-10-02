using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//visualize a set of points
public class DebugPoints : MonoBehaviour
{
    public Transform map;
    public int amountOfPoints = 4;
    public float radius = 1f;
    public float angleOffset = 0f;
    public Color gizmoColour = Color.white;

    private Vector3[] points;
    List<Vector3> p = new List<Vector3>();

    private void OnValidate()
    {
        if(amountOfPoints <= 0) { amountOfPoints = 1; }
        Configure();
    }

    private delegate Vector3[] Del();
    private Del del = null;
    public enum SpawnShape { LINE, CIRCLE, SQUARE, TRIANGLE, STAR }
    public SpawnShape spawnShape = SpawnShape.LINE;
    private void Configure()
    {
        del = null;

        switch (spawnShape)
        {
            case SpawnShape.CIRCLE:
                del += () => ShapeCreator.Circle(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                break;
            case SpawnShape.LINE:
                del += () => ShapeCreator.Line(amountOfPoints, transform.right, radius);
                break;
            case SpawnShape.SQUARE:
                del += () => ShapeCreator.Square(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                break;
            case SpawnShape.TRIANGLE:
                del += () => ShapeCreator.Triangle(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                break;
            default:
                Debug.LogError("DebugShape Doesn't Exist!!");
                break;
        }
    }

    
    private void Execute()
    {
        Debug.DrawLine(transform.position, map.position, Color.cyan);
        transform.rotation = Quaternion.FromToRotation(transform.up, transform.position - map.position) * transform.rotation;
        points = del();
        p.Clear();

        //determine if points are on map, add to list
        RaycastHit hit;
        for (int i = 0; i < points.Length; i++)
        {
            points[i] += transform.position;
            if (Physics.Raycast(points[i], -transform.up, out hit, float.PositiveInfinity))
            {
                p.Add(hit.point + hit.normal);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Execute();
        Gizmos.color = gizmoColour;
        for (int i = 0; i < p.Count; i++)
        {
            Gizmos.DrawSphere(p[i], 0.5f);
        }
    }
}
