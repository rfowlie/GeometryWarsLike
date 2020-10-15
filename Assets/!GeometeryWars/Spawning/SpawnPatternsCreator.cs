using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//visualize a set of points
[RequireComponent(typeof(SpawnPatternsDebugger))]
public class SpawnPatternsCreator : MonoBehaviour
{
    public SpawnPatternsDebugger debugger;
    

    public Transform map;
    public int amountOfPoints = 4;
    public float radius = 1f;
    [Range(0f, 360f)] public float angleOffset = 0f;
    public Color gizmoColour = Color.white;

    private Vector3[] points;
    private List<Vector3> p = new List<Vector3>();
    public Vector3[] GetPoints()
    {
        return p.ToArray();
    }

    private void OnValidate()
    {
        if(spawnShape != currentSpawnShape)
        {
            currentSpawnShape = spawnShape;
            Configure();
        }

        if (amountOfPoints <= 0) { amountOfPoints = 1; }
    }


    private delegate Vector3[] Del();
    private Del del = null;
    public enum SpawnShape { LINE, CIRCLE, SQUARE, TRIANGLE, STAR }
    public SpawnShape spawnShape = SpawnShape.LINE;
    private SpawnShape currentSpawnShape = SpawnShape.LINE;
    private void Configure()
    {
        del = null;

        switch (spawnShape)
        {
            case SpawnShape.LINE:
                del += () => ShapeCreator.Line(amountOfPoints, Quaternion.AngleAxis(angleOffset, transform.up) * transform.forward, radius);
                break;
            case SpawnShape.CIRCLE:
                del += () => ShapeCreator.Circle(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
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
        transform.rotation = Quaternion.FromToRotation(transform.up, transform.position - map.position) * transform.rotation;
        points = del();
        p.Clear();

        //determine if points are on map, add to list
        RaycastHit hit;
        for (int i = 0; i < points.Length; i++)
        {
            points[i] += transform.position;
            Vector3 dir = (map.position - points[i]).normalized;
            //Debug.Log("DrawLines");
            Debug.DrawLine(points[i], points[i] + dir, Color.yellow);
            if (Physics.Raycast(points[i], dir, out hit, float.PositiveInfinity))
            {
                Debug.DrawLine(hit.point, hit.point + hit.normal, Color.red);
                p.Add(hit.point + hit.normal);
            }
        }               
    }

    private void OnDrawGizmos()
    {
        if(del == null) { Configure(); }
        if(map != null)
        {
            Debug.DrawLine(transform.position, map.position, Color.cyan);

            Execute();
            Gizmos.color = gizmoColour;
            for (int i = 0; i < p.Count; i++)
            {
                Gizmos.DrawSphere(p[i], 0.5f);
            }
        }        
    }
}