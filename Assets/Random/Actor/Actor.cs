using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private GridOLD grid;
    [SerializeField] private Transform target;
    [SerializeField] private int pathLength = 6;
    private Vector3Int targetGridPosition;
    [SerializeField] public bool isOn = false;

    private Queue<Vector3Int> path = new Queue<Vector3Int>();
    private Vector3 pathCurrent = Vector3.zero;
    private Vector3 pathNext = Vector3.zero;
    private float count = 0f;
    private float distance = 0f;

    //basic setup for pathfinding
    private void PathFind()
    {
        targetGridPosition = grid.TileFromPoint(target.position);

        //calc initial path
        path = Pathfinding.BresenhamLineAlgo(grid.TileFromPoint(transform.position), grid.TileFromPoint(target.position), pathLength);
        if (path.Count > 0)
        {
            pathCurrent = transform.position;
            pathNext = grid.CalcPointFromGrid(path.Dequeue());
            count = 0;
            distance = (pathNext - pathCurrent).magnitude;
            isOn = true;
        }
    }

    private void Start()
    {
        PathFind();         
    }
    private void Update()
    {
        if(grid.TileFromPoint(target.position) != targetGridPosition)
        {
            PathFind();
        }
        else if(isOn)
        {
            count += Time.deltaTime;
            if(count > distance)
            {
                if (path.Count == 0)
                {
                    PathFind();
                }
                else
                {
                    pathCurrent = pathNext;
                    pathNext = grid.CalcPointFromGrid(path.Dequeue());
                    count -= distance;
                    distance = (pathNext - pathCurrent).magnitude;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(isOn)
        {
            transform.position = Vector3.Lerp(pathCurrent, pathNext, count / distance);
        }
    }
}
