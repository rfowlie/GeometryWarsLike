﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace PatternCreator
{
    public enum SpawnShape { LINE, CIRCLE, SQUARE, TRIANGLE, STAR }

    //visualize a set of points
    //[RequireComponent(typeof(Drawer))]
    [System.Serializable]
    public class Creator : MonoBehaviour
    {
        public void ResetValues()
        {
            //set creator to center of map
            transform.position = map.transform.position;

            amountOfPoints = 1;
            radius = 10;
            angleOffset = 0;
            gizmoColour = Color.white;
            spawnShape = SpawnShape.LINE;
            Configure();
        }

        public Transform map;
        [Space]
        public bool isVisible = true;
        [Space]
        public SpawnShape spawnShape = SpawnShape.LINE;
        public Color gizmoColour = Color.white;
        private SpawnShape currentShape = SpawnShape.LINE;
        public int amountOfPoints = 4;
        public float radius = 1f;
        [Range(0f, 360f)] public float angleOffset = 0f;
        [Range(0f, 100f)] public float percentage = 100f;
        //used to change raycast calc
        public bool towardsCenter = false;

        private Vector3[] shapePoints;
        //final values
        private Vector3[] points;
        private Vector3[] normals;

        private int length = 0;

        public Vector3[] CreatePoints()
        {
            List<Vector3> temp = new List<Vector3>();            
            for (int i = 0; i < length; i++)
            {
                if(points[i] != Vector3.zero) { temp.Add(points[i]); }
            }

            return temp.ToArray();
        }

        private void OnValidate()
        {
            if (spawnShape != currentShape)
            {
                currentShape = spawnShape;
                Configure();
            }

            if (amountOfPoints <= 0) { amountOfPoints = 1; }
        }


        private delegate Vector3[] Del();
        private Del CalculateStartingPoints = null;
        
        
        private void Configure()
        {
            CalculateStartingPoints = null;

            switch (spawnShape)
            {
                case SpawnShape.LINE:
                    CalculateStartingPoints += () => Shapes.Line(amountOfPoints, Quaternion.AngleAxis(angleOffset, transform.up) * transform.forward, radius);
                    break;
                case SpawnShape.CIRCLE:
                    CalculateStartingPoints += () => Shapes.Circle(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                    break;
                case SpawnShape.SQUARE:
                    CalculateStartingPoints += () => Shapes.Square(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                    break;
                case SpawnShape.TRIANGLE:
                    CalculateStartingPoints += () => Shapes.Triangle(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                    break;
                default:
                    Debug.LogError("DebugShape Doesn't Exist!!");
                    break;
            }

            Execute();
        }

        private void Execute()
        {
            //no map no calc
            if(map == null) { return; }
            transform.rotation = Quaternion.FromToRotation(transform.up, transform.position - map.position) * transform.rotation;
            shapePoints = CalculateStartingPoints();
            points = new Vector3[shapePoints.Length];
            normals = new Vector3[shapePoints.Length];

            //determine if points are on map, add to list
            RaycastHit hit;
            float distanceToMap = (map.position - transform.position).magnitude;
            for (int i = 0; i < shapePoints.Length; i++)
            {
                shapePoints[i] += transform.position;
                //calculate raycast direction, depends on which bool is selected
                Vector3 dir = towardsCenter ? (map.position - shapePoints[i]).normalized : -transform.up;
                Debug.DrawLine(shapePoints[i], shapePoints[i] + dir, Color.yellow);
                if (Physics.Raycast(shapePoints[i], dir, out hit, distanceToMap))
                {
                    points[i] = hit.point;
                    normals[i] = hit.normal;
                }
            }
        }

        //*****************************************
        [HideInInspector] public bool isOn = true;
        [HideInInspector] [SerializeField] public List<bool> toggles = new List<bool>();
        [HideInInspector] [SerializeField] public List<string> patternNames = new List<string>();
        [HideInInspector] [SerializeField] public List<Color> colors = new List<Color>();
        [HideInInspector] [SerializeField] public List<Vector3[]> patterns = new List<Vector3[]>();
        [HideInInspector] [SerializeField] public List<PatternInfo> info = new List<PatternInfo>();

        
        public PatternInfo CreateInfo()
        {
            PatternInfo temp = new PatternInfo();
            //set to relative position from map
            temp.relativePosition = map.InverseTransformDirection(transform.position);
            temp.rotation = transform.rotation;
            temp.amountOfPoints = amountOfPoints;
            temp.radius = radius;
            temp.angleOffset = angleOffset;
            temp.percentage = percentage;
            temp.shape = currentShape;
            temp.towardsCenter = towardsCenter;

            return temp;
        }

        public void SetInfo(PatternInfo info)
        {
            transform.position = info.relativePosition;
            transform.rotation = info.rotation;

            amountOfPoints = info.amountOfPoints;
            radius = info.radius;
            angleOffset = info.angleOffset;
            spawnShape = info.shape;

            Configure();
        }

        //return all the info for each pattern selecte
        public PatternInfo[] ReturnInfo()
        {
            List<PatternInfo> list = new List<PatternInfo>();
            for (int i = 0; i < patterns.Count; i++)
            {
                //only get points from lists that have toggles 
                if (toggles[i])
                {
                    list.Add(info[i]);
                }
            }

            return list.ToArray();
        }

        //convert all arrays in points list to one giant array
        public Vector3[] GetPoints()
        {
            List<Vector3> t = new List<Vector3>();
            for (int i = 0; i < patterns.Count; i++)
            {
                //only get points from lists that have toggles 
                if (toggles[i])
                {
                    for (int j = 0; j < patterns[i].Length; j++)
                    {
                        t.Add(patterns[i][j]);
                    }
                }                
            }

            return t.ToArray();
        }


        //Debug Draw
        private void OnDrawGizmos()
        {
            //draw current pattern
            if (CalculateStartingPoints == null) { Configure(); }
            if (map != null && isVisible)
            {
                Debug.DrawLine(transform.position, map.position, Color.cyan);
                Execute();

                Gizmos.color = gizmoColour;
                length = Mathf.RoundToInt(points.Length * (percentage * 0.01f));
                for (int i = 0; i < length; i++)
                {
                    if(points[i] != Vector3.zero)
                    {
                        Gizmos.color = gizmoColour;
                        Gizmos.DrawSphere(points[i], 0.5f);
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(points[i], points[i] + normals[i]);
                    }
                }
            }

            //draw stored patterns
            if (patterns.Count > 0 && isOn)
            {                
                for (int i = 0; i < patterns.Count; i++)
                {                    
                    //skip if not toggled
                    if(toggles[i])
                    {
                        //set colour for pattern
                        Gizmos.color = colors[i];

                        //draw points in pattern
                        for (int j = 0; j < patterns[i].Length; j++)
                        {
                            Gizmos.DrawSphere(patterns[i][j], 0.5f);
                        }
                    }
                }
            }
        }
    }
}
