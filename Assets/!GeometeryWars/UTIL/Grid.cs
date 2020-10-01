using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Grid : MonoBehaviour
    {
        //easy way to ensure specific values for variables without a custom inspector
        private void OnValidate()
        {
            if (bounds.x < 1f)
                bounds.x = 1f;
            if (bounds.y < 1f)
                bounds.y = 1f;
            if (bounds.z < 1f)
                bounds.z = 1f;
            if (dimensions.x < 1)
                dimensions.x = 1;
            if (dimensions.y < 1)
                dimensions.y = 1;
            if (dimensions.z < 1)
                dimensions.z = 1;

            nodeDimensions = new Vector3(
                                     bounds.x / dimensions.x,
                                     bounds.y / dimensions.y,
                                     bounds.z / dimensions.z
                                   );
        }

        //VARIABLES
        #region variables
        [Header("Enable")]
        public bool drawGrid = true;
        public bool drawNodes = false;

        //grid size
        [SerializeField] private Vector3 bounds = Vector3.one;
        //how many rows and cols and height you want, this will determine node size, make int
        [SerializeField] private Vector3Int dimensions = Vector3Int.one;

        public Vector3 nodeDimensions { get; private set; }

        #endregion

        //METHODS
        #region methods
        //check if point is within grid
        public bool CalcPointInGrid(Vector3 point)
        {
            Vector3 b = bounds - transform.position;
            if (Mathf.Abs(point.x) < Mathf.Abs(b.x) &&
                Mathf.Abs(point.y) < Mathf.Abs(b.y) &&
                Mathf.Abs(point.z) < Mathf.Abs(b.z))
            {
                return true;
            }

            return false;
        }

        //find the grid point closest to point passed in
        public Vector3Int TileFromPoint(Vector3 point)
        {
            Vector3 p = point - transform.position;
            Vector3 n = nodeDimensions;
            int x = Mathf.RoundToInt(p.x / n.x);
            int y = Mathf.RoundToInt(p.y / n.y);
            int z = Mathf.RoundToInt(p.z / n.z);

            return new Vector3Int(x, y, z);
        }

        //see if grid position passed in is in grid
        public bool CalcGridInGrid(Vector3Int grid)
        {
            if (grid.x < 0 || grid.x > dimensions.x - 1 ||
               grid.y < 0 || grid.y > dimensions.y - 1 ||
               grid.z < 0 || grid.z > dimensions.z - 1)
            {
                return false;
            }

            return true;
        }

        //determine world position of grid position passed in
        public Vector3 CalcPointFromGrid(Vector3Int grid)
        {
            Vector3 n = nodeDimensions;
            Vector3 point = new Vector3(
                                        grid.x * n.x,
                                        grid.y * n.y,
                                        grid.z * n.z
                                       );

            return transform.position + point;
        }

        //given a grid coordinate, calculate all neighbor coordinates
        private Vector3Int[] CalcNeighborsFromGrid(Vector3Int grid)
        {
            List<Vector3Int> neighbors = new List<Vector3Int>();

            for (int x = -1; x < 2; x++)
            {
                if (grid.x + x < 0 || grid.x + x > bounds.x - 1)
                { continue; }
                for (int y = -1; y < 2; y++)
                {
                    if (grid.y + y < 0 || grid.y + y > bounds.y - 1)
                    { continue; }
                    for (int z = -1; z < 2; z++)
                    {
                        if (grid.z + z < 0 || grid.z + z > bounds.z - 1)
                        { continue; }

                        //skip self
                        if (x == 0 && y == 0 && z == 0)
                        { continue; }
                        neighbors.Add(new Vector3Int(grid.x + x, grid.y + y, grid.z + z));
                    }
                }
            }

            return neighbors.ToArray();
        }

        //get neighbors from world point
        public Vector3Int[] CalcNeighborsFromPoint(Vector3 point)
        {
            return CalcNeighborsFromGrid(TileFromPoint(point));
        }

        //measuring values between 0 and 1 will ensure it doesn't matter what the grid size is
        public Vector3 GetPointFromLengthWidth(float width, float length)
        {
            length = Mathf.Clamp(length, 0.01f, 0.99f);
            width = Mathf.Clamp(width, 0.01f, 0.99f);

            Vector3 temp = new Vector3(bounds.x * width, bounds.y * length, 0f);
            return transform.position + temp;
        }
        public Vector3 GetRandomPointInPartialGrid(Vector2 bottomLeft, Vector2 topRight)
        {
            //ensure that values are between 0 and 1
            bottomLeft.x = Mathf.Clamp(bottomLeft.x, 0.01f, 0.99f);
            bottomLeft.y = Mathf.Clamp(bottomLeft.y, 0.01f, 0.99f);
            topRight.x = Mathf.Clamp(topRight.x, 0.01f, 0.99f);
            topRight.y = Mathf.Clamp(topRight.y, 0.01f, 0.99f);
            Vector3 temp = new Vector3(UnityEngine.Random.Range(bottomLeft.x, topRight.x) * bounds.x,
                                       UnityEngine.Random.Range(bottomLeft.y, topRight.y) * bounds.y,
                                       0f);

            return transform.position + temp;
        }
        public Vector3 GetRandomPointInGrid()
        {
            Vector3 temp = new Vector3(UnityEngine.Random.Range(0, bounds.x),
                                        UnityEngine.Random.Range(0, bounds.y),
                                        0);

            return temp + transform.position;
        }

        public Vector3[] GetCircleOfPoints(Vector2 center, int amount = 1, float radius = 1f, float rotation = 0f)
        {
            center.x = Mathf.Clamp(center.x, 0.01f + radius, 0.99f);
            center.y = Mathf.Clamp(center.y, 0.01f + radius, 0.99f);
            
            Vector3 c = GetPointFromLengthWidth(center.x, center.y);
            float angle = amount == 1 ? 0 : 360f / amount;
            Vector3[] points = new Vector3[amount];
            for (int i = 0; i < amount; i++)
            {
                Quaternion rot = Quaternion.AngleAxis(angle * i + rotation, Vector3.forward);
                Vector3 pos = c + (rot * Vector3.up) * radius;
                //check if point in grid, otherwise set to center
                points[i] = CalcPointInGrid(pos) ? pos : c;                
            }

            return points;
        }

        public Vector3[] GetLineOfPoints(Vector2 start, Vector2 end, int amount)
        {
            start.x = Mathf.Clamp(start.x, 0.01f, 0.99f);
            start.y = Mathf.Clamp(start.y, 0.01f, 0.99f);
            end.x = Mathf.Clamp(start.x, 0.01f, 0.99f);
            end.y = Mathf.Clamp(start.y, 0.01f, 0.99f);
            Vector3 s = GetPointFromLengthWidth(start.x, start.y);
            Vector3 e = GetPointFromLengthWidth(start.x, start.y);
            Vector3 dir = (end - start).normalized;
            float distance = (end - start).magnitude / (float)(amount - 1);

            Vector3[] points = new Vector3[amount];
            for (int i = 0; i < amount; i++)
            {
                points[i] = (distance * i) * dir + s;
            }

            return points;
        }

        #endregion

        //DEBUG
        #region debug
        private void OnDrawGizmos()
        {
            if (drawGrid)
                DrawGrid();
            if (drawNodes)
                DrawNodes();
        }

        //gizmos for debugging
        public Color gridColour = Color.black;
        public Color nodeColourEmpty = Color.white;
        public Color nodeColourFull = Color.red;
        //1 being max size
        [Range(0.1f, 1f)]
        public float nodeSize = 0.5f;

        protected virtual void DrawGrid()
        {
            Gizmos.color = gridColour;
            //get center since measuring from bottom back left
            Vector3 center = transform.position + bounds / 2f;
            Gizmos.DrawWireCube(center, bounds);
        }

        protected virtual void DrawNodes()
        {
            //determine node size
            Vector3 nSize = nodeDimensions;

            Vector3 origin = transform.position + nSize / 2f;
            for (int x = 0; x < dimensions.x; x++)
            {
                for (int y = 0; y < dimensions.y; y++)
                {
                    for (int z = 0; z < dimensions.z; z++)
                    {
                        Vector3 nextPos = origin + (new Vector3(nSize.x * x, nSize.y * y, nSize.z * z));
                        if (Physics.CheckBox(nextPos, nSize / 2f))
                        {
                            Gizmos.color = nodeColourFull;
                        }
                        else
                        {
                            Gizmos.color = nodeColourEmpty;
                        }

                        Gizmos.DrawCube(nextPos, nSize * nodeSize);
                    }
                }
            }
        }

        #endregion
    }
}

