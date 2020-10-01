using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Common raycast operations
public class RaycastEX
{
    //returns true/false hit
    //simple raycast from target in direction by distance colliding with everthing opposite to layermask
    public static bool CollisionCheck(Transform target, Vector3 direction, float distance, LayerMask ignoreLayer)
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, direction, out hit, distance, ~ignoreLayer))
        {
            return false;
        }

        return true;
    }

    
    //returns raycast hit from a generic raycast in direction by distance from target
    public static RaycastHit RaycastCheck(Transform target, Vector3 direction, float distance, LayerMask ignoreLayer)
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, direction, out hit, distance, ~ignoreLayer))
        {
            return hit;
        }

        return hit;
    }

    //same as above without layermask
    public static RaycastHit RaycastCheck(Transform target, Vector3 direction, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(target.position, direction, out hit, distance))
        {
            return hit;
        }

        return hit;
    }

    //returns a vector3 representing the minimum distance from the collision based on the check info
    //public static Vector3 RaycastInfoCheck(Vector3 position, RaycastInfo_SO check)
    //{
    //    if (Physics.Raycast(position + check.rayStart, check.rayDirection, out check.hit,
    //                        check.rayDistance, ~check.ignoreLayer, QueryTriggerInteraction.Ignore))
    //    {
    //        //return the distance from the collision point plus min distance
    //        return check.hit.point - (check.rayDirection * check.minDistance);
    //    }
    //    else
    //    {
    //        return position + (check.rayDirection * check.rayDistance);
    //    }
    //}
    //public static Vector3 SpherecastInfoCheck(Vector3 position, float radius, RaycastInfo_SO check)
    //{
    //    if (Physics.SphereCast(position + check.rayStart, radius, check.rayDirection, out check.hit,
    //                        check.rayDistance, ~check.ignoreLayer, QueryTriggerInteraction.Ignore))
    //    {
    //        //return the distance from the collision point plus min distance
    //        return check.hit.point - (check.rayDirection * check.minDistance);
    //    }
    //    else
    //    {
    //        return position + (check.rayDirection * check.rayDistance);
    //    }
    //}















    //OLD
    //generic raycast method that gets are hit information
    private static RaycastHit[] Search(GameObject obj, int numbRays, float viewRange, float viewDis)
    {
        //list to store hits
        List<RaycastHit> hits = new List<RaycastHit>();

        RaycastHit hit;
        float deg = viewRange / numbRays;
        float start = -(viewRange / 2f);

        //shoot rays
        for (int i = 0; i <= numbRays; i++)
        {
            //get rotated vector
            Vector3 dir = LinearRotation.AroundY(obj.transform.forward * viewDis, start + deg * i);

            //draw lines            
            Debug.DrawRay(obj.transform.position, dir, Color.green, 5f);
            //Debug.Log("Dir: " + dir);

            //raycast
            if (Physics.Raycast(obj.transform.position, dir, out hit, viewDis))
            {
                hits.Add(hit);
            }
        }

        //return array for space purposes
        return hits.ToArray();
    }

    //returns the PRIVATE search results
    public static RaycastHit[] AllCollisions(GameObject obj, int numbRays, float viewRange, float viewDis)
    {
        return Search(obj, numbRays, viewRange, viewDis);
    }


    //allow things to raycast search for specific gameObjects
    public static bool FindObject(GameObject obj, int numbRays, float viewRange, float viewDis, string objTag)
    {
        RaycastHit[] hits = Search(obj, numbRays, viewRange, viewDis);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag == objTag)
            {
                return true;
            }
        }

        return false;
    }

    //get the gameobject of the nearest type of object
    public static GameObject GetNearestObj(GameObject obj, int numbRays, float viewRange, float viewDis, LayerMask layer)
    {
        RaycastHit[] hits = Search(obj, numbRays, viewRange, viewDis);

        //extract only objects with correct tag/layer
        List<Transform> correctObj = new List<Transform>();
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer == layer)
            {
                correctObj.Add(hits[i].transform);
            }
        }

        //if nothing with correct tag/layer found return null
        if (correctObj.Count == 0)
        {
            return null;
        }

        Transform[] cO = correctObj.ToArray();


        //get closest collision
        int cc = -1;
        float closestCollision = viewDis;
        Vector3 startPos = obj.transform.position;

        for (int i = 0; i < hits.Length; i++)
        {
            if ((cO[i].position - startPos).magnitude < closestCollision)
            {
                closestCollision = (cO[i].position - startPos).magnitude;

                //store the i position of the obj
                cc = i;
            }
        }

        //couldn't find it
        return cO[cc].gameObject;
    }

    //search for the nearest collision to object within specified range
    //return gameobjects information
    public static GameObject ClosestCollision(GameObject obj, int numbRays, float viewRange, float viewDis)
    {
        RaycastHit[] hits = Search(obj, numbRays, viewRange, viewDis);

        int cc = -1;
        float closestCollision = viewDis;
        Vector3 startPos = obj.transform.position;

        for (int i = 0; i < hits.Length; i++)
        {
            if ((hits[i].point - startPos).magnitude < closestCollision)
            {
                closestCollision = (hits[i].point - startPos).magnitude;

                //store the i position of the obj
                cc = i;
            }
        }

        return hits[cc].collider.gameObject;
    }
}
