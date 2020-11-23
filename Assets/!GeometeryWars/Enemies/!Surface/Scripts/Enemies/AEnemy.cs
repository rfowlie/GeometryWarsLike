using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    public abstract class AEnemy : Poolable
    {
        //the map layer so raycasting for movement is correct
        public LayerMask mapLayer;
        public Transform body;
        //add slight delay to unit being active
        public bool isActive = false;
        public float timeWait = 1f;
        public float speedThrust = 1f;
        public float obstacleDistance = 3f;
        public LayerMask obstacleLayer;
        protected Vector3 velocity = Vector3.zero;
        public Transform target;
        protected EnemyMovement currentMovement;

        private Coroutine delay = null;

        [Tooltip("The amount of points recieved for destroying this enemy")]
        public int value = 100;

        //EVENTS
        //notify that this has been removed from play
        public static event Action<int> DEATH;

        //GAMELOOP
        protected virtual void Start()
        {
            GlobalVariables gv = FindObjectOfType<GlobalVariables>();
            mapLayer = gv.mapLayer;
            obstacleLayer = gv.obstacleLayer;

            SetMovement();
        }
        
        protected virtual void OnEnable()
        {
            //get target
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }

            //get child triggers
            ChildTrigger[] ct = GetComponentsInChildren<ChildTrigger>();
            foreach(ChildTrigger c in ct)
            {
                //Debug.Log("Child Trigger Assigned");
                c.TRIGGER += ReturnSelf;
            }

            //face random direction
            transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), transform.up) * transform.rotation;
            //start movement delay
            isActive = false;
            delay = StartCoroutine(CoroutineEX.Delay(() => { isActive = true; }, timeWait));
        }

        private void OnDisable()
        {
            ChildTrigger[] ct = GetComponentsInChildren<ChildTrigger>();
            foreach (ChildTrigger c in ct)
            {                
                c.TRIGGER -= ReturnSelf;
            }
        }

        protected virtual void FixedUpdate()
        {
            if (isActive)
            {
                //Calc next pos and then check for obstacle collision
                RaycastHit hitNext;
                Vector3 nextPos = transform.position + currentMovement.RayDirection();
                if (Physics.SphereCast(transform.position, 1f, transform.forward, out hitNext, obstacleDistance, obstacleLayer))
                {
                    //obstacle hit...                    
                    Vector3 p = Vector3.Project(transform.position - hitNext.point, hitNext.normal);
                    p = ((hitNext.point - transform.position) + p).normalized;
                    //Debug.DrawRay(transform.position, p * 3f, Color.black, 1f);
                    nextPos = transform.position + p * speedThrust * Time.fixedDeltaTime;
                }


                RaycastHit hit;
                if (Physics.Raycast(nextPos, -transform.up, out hit, float.PositiveInfinity, mapLayer))
                {
                    //move
                    transform.position = currentMovement.NextPosition(hit);                    
                }

                //rotation
                transform.rotation = currentMovement.NextRotation(hit);
            }
        }
        

        protected virtual void OnTriggerEnter(Collider other)
        {
            ReturnSelf();
        }

        private void ReturnSelf()
        {
            ReturnToPool(gameObject);
        }


        //METHODS
        protected abstract void SetMovement();
    }
}
