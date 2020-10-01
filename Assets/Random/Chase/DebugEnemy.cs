using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Thrusting thrust;
    private SteerToTarget sT;
    private SteerFromTarget sF;
    // Start is called before the first frame update
    void Start()
    {
        thrust = new Thrusting(transform, Space.RELATIVE, Vector3.forward, 1f);
        sT = new SteerToTarget(target, transform, 50f);
        sF = new SteerFromTarget(transform, target, 50f);
    }

    // Update is called once per frame
    void Update()
    {
        thrust.Execute();
        sT.Execute();
    }
}
