using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PatternCreator
{
    public class Adjustor : MonoBehaviour
    {
        public Transform map;
        public SO_PatternInfoContainer container;
        public PatternInfo[] info;
        [HideInInspector] public Vector3[][] points;

        
        private void DisplayInfo()
        {
            if(container == null) { info = null; }
            if(container != null) { info = container.values; }
        }

        public void LoadContainer()
        {
            DisplayInfo();

            if (container != null)
            {
                //setup array to hold points
                points = new Vector3[container.GetLength()][];

                int length = container.GetLength();
                for (int i = 0; i < length; i++)
                {
                    //get values
                    PatternInfo p = container.values[i];
                    //place this in correct position
                    transform.position = map.TransformPoint(p.relativePosition);
                    transform.rotation = p.rotation * transform.rotation;

                    //calculate points for each pattern info
                    List<Vector3> list = new List<Vector3>(Shapes.GetShape(p.shape, p.amountOfPoints, p.radius, transform.up, transform.forward, p.angleOffset));
                    int lengthWithPercent = Mathf.RoundToInt(list.Count * (p.percentage * 0.01f));
                    //store proper amount of points based on percentage
                    if(lengthWithPercent > 0)
                    {
                        int remove = list.Count - lengthWithPercent;
                        list.RemoveRange(lengthWithPercent - 1, remove);
                        //raycast onto map for exact points
                        points[i] = RaycastOnMap(list.ToArray());
                    }
                }
            }
        }

        private Vector3[] RaycastOnMap(Vector3[] points)
        {
            RaycastHit hit;
            float distanceToMap = (map.position - transform.position).magnitude;
            for (int i = 0; i < points.Length; i++)
            {
                points[i] += transform.position;
                //calculate raycast direction, depends on which bool is selected
                //Vector3 dir = towardsCenter ? (map.position - shapePoints[i]).normalized : -transform.up;
                Debug.DrawLine(points[i], points[i] + -transform.up, Color.yellow);
                if (Physics.Raycast(points[i], -transform.up, out hit, distanceToMap))
                {
                    points[i] = hit.point;
                    Debug.DrawLine(hit.point, hit.point + hit.normal);
                    //normals[i] = hit.normal;
                }
            }

            return points;
        }

        private void OnDrawGizmos()
        {
            //Draw calculated points
            if(container != null && points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    for (int j = 0; j < points[i].Length; j++)
                    {
                        Gizmos.DrawSphere(points[i][j], 0.5f);
                    }
                }
            }
        }
    }
}