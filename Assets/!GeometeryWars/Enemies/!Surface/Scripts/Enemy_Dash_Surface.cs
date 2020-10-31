using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeometeryWars
{
    //rotates to face player...
    //dashs quickly forward a certain distance/range???
    //then stops for awhile
    //only when player in range??? 
    //measure the arc distance??? 
    //wouldn't be accurate if using odd shapes
    public class Enemy_Dash_Surface : AEnemy
    {
        [SerializeField] private Transform target;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }

            //face random direction
            transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), transform.up) * transform.rotation;
        }

        private void Update()
        {
            //determine if player close enough...
        }

        private void FixedUpdate()
        {
            if (isActive)
            {
                RaycastHit hit;
                //Vector3 nextPos = transform.position + velocity * speedThrust * Time.fixedDeltaTime;
                Vector3 nextPos = transform.forward * speedThrust * Time.fixedDeltaTime;
                //Debug.DrawLine(transform.position, transform.position + transform.forward * 2f, Color.cyan);
                if (Physics.Raycast(transform.position + nextPos, -transform.up, out hit, float.PositiveInfinity, map))
                {
                    transform.position = hit.point + hit.normal;
                    Vector3 selfToTarget = (target.position - transform.position).normalized;
                    transform.rotation = Quaternion.FromToRotation(transform.forward, selfToTarget) * transform.rotation;
                    transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                }
            }
        }
    }
}
