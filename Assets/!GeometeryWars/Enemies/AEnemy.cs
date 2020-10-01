using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    public abstract class AEnemy : Poolable
    {
        public bool isActive = false;
        public float timeWait = 1f;

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(timeWait);
            isActive = true;
        }

        protected virtual void OnEnable()
        {
            isActive = false;
            StartCoroutine(Wait());
        }

        [Tooltip("The amount of points recieved for destroying this enemy")]
        public int value = 100;
        //need for points???
        //public static event Action<GameObject> DEATH;
        public static event Action<int> SCORE;

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                //update score
                SCORE(value);
                pool.Return(gameObject);
            }
        }
    }
}
