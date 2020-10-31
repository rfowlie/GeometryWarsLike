using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Wander_Surface : AEnemy
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            //face random direction
            transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), transform.up) * transform.rotation;
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                RaycastHit hit;                
                Vector3 nextPos = transform.forward * speedThrust * Time.fixedDeltaTime;
                //Debug.DrawLine(transform.position, transform.position + transform.forward * 2f, Color.cyan);
                if (Physics.Raycast(transform.position + nextPos, -transform.up, out hit, float.PositiveInfinity, map))
                {
                    transform.position = hit.point + hit.normal;
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }
            }
        }
    }
}

