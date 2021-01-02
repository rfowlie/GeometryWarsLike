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
            //strenght of effect varies from distance to target
            intensity = (target.position - transform.position).magnitude / 3f;
        }
    }
}