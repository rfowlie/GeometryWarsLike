using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    //needed for wobble direction method
    public struct WobbleInfo
    {
        public WobbleInfo(Vector3 direction, float speed = 1f, float intensity = 1f)
        {
            this.direction = direction;
            this.speed = speed;
            this.intensity = intensity;
        }

        public Vector3 direction;
        public float speed;
        public float intensity;
    }


    //static methods that hold enemy movement methods
    public static class SurfaceMovement
    {
        //directions
        public static class Direction
        {
            public static Vector3 Forward(Transform t, float speed)
            {
                return t.forward * speed * Time.deltaTime;
            }
            //using sin combine random directions and intensities to create a unique wobble
            public static Vector3 Wobble(Transform transform, float offset, params WobbleInfo[] wobbles)
            {
                Vector3 total = Vector3.zero;
                offset += Time.time;
                for(int i = 0; i < wobbles.Length; i++)
                {
                    total += transform.TransformDirection(wobbles[i].direction) * Mathf.Sin(offset * wobbles[i].speed) * wobbles[i].intensity;
                }

                return total * Time.deltaTime;
            }
        }
        //rotations
        public static class Rotation
        {
            public static Quaternion Forward(Transform t, RaycastHit hit)
            {
                return Quaternion.FromToRotation(t.up, hit.normal) * t.rotation;
            }
            public static Quaternion SlightRotation(Transform t, RaycastHit hit, float offset)
            {
                //apply slight rotation first and then return rotation 
                t.Rotate(new Vector3(0f, offset, 0f));
                return Quaternion.FromToRotation(t.up, hit.normal) * t.rotation;
            }
            public static Quaternion FaceTarget(Transform transform, Transform target, RaycastHit hit)
            {
                Vector3 selfToTarget = (target.position - transform.position).normalized;
                transform.rotation = Quaternion.FromToRotation(transform.forward, selfToTarget) * transform.rotation;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                return transform.rotation;
            }
            public static Quaternion AnticipatePosition(Transform transform, Transform target, float intensity, RaycastHit hit)
            {
                Vector3 velocity = target.GetComponent<PlayerManager>().velocity * intensity;
                Vector3 selfToTarget = ((target.position + velocity) - transform.position).normalized;
                transform.rotation = Quaternion.FromToRotation(transform.forward, selfToTarget) * transform.rotation;
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                return transform.rotation;
            }
        }
    }
}
