using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeometeryWars
{
    public class Enemy_Chase_Jump_Surface : AEnemy
    {
        protected override void SetMovement()
        {
            currentMovement = new Chase(this);
        }

        protected override void Start()
        {
            base.Start();            
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            jumpTime = 0f;
            c = StartCoroutine(RandomRangeDelay(delayMin, delayMax));
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (isJump)
            {
                jumpTime += Time.fixedDeltaTime;
                float jumpPosition = Jump(jumpTime, jumpForce, gravity);
                if (jumpPosition < 0)
                {
                    jumpPosition = 0f;
                    isJump = false;
                    c = null;
                    c = StartCoroutine(RandomRangeDelay(delayMin, delayMax));
                }

                body.localPosition = transform.up * jumpPosition;
            }
        }

        public bool isJump = false;
        public float jumpTime = 0f;
        public float jumpForce = 10f;
        public float gravity = 10f;
        public float delayMin = 1f;
        public float delayMax = 3f;
        Coroutine c = null;

        private IEnumerator RandomRangeDelay(float min, float max)
        {
            yield return new WaitForSecondsRealtime(Random.Range(min, max));
            jumpTime = 0f;
            isJump = true;
        }

        //y = Vit - 1/2gt^2
        public static float Jump(float jumpTime, float jumpForce, float gravity)
        {
            return (jumpForce * jumpTime) - 0.5f * gravity * (jumpTime * jumpTime);
        }
    }
}

