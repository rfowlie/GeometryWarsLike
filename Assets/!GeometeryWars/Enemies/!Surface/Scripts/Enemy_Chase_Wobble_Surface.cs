using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Chase_Wobble_Surface : AEnemy
    {
        [SerializeField] private Transform target;

        //debug for now
        [SerializeField] Vector3 wobbleDirection = Vector3.right;
        [SerializeField] float speedWobble = 1f;
        [SerializeField] float strengthWobble = 1f;


        protected override void OnEnable()
        {
            base.OnEnable();
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                //get wobble direction
                Vector3 wobble = transform.TransformDirection(wobbleDirection) * Mathf.Sin(Time.time * speedWobble) * ((Mathf.Sin(Time.time) * 0.5f + 0.5f) * strengthWobble);

                RaycastHit hit;            
                //add to current forward direction to get next position 
                Vector3 nextPos = (wobble + transform.forward) * speedThrust * Time.fixedDeltaTime;
                //do raycast and calculate correct position as well as rotation to remain perpendicular to surface
                if (Physics.Raycast(transform.position + nextPos, -transform.up, out hit, float.PositiveInfinity, map))
                {
                    transform.position = hit.point + hit.normal;

                    Vector3 selfToTarget = (target.position - transform.position).normalized;
                    transform.rotation = Quaternion.FromToRotation(transform.forward, selfToTarget) * transform.rotation;
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            ReturnToPool(gameObject);
        }
    }
}

