﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public class Enemy_Chase_Wobble_Surface : AEnemy
    {
        //debug for now
        [SerializeField] Vector3 wobbleDirection = Vector3.right;
        [SerializeField] float wobbleSpeed = 1f;
        [SerializeField] float wobbleIntensity = 1f;
        private float offset;

        public override void SetMovement()
        {
            offset = Random.Range(-1f, 1f);
            Movement = () =>
            {
                Vector3 d = SurfaceMovement.Direction.Forward(transform, speedThrust);
                Vector3 d2 = SurfaceMovement.Direction.Wobble(transform, offset, new WobbleInfo(Vector3.right, wobbleSpeed, wobbleIntensity));
                return d + d2;
            };
        }

        public override void SetRotation()
        {
            Rotation = () => SurfaceMovement.Rotation.FaceTarget(transform, target, hit);
        }
    }
}

