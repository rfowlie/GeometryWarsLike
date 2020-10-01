using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//JUST DEBUG RIGHT NOW
public class AChaseTiled : MonoBehaviour
{
    private void OnValidate()
    {
        if (pathLengthMax < -1)
            pathLengthMax = -1;
    }

    [SerializeField] private GridOLD grid;
    [SerializeField] private Transform actor;
    [SerializeField] private Transform target;
    [Tooltip("-1 means show all path nodes")]
    public int pathLengthMax = -1;

    [SerializeField] Color gizmosColour;
    [Range(0.1f, 1f)]
    [SerializeField] float gizmoSize = 0.8f;

    private void OnDrawGizmos()
    {
        if(grid != null && actor != null && target != null)
        {            
            if(grid.CalcPointInGrid(actor.position) && grid.CalcPointInGrid(target.position))
            {
                Debug.DrawLine(actor.position, target.position);
                Queue<Vector3Int> path = Pathfinding.BresenhamLineAlgo(
                                                        grid.TileFromPoint(actor.position), 
                                                        grid.TileFromPoint(target.position),
                                                        pathLengthMax);

                
                Gizmos.color = gizmosColour;
                Vector3 nSize = grid.nodeDimensions * gizmoSize;
                while(path.Count > 0)
                {
                    Gizmos.DrawCube(grid.CalcPointFromGrid(path.Dequeue()), nSize);
                }
            }
        }
    }
}
