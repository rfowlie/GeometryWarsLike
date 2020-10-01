using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : Poolable
{
    public float speed = 5f;

    private void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.fixedDeltaTime;
        //Vector3 next = transform.up * speed * Time.fixedDeltaTime;
        //if(Physics.Raycast(transform.position, next.normalized, next.magnitude))
        //{
        //    pool.Return(gameObject);
        //}
        //else
        //{
        //    transform.position += transform.up * speed * Time.fixedDeltaTime;
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        pool.Return(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        pool.Return(gameObject);
    }
}
