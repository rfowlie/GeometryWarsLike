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
    }
}
