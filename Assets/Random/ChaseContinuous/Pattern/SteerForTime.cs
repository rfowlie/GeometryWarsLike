using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerForTime : Pattern
{
    //CONSTRUCTOR
    //takes in a literal rotation amount
    public SteerForTime(Transform actor, float pitchTarget, float yawTarget, float pitchSpeed, float yawSpeed)
    {
        this.actor = actor;
        this.pitchTarget = pitchTarget;
        this.yawTarget = yawTarget;
        this.pitchDirection = pitchTarget == 0f ? 0 : Mathf.RoundToInt(pitchTarget / Mathf.Abs(pitchTarget));
        this.yawDirection = yawTarget == 0f ? 0 : Mathf.RoundToInt(yawTarget / Mathf.Abs(yawTarget));

        this.pitchSpeed = pitchSpeed;
        this.yawSpeed = yawSpeed;
    }

    //take in world position and calc rotation amount
    public SteerForTime(Transform actor, Vector3 targetPoint, float pitchSpeed, float yawSpeed)
    {
        //get local position from actor
        this.actor = actor;
        Vector3 local = actor.InverseTransformPoint(targetPoint);
        pitchTarget = local.y == 0 ? 0 : Mathf.Acos(Vector3.Dot(Vector3.up, new Vector3(0, local.y, 0).normalized)) * Mathf.Rad2Deg;
        yawTarget = (local.x == 0 && local.z == 0) ? 0 : Mathf.Acos(Vector3.Dot(Vector3.forward, new Vector3(local.x, 0, local.z).normalized)) * Mathf.Rad2Deg;
        pitchDirection = local.y == 0 ? 0 : Mathf.RoundToInt(local.y / Mathf.Abs(local.y));
        yawDirection = local.x == 0 ? Random.Range(0, 1) * 2 - 1 : Mathf.RoundToInt(local.x / Mathf.Abs(local.x));

        this.pitchSpeed = pitchSpeed;
        this.yawSpeed = yawSpeed;
    }

    //VARIABLES
    Transform actor;
    float pitchTarget = 0f;
    float yawTarget = 0f;
    int pitchDirection = 0;
    int yawDirection = 0;
    float pitchSpeed = 0f;
    float yawSpeed = 0f;
    float pitchOn = 0f;
    float yawOn = 0f;
    float pitchTotal = 0f;
    float yawTotal = 0f;


    //METHODS
    public override void Calculate()
    {
        pitchOn = pitchTotal < pitchTarget ? 1 : 0;
        yawOn = yawTotal < yawTarget ? 1 : 0;        
    }
    public override void Execute()
    {
        float pitch = pitchOn * pitchSpeed * Time.fixedDeltaTime;
        float yaw = yawOn * yawSpeed * Time.fixedDeltaTime;
        pitchTotal += pitch;
        yawTotal += yaw;

        //because the direction can be +/- add it here so not to mess up the TOTAL calculation
        Vector3 rot = new Vector3(-pitch * pitchDirection, yaw * yawDirection, 0f);
        actor.eulerAngles += rot;
    }

    //setup 
    public override void Enter()
    {
        pitchTotal = 0f;
        yawTotal = 0f;
        Calculate();
    }
    public override bool Finished()
    {
        return (pitchOn == 0 && yawOn == 0);
    }
}
