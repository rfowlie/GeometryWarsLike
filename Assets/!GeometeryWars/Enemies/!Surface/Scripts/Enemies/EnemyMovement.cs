using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
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
    public class wander : EnemyMovement
    {
        public wander(AEnemy e) : base(e)
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
            //TEST FOR OTHER WOBBLE TYPES
            //Vector3 wobble2 = e.transform.TransformDirection(Vector3.forward) * Mathf.Sin((seedTime + Time.time) * (wobbleSpeed * 2)) * (wobbleStrength * 0.75f);
            //Vector3 total = (wobble + wobble2);
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
