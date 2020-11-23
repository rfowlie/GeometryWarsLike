using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Hunt : AEnemy
    {
        protected override void SetMovement()
        {
            Movement = () => EMovement.Direction.Forward(transform, speedThrust);
        }

        protected override void SetRotation()
        {
            Rotation = () => EMovement.Rotation.AnticipatePosition(transform, target, intensity, hit);
        }

        public float huntRange = 5f;
        public float intensity = 0;

        private void Update()
        {
            intensity = (target.position - transform.position).magnitude / 3f;

            ////check distance to player, change from anticipate to normal chase
            //if((target.position - transform.position).magnitude < huntRange)
            //{
            //    Rotation = () => EMovement.Rotation.FaceTarget(transform, target, hit);
            //}
            //else if((target.position - transform.position).magnitude > huntRange + 2f)
            //{
            //    Rotation = () => EMovement.Rotation.AnticipatePosition(transform, target, 4f, hit);
            //}
        }
    }
}