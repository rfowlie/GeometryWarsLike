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

        [Tooltip("The amount of points recieved for destroying this enemy")]
        public int value = 100;

        //notify listeners that this was destroyed
        public static event Action<int> SHOT;


        //GAMELOOP
        protected virtual void Start()
        {
            GlobalVariables gv = FindObjectOfType<GlobalVariables>();
            mapLayer = gv.mapLayer;
            obstacleLayer = gv.obstacleLayer;

            SetMovement();
            SetRotation();
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
            CoroutineEX.Delay(this, () => isActive = true, timeWait);
        }

        private void OnDisable()
        {
            ChildTrigger[] ct = GetComponentsInChildren<ChildTrigger>();
            foreach (ChildTrigger c in ct)
            {                
                c.TRIGGER -= ReturnSelf;
            }
        }

        protected RaycastHit hit;
        protected Func<Vector3> Movement;
        protected Func<Quaternion> Rotation;

        protected virtual void FixedUpdate()
        {
            if (isActive)
            {
                //Calc next pos and then check for obstacle collision
                RaycastHit hitNext;
                //fancy null check, because returns a value type use the ?? coalescing operator to tell what value to return on null
                Vector3 nextPos = transform.position + Movement?.Invoke() ?? Vector3.zero;
                //Vector3 nextPos = transform.position + currentMovement.RayDirection();
                if (Physics.SphereCast(transform.position, 1f, transform.forward, out hitNext, obstacleDistance, obstacleLayer))
                {
                    //obstacle hit...                    
                    Vector3 p = Vector3.Project(transform.position - hitNext.point, hitNext.normal);
                    p = ((hitNext.point - transform.position) + p).normalized;
                    //Debug.DrawRay(transform.position, p * 3f, Color.black, 1f);
                    nextPos = transform.position + p * speedThrust * Time.fixedDeltaTime;
                }


                //RaycastHit hit;
                if (Physics.Raycast(nextPos, -transform.up, out hit, float.PositiveInfinity, mapLayer))
                {
                    //move
                    transform.position = hit.point + hit.normal;
                }

                //rotation
                //same as above here, quaterion is a value type so need the null coalescing operator to convert
                transform.rotation = Rotation?.Invoke() ?? transform.rotation;
                //transform.rotation = currentMovement.NextRotation(hit);
            }
        }
        

        protected virtual void OnTriggerEnter(Collider other)
        {
            //increase player score
            if(other.gameObject.tag == "Bullet")
            {
                SHOT(value);
            }

            ReturnSelf();
        }

        private void ReturnSelf()
        {
            ReturnToPool(gameObject);
        }


        //METHODS
        protected abstract void SetMovement();
        protected abstract void SetRotation();
    }
}
