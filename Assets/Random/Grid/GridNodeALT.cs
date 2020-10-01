using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//default grid node type
public class GridNodeALT : MonoBehaviour
{
    //constructor
    public GridNodeALT(Vector3 WorldPosition, Vector3Int GridPosition, Vector3 NodeSize, NodeStatus NodeStatus)
    {
        this.WorldPosition = WorldPosition;
        this.GridPosition = GridPosition;
        this.NodeSize = NodeSize;        

        switch(NodeStatus)
        {
            case NodeStatus.CLEAR:
                NodeColour = Color.clear;
                break;
            case NodeStatus.OBSTACLE:
                NodeColour = Color.red;
                break;
            case NodeStatus.PATH:
                NodeColour = Color.green;
                break;
        }
    }

    public Vector3 WorldPosition { get; private set; }
    public Vector3Int GridPosition { get; private set; }


    //Have the nodes draw themselves, this way grid doesn't have to keep track of path or obstacles
    //and making it complicated to draw   
    public Vector3 NodeSize { get; private set; }
    public Color NodeColour { get; private set; }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = NodeColour;
        Gizmos.DrawCube(WorldPosition, NodeSize);
    }
}

public enum NodeStatus
{
    CLEAR,
    OBSTACLE,
    PATH
}
