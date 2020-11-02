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
        //add slight delay to unit being active
        public bool isActive = false;
        public float timeWait = 1f;
        public float speedThrust = 1f;
        public float obstacleDistance = 3f;
        public LayerMask obstacleLayer;
        protected Vector3 velocity = Vector3.zero;
        public Transform target;
        protected EnemyMovement currentMovement;

        [Tooltip("The amount of points recieved for destroying this enemy")]
        public int value = 100;

        //EVENTS
        //notify that this has been removed from play
        public static event Action<int> DEATH;

        //GAMELOOP
        protected void Start()
        {
            GameVariables gv = FindObjectOfType<GameVariables>();
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
            //face random direction
            transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), transform.up) * transform.rotation;
            //start movement delay
            isActive = false;
            StartCoroutine(Wait());
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
            ReturnToPool(gameObject);
        }


        //METHODS
        protected abstract void SetMovement();

        //COROUTINES
        //for now a way to prevent enemies from moving right away
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(timeWait);
            isActive = true;
        }
    }


    //---------------------------------------------------
    public abstract class EnemyMovement
    {
        public EnemyMovement(AEnemy e)
        {
            this.e = e;
        }

        protected AEnemy e;

        public virtual Vector3 RayDirection()
        {
            return e.transform.forward * e.speedThrust * Time.fixedDeltaTime;
        }
        public virtual Vector3 NextPosition(RaycastHit hit)
        {
            return hit.point + hit.normal;
        }
        public virtual Quaternion NextRotation(RaycastHit hit)
        {
            return Quaternion.FromToRotation(e.transform.up, hit.normal) * e.transform.rotation;
        }
    }

    //basic movement, no rotation just move forward
    public class MoveForward : EnemyMovement
    {
        public MoveForward(AEnemy e) : base(e)
        {
        }
    }

    //rotate to face target
    public class RotateToTarget : EnemyMovement
    {
        public RotateToTarget(AEnemy e) : base(e)
        {
        }

        public override Vector3 RayDirection()
        {
            return Vector3.zero;
        }

        public override Quaternion NextRotation(RaycastHit hit)
        {
            Vector3 selfToTarget = (e.target.position - e.transform.position).normalized;
            e.transform.rotation = Quaternion.FromToRotation(e.transform.forward, selfToTarget) * e.transform.rotation;
            e.transform.rotation = Quaternion.FromToRotation(e.transform.up, hit.normal) * e.transform.rotation;
            return e.transform.rotation;
        }
    }

    //rotate to face, move in faced direction
    public class Chase : EnemyMovement
    {
        public Chase(AEnemy e) : base(e)
        {
        }

        public override Quaternion NextRotation(RaycastHit hit)
        {
            Vector3 selfToTarget = (e.target.position - e.transform.position).normalized;
            e.transform.rotation = Quaternion.FromToRotation(e.transform.forward, selfToTarget) * e.transform.rotation;
            e.transform.rotation = Quaternion.FromToRotation(e.transform.up, hit.normal) * e.transform.rotation;
            return e.transform.rotation;
        }
    }

    //chase but with horizontal offset in movement
    public class Wobble : EnemyMovement
    {
        public Wobble(AEnemy e, Vector3 wobbleDirection, float wobbleSpeed, float wobbleStrength) : base(e)
        {
            this.wobbleDirection = wobbleDirection;
            this.wobbleSpeed = wobbleSpeed;
            this.wobbleStrength = wobbleStrength;

            //generate random seed time so not all wobbles look exactly alike
            seedTime = UnityEngine.Random.Range(0f, DateTime.Now.Millisecond);
        }

        private Vector3 wobbleDirection;
        private float wobbleSpeed, wobbleStrength;

        private float seedTime = 0f;

        //incorporate wobble offset
        public override Vector3 RayDirection()
        {
            Vector3 wobble = e.transform.TransformDirection(wobbleDirection) * Mathf.Sin((seedTime + Time.time) * wobbleSpeed) * wobbleStrength;
            return (wobble + e.transform.forward) * e.speedThrust * Time.fixedDeltaTime;
        }

        //rotate to face target
        public override Quaternion NextRotation(RaycastHit hit)
        {
            Vector3 selfToTarget = (e.target.position - e.transform.position).normalized;
            e.transform.rotation = Quaternion.FromToRotation(e.transform.forward, selfToTarget) * e.transform.rotation;
            e.transform.rotation = Quaternion.FromToRotation(e.transform.up, hit.normal) * e.transform.rotation;
            return e.transform.rotation;
        }
    }
}
