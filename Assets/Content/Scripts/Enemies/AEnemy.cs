using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    public interface IEnemyMovement
    {
        Vector3 Movement();
        Quaternion Rotation();
    }

    public abstract class AEnemy : MonoBehaviour
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
        protected int damage = 10;
        public int GetDamage() { return damage; }

        //notify listeners that this was destroyed
        public static event Action<EnemyInfo> SHOT;
        

        protected virtual void Start()
        {
            mapLayer = GameController.Instance.GetMapLayer();
            obstacleLayer = GameController.Instance.GetObstacleLayer();

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
                c.TRIGGER += () => gameObject.SetActive(false);
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
                c.TRIGGER -= () => gameObject.SetActive(false);
            }
        }

        public RaycastHit hit;
        public Func<Vector3> Movement;
        public Func<Quaternion> Rotation;
                
        public abstract void SetMovement();
        public abstract void SetRotation();        

        public void UpdateMovement()
        {
            Move();
        }

        
        public virtual void Move()
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
            else
            {
                //Debug.Log("Not active!!");
            }
        }
        
        public void CallEvent()
        {
            EnemyInfo e = new EnemyInfo(value, transform.position, transform.up);
            SHOT(e);
            gameObject.SetActive(false);
        }
        protected virtual void OnTriggerEnter(Collider other)
        {            
            ////increase player score
            //if(other.gameObject.tag == "Bullet")
            //{
            //    EnemyInfo e = new EnemyInfo(value, transform.position, transform.up);
            //    SHOT(e);
            //}

            //gameObject.SetActive(false);
        }
    }

    public struct EnemyInfo
    {
        public EnemyInfo(int points, Vector3 position, Vector3 up)
        {
            this.points = points;
            this.position = position;
            this.up = up;
        }
        public readonly int points;
        public readonly Vector3 position;
        public readonly Vector3 up;
    }
}
