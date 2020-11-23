using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeometeryWars
{
    public abstract class AEnemyJump : AEnemy
    {
        public EnemyMovement defaultMovement;
        public EnemyMovement jumpMovement;
        private bool isJump = false;
        private float jumpTime = 0f;
        public float jumpForce = 10f;
        public float gravity = 10f;
        public float delayMin = 1f;
        public float delayMax = 3f;
        Coroutine c = null;

        private void SetJump()
        {
            jumpTime = 0f;
            isJump = true;
            currentMovement = jumpMovement;
        }
        protected override void Start()
        {
            base.Start();            
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            jumpTime = 0f;
            c = StartCoroutine(CoroutineEX.RandomDelayFunc(SetJump, delayMin, delayMax));
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (isJump)
            {
                jumpTime += Time.fixedDeltaTime;
                float jumpPosition = PhysicsEX.Jump(jumpTime, jumpForce, gravity);
                if (jumpPosition < 0)
                {
                    jumpPosition = 0f;
                    isJump = false;
                    c = null;
                    currentMovement = defaultMovement;
                    c = StartCoroutine(CoroutineEX.RandomDelayFunc(SetJump, delayMin, delayMax));
                }

                body.localPosition = Vector3.up * jumpPosition;
            }
        }
    }
}
