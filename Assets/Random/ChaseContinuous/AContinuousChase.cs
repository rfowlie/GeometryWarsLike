using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AContinuousChase : MonoBehaviour
{
    public GridOLD grid;
    public Transform target;

    //pushes the object in its forward direction
    public float speedThrust = 1f;
    public float speedSteering = 1f;
    [Range(-1f, 1f)]
    public float steeringThreshold = 0.95f;
    public float drag = 2f;
    private float strengthThrust;
    private float strengthSteering;
    private Vector3 rotation = Vector3.zero;

    private Vector3Int destination;

    private void Start()
    {
        //destination = grid.TileFromPoint(target.position);
    }

    private void Update()
    {
        rotation = CalcRotationDirection(target);

        //convert target to local space
        Vector3 targetLocal = transform.InverseTransformPoint(target.position).normalized;
        //get the strength of forward movement
        strengthThrust = (Vector3.Dot(Vector3.forward, targetLocal) + 1) * 0.5f;
        //rotation adjustments can be whatever you want
        strengthSteering = 1 - Mathf.Pow(strengthThrust, drag);
    }

    public Vector3 CalcRotationDirection(Transform target)
    {
        //convert target to local space
        Vector3 targetLocal = transform.InverseTransformPoint(target.position).normalized;
        //determine if rotation in pitch or yaw access needed to face target straight on, as well as which way to rotate
        float dotPitch = targetLocal.y == 0 ? 1 : Vector3.Dot(Vector3.up, new Vector3(0, targetLocal.y, 0));
        float dotYaw = targetLocal.x == 0 ? 1 : Vector3.Dot(Vector3.right, new Vector3(targetLocal.x, 0, 0));
        float pitch = dotPitch > steeringThreshold ? 0 : targetLocal.y / Mathf.Abs(targetLocal.y);
        float yaw = dotYaw > steeringThreshold ? 0 : targetLocal.x / Mathf.Abs(targetLocal.x);

        return new Vector3(-pitch, yaw, 0);
    }

    private void FixedUpdate()
    {        
        //rotation
        if(rotation != Vector3.zero)
        {
            transform.eulerAngles += rotation * speedSteering * strengthSteering * Time.fixedDeltaTime; 
        }

        //movement
        transform.position += transform.forward * speedThrust * strengthThrust * Time.fixedDeltaTime;
    }
}
