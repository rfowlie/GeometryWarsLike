using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGridOnShape : MonoBehaviour
{
    public Transform shape;
    public int numberOfPoints = 10;
    public Vector3[] points;

    private Vector3[] CreateGrid(int numberOfPoints)
    {
        Debug.Log("Create Grid");
        List<Vector3> p = new List<Vector3>();
        RaycastHit hit;

        Vector3 dir;
        for (int i = 0; i < numberOfPoints; i++)
        {
            Quaternion rotHorizontal = Quaternion.AngleAxis((360f/ numberOfPoints) * i, shape.up);
            dir = rotHorizontal * shape.forward;            

            for (int j = 0; j < numberOfPoints; j++)
            {
                Quaternion rotVertical = Quaternion.AngleAxis((360f / numberOfPoints) * i, shape.forward);
                dir = rotVertical * dir;
                //Debug.DrawRay(shape.position, dir * 10f, Color.green, 1f);

                Vector3 start = dir * 10f + shape.position;
                if(Physics.Raycast(start, -dir, out hit, 10f))
                {
                    p.Add(hit.point + hit.normal);
                }
            }
        }

        //for (int i = 0; i < numberOfPoints; i++)
        //{
        //    //get updown rotation
        //    Quaternion rotUp = Quaternion.AngleAxis((360f / numberOfPoints) * i, shape.right);
            
        //    for (int j = 0; j < numberOfPoints; j++)
        //    {
        //        //get left right rotation
        //        Quaternion rot = Quaternion.AngleAxis((360f / numberOfPoints) * i, shape.up);
        //        rot *= rotUp;

        //        //determine raycast start and dir
        //        Vector3 dir = rot * shape.forward;
        //        Debug.DrawRay(shape.position, dir * 25f, Color.green, 2f);

        //        //Vector3 start = rot * dir * 20f + shape.position;
        //        //Debug.DrawRay(start, dir * 20f, Color.black, 3f);
        //        //if (Physics.Raycast(start, rot * dir, out hit, 100f))
        //        //{
        //        //    p.Add(hit.point + hit.normal);
        //        //}
        //        //else
        //        //{
        //        //    Debug.Log("No Hit");
        //        //}
        //    }
        //}

        return p.ToArray();
    }


    private void OnValidate()
    {
        if(numberOfPoints < 0)
        {
            numberOfPoints = 1;
        }

        points = CreateGrid(numberOfPoints);
    }

    //DEBUG
    public Color gizmoColour = Color.white;
    public float gizmoRadius = 0.5f;
    private void OnDrawGizmos()
    {
        if(points == null)
        {
            points = CreateGrid(numberOfPoints);
        }
        if(points != null)
        {
            Gizmos.color = gizmoColour;
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawSphere(points[i], gizmoRadius);
            }
        }
    }
}
