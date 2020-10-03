using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Chase_Surface : AEnemy
    {
        [SerializeField] private Transform target;

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

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("TriggerEnterWorks");
            //gameObject.SetActive(false);
        }
    }
}

