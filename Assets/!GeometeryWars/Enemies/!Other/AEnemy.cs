using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeometeryWars
{
    public abstract class AEnemy : Poolable
    {
        //the map layer so raycasting for movement is correct
        public LayerMask map;
        //add slight delay to unit being active
        public bool isActive = false;
        public float timeWait = 1f;

        public float speedThrust = 1f;
        protected Vector3 velocity = Vector3.zero;

        //for now a way to prevent enemies from moving right away
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

        protected virtual void OnTriggerEnter(Collider other)
        {
            ReturnToPool(gameObject);
        }

        [Tooltip("The amount of points recieved for destroying this enemy")]
        public int value = 100;

        //notify that this has been removed from play
        public static event Action<int> DEATH;        
    }
}
