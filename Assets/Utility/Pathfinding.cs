using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//Pathfinding using grid 
public abstract class Pathfinding : MonoBehaviour
{
    //determine most sutable / realistic path given proximity to grid positions, allows diagonals
    public static Queue<Vector3Int> BresenhamLineAlgo(Vector3Int start, Vector3Int end, int pathLength = -1)
    {        
        //find largest step
        int stepX = Mathf.Abs(end.x - start.x);
        int stepY = Mathf.Abs(end.y - start.y);
        int stepZ = Mathf.Abs(end.z - start.z);
        int step;

        if(stepX >= stepY && stepX >= stepZ)
        {
            step = stepX;
        }
        else if(stepY >= stepZ)
        {
            step = stepY;
        }
        else
        {
            step = stepZ;
        }

        //new point value
        int pX = 0;
        int pY = 0;
        int pZ = 0;

        //get point direction when increase to next point
        int pdX = (end.x - start.x) == 0 ? 0 : (end.x - start.x) / Mathf.Abs(end.x - start.x);
        int pdY = (end.y - start.y) == 0 ? 0 : (end.y - start.y) / Mathf.Abs(end.y - start.y);
        int pdZ = (end.z - start.z) == 0 ? 0 : (end.z - start.z) / Mathf.Abs(end.z - start.z);

        //get error distances from largest step
        float dx = (float)stepX / (float)step;
        float dy = (float)stepY / (float)step;
        float dz = (float)stepZ / (float)step;

        //error margins, determine whether to increase to next position
        float eX = 0;
        float eY = 0;
        float eZ = 0;

        //adjust step to account for desired path length
        step = pathLength == -1 ? step : (pathLength - step) < 0 ? step + (pathLength - step) : step;

        Queue<Vector3Int> newPath = new Queue<Vector3Int>();
        for(int i = 1; i < step + 1; i++)
        {            
            eX += dx;
            if(1 - eX < eX)
            {
                pX += pdX;
                eX -= 1;
            }
            eY += dy;
            if(1 - eY < eY)
            {
                pY += pdY;
                eY -= 1;
            }
            eZ += dz;
            if(1 - eZ < eZ)
            {
                pZ += pdZ;
                eZ -= 1;
            }

            //add next point, one of these should increase
            newPath.Enqueue(start + new Vector3Int(pX, pY, pZ));           
        }

        return newPath;
    }


    public struct GridCosts
    {
        public int adjacent;
        public int diagonal;
        public int diagonalLong;
    }

    //get a sense for the generic cost of the path from one point in the grid to another 
    private static float CalcCost(GridCosts costs, Vector3Int start, Vector3Int end)
    {
        int[] arr = { Mathf.Abs(end.x - start.x), Mathf.Abs(end.y - start.y), Mathf.Abs(end.z - start.z) };
        Array.Sort(arr);
        return costs.adjacent * arr[0] + costs.diagonal * (arr[1] - arr[0]) + costs.diagonalLong * (arr[2] - arr[1]);
    }

    public static void AStar(GridCosts costs)
    {

    }
}





