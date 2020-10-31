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

            //face random direction
            transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), transform.up) * transform.rotation;
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





//MAYBE FOR LATER DOWN THE LINE
//ALLOW FOR UNITS TO SWAP OUT THERE MOVEMENT TYPE
//OR HAVE MULTIPLE MOVEMENT TYPES THAT THEY ALTERNATE BETWEEN

public abstract class MoveType
{
    //each must movement Type must define how this works
    public abstract void Move();
}
public class SurfaceChase : MoveType
{
    public SurfaceChase(Transform transform, Transform target, float speedThrust)
    {
        this.transform = transform;
        this.target = target;
        this.speedThrust = speedThrust;
    }

    //required vars
    Transform transform;
    Transform target;
    float speedThrust;
    LayerMask map;

    //hold calculations
    Vector3 position;
    Quaternion rotation;

    public override void Move()
    {
        RaycastHit hit;
        //Vector3 nextPos = transform.position + velocity * speedThrust * Time.fixedDeltaTime;
        Vector3 nextPos = transform.forward * speedThrust * Time.fixedDeltaTime;
        //Debug.DrawLine(transform.position, transform.position + transform.forward * 2f, Color.cyan);
        if (Physics.Raycast(transform.position + nextPos, -transform.up, out hit, float.PositiveInfinity, map))
        {            
            position = hit.point + hit.normal;

            Vector3 selfToTarget = (target.position - transform.position).normalized;
            rotation = Quaternion.FromToRotation(transform.forward, selfToTarget) * transform.rotation;
            rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }
}

