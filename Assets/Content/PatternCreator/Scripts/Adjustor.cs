using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PatternCreator
{
    //used by the custom window store points values and to create new patterns...
    public class Adjustor : MonoBehaviour
    {
        public void Setup(Transform map)
        {
            this.map = map;

            //setup array to hold points
            points = new List<Vector3[]>();
            colours = new List<Color>();
        }
    
        //[SerializeField] private SO_PatternInfoContainer container;
        private Transform map;
        private List<Vector3[]> points;
        [SerializeField] private List<Color> colours;
        [HideInInspector] public int currentSelection = -1;
        public PatternInfo currentInfo;


        private Color RandomColour()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        }
        private void SetPosition(PatternInfo p)
        {
            if(map != null)
            {
                //place this transform into correct position, dictated by pattern
                transform.position = map.TransformDirection(p.relativePosition);
                //transform.rotation = p.rotation * transform.rotation;
            }
        }
        private Vector3[] CalculatePoints(PatternInfo p)
        {
            Vector3 mapXZ = map.transform.position;
            mapXZ.y = transform.position.y;
            transform.rotation = Quaternion.FromToRotation(transform.forward, mapXZ - transform.position) * transform.rotation;
            transform.rotation = Quaternion.FromToRotation(transform.forward, map.position - transform.position) * transform.rotation;
            //store/update the rotation...
            currentInfo.rotation = transform.up;

            //calculate points for each pattern info
            List<Vector3> list = new List<Vector3>(Shapes.GetShape(p.shape, p.fillerPoints, p.radius, p.angleOffset));
            //setup rotation
            Vector3 spawnAxis = Quaternion.AngleAxis(p.angleOffset, map.position - transform.position) * transform.up;
            Quaternion rot = Quaternion.LookRotation(map.position - transform.position, spawnAxis);
            //apply rotation to points
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = rot * list[i];
            }
            //reset rotation of transform
            transform.rotation = Quaternion.identity;
            int numberToHide = Mathf.RoundToInt(list.Count * (1 - (p.viewPercentage * 0.01f)));
            if(numberToHide > 0)
            {
                for (int i = 0; i < numberToHide; i++)
                {
                    list.RemoveAt(list.Count - 1);
                }
            }

            return list.ToArray();
        }
        private Vector3[] RaycastOnMap(Vector3[] points)
        {
            RaycastHit hit;
            float distanceToMap = (map.position - transform.position).magnitude;
            for (int k = 0; k < points.Length; k++)
            {
                points[k] += transform.position;
                Vector3 direction = (map.position - transform.position).normalized;
                Debug.DrawLine(points[k], points[k] + direction, Color.yellow);
                if (Physics.Raycast(points[k], direction, out hit, distanceToMap))
                {
                    points[k] = hit.point;
                    //SHOW NORMAL???
                    //Debug.DrawLine(hit.point, hit.point + hit.normal);
                    //normals[i] = hit.normal;
                }
            }

            return points;
        }
        public void AddPattern(PatternInfo p)
        {
            //create new random colour for this pattern
            colours.Add(RandomColour());
            SetPosition(p);
            points.Add(RaycastOnMap(CalculatePoints(p)));
        }
        public void RemovePattern(int index)
        {
            if (index >= 0 && index < points.Count)
            {
                points.RemoveAt(index);
            }
        }
        public void CalculateAll(SO_PatternInfoContainer container)
        {
            int length = container.GetLength();

            for (int i = 0; i < length; i++)
            {
                AddPattern(container.values[i]);
            }
        }
        public void SetSelection(PatternInfo p, int index)
        {
            currentSelection = index;
            currentInfo = p;
        }

        


        //************************************
        private void OnDrawGizmos()
        {
            //make sure facing map
            if (map != null)
            {
                //update current selected
                if(currentSelection >= 0)
                {
                    //update relative position
                    currentInfo.relativePosition = map.InverseTransformDirection(transform.position);

                    //updating copy of information, which can be tweaked from pointer inspector...
                    points[currentSelection] = RaycastOnMap(CalculatePoints(currentInfo));
                }
            }
            //Draw calculated points
            if (map != null && points != null)
            {
                Debug.DrawLine(transform.position, map.position, Color.cyan);
                for (int i = 0; i < points.Count; i++)
                {
                    Gizmos.color = colours[i];
                    for (int j = 0; j < points[i].Length; j++)
                    {                        
                        Gizmos.DrawSphere(points[i][j], 0.5f);
                    }
                }
            }
        }
    }
}