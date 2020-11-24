using System.Collections;
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
            amountOfPoints = 1;
            radius = 10;
            angleOffset = 0;
            gizmoColour = Color.white;
            patternName = string.Empty;
            spawnShape = SpawnShape.LINE;
            Configure();
        }

        public Transform map;
        [Space]
        [Space]
        public SpawnShape spawnShape = SpawnShape.LINE;
        public Color gizmoColour = Color.white;
        private SpawnShape currentSpawnShape = SpawnShape.LINE;
        public int amountOfPoints = 4;
        public float radius = 1f;
        [Range(0f, 360f)] public float angleOffset = 0f;
        [Space]
        public string patternName = string.Empty;

        private Vector3[] points;
        private List<Vector3> p = new List<Vector3>();
        public Vector3[] CreatePoints()
        {
            Vector3[] t = p.ToArray();
            return t;
        }

        private void OnValidate()
        {
            if (spawnShape != currentSpawnShape)
            {
                currentSpawnShape = spawnShape;
                Configure();
            }

            if (amountOfPoints <= 0) { amountOfPoints = 1; }
        }


        private delegate Vector3[] Del();
        private Del del = null;
        
        
        private void Configure()
        {
            del = null;

            switch (spawnShape)
            {
                case SpawnShape.LINE:
                    del += () => Shapes.Line(amountOfPoints, Quaternion.AngleAxis(angleOffset, transform.up) * transform.forward, radius);
                    break;
                case SpawnShape.CIRCLE:
                    del += () => Shapes.Circle(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                    break;
                case SpawnShape.SQUARE:
                    del += () => Shapes.Square(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
                    break;
                case SpawnShape.TRIANGLE:
                    del += () => Shapes.Triangle(amountOfPoints, radius, transform.up, transform.forward, angleOffset);
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

        //*****************************************
        [HideInInspector] public bool isOn = true;
        [HideInInspector] [SerializeField] public List<Vector3[]> patterns = new List<Vector3[]>();
        [HideInInspector] [SerializeField] public List<string> patternNames = new List<string>();
        [HideInInspector] [SerializeField] public List<Color> colors = new List<Color>();
        [HideInInspector] [SerializeField] public List<bool> toggles = new List<bool>();
        [HideInInspector] [SerializeField] public List<PatternInfo> info = new List<PatternInfo>();

        //edit info
        public struct PatternInfo
        {
            public PatternInfo(Vector3 position, Quaternion rotation, int amountOfPoints, float radius, float angleOffset, SpawnShape shape)
            {
                this.position = position;
                this.rotation = rotation;
                this.amountOfPoints = amountOfPoints;
                this.radius = radius;
                this.angleOffset = angleOffset;
                this.shape = shape;
            }

            //creator position
            public Vector3 position;
            public Quaternion rotation;
            public int amountOfPoints;
            public float radius;
            public float angleOffset;
            public SpawnShape shape;
        }

        public PatternInfo CreateInfo()
        {
            return new PatternInfo(transform.position, transform.rotation, amountOfPoints, radius, angleOffset, currentSpawnShape);
        }

        public void SetInfo(PatternInfo pi)
        {
            transform.position = pi.position;
            transform.rotation = pi.rotation;
            
            amountOfPoints = pi.amountOfPoints;
            radius = pi.radius;
            angleOffset = pi.angleOffset;
            spawnShape = pi.shape;

            Configure();
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
            if (del == null) { Configure(); }
            if (map != null)
            {
                Debug.DrawLine(transform.position, map.position, Color.cyan);
                Execute();

                Gizmos.color = gizmoColour;
                for (int i = 0; i < p.Count; i++)
                {
                    Gizmos.DrawSphere(p[i], 0.5f);
                }
            }


            //draw all other patterns
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
