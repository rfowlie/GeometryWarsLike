using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    //enemy that moves in the direction of the target and rotates to face them at all times
    public class Enemy_Chase : AEnemy
    {
        [SerializeField] private Transform target;
        public float speedThrust = 1f;
        public float speedSteer = 45f;

        private Vector3 directionToTarget;
        private Vector3 nextVelocity;
        private float nextRotation;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 directionToTarget = target.position - transform.position;
            nextVelocity = directionToTarget.normalized * speedThrust;
            nextRotation = Mathf.Atan2(-directionToTarget.x, directionToTarget.y) * Mathf.Rad2Deg;
        }

        private void FixedUpdate()
        {
            if(isActive)
            {
                transform.position += nextVelocity * Time.fixedDeltaTime;
                transform.rotation = Quaternion.AngleAxis(nextRotation, Vector3.forward);
            }            
        }
    }
}

