﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flee : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float speedThrust = 1f;
    public float speedSteer = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speedThrust * Time.deltaTime;
        transform.localEulerAngles += Steer.AwayFromTarget(transform, target.position) * speedSteer * Time.deltaTime;
    }
}
