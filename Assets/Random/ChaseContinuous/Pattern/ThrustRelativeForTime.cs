using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Moves in forward or backwards direction for a set time
public class ThrustRelativeForTime : Pattern
{
    public ThrustRelativeForTime(Transform actor, float moveTime, float speed, bool inverse = false)
    {
        this.actor = actor;
        this.moveTime = moveTime;
        this.speed = speed;
        direction = inverse ? -1 : 1;
    }

    Transform actor;
    int direction;
    float moveTime;
    float speed;
    private float count = 0;


    public override void Calculate()
    {
        count += Time.deltaTime;
    }
    public override void Execute()
    {
        actor.transform.position += actor.forward * direction * speed * Time.fixedDeltaTime;
    }    
    public override void Enter()
    {
        count = 0f;
    }
     public override bool Finished()
    {
        //Debug.Log("Finished: " + (count > moveTime));
        return (count > moveTime);
    }
}






public abstract class P
{
    protected delegate void del();
    protected del action;

    //return a value instead???
    public virtual void Execute()
    {
        action();
    }
}

public enum Space { WORLD, RELATIVE }
//simple move in a relative direction at a specified speed
public class Thrusting : P
{
    public Thrusting(Transform actor, Space space, Vector3 direction, float speed)
    {
        if(space == Space.RELATIVE)
        {
            action = () => actor.position += actor.TransformDirection(direction) * speed * Time.deltaTime;
        }
        else if(space == Space.WORLD)
        {
            action = () => actor.position += direction * speed * Time.deltaTime;
        }
    }
}

public class SteerToTarget : P
{
    public SteerToTarget(Transform actor, Transform target, float speed)
    {
        action = () =>
        {
            Vector3 rotation = CalcRotationDirection(actor, target);
            actor.localEulerAngles += rotation * speed * Time.deltaTime;
        };        
    }

    private float steeringThreshold = 0.99f;

    public Vector3 CalcRotationDirection(Transform actor, Transform target)
    {
        //convert target to local space
        Vector3 targetLocal = actor.InverseTransformPoint(target.position).normalized;
        Vector3 targetLocal2 = (target.position - actor.position).normalized;
        Debug.Log("Local: " + targetLocal + " Local2: " + targetLocal2);
        
        //determine if rotation in pitch or yaw access needed to face target straight on, as well as which way to rotate
        float dotPitch = targetLocal.y == 0 ? 1 : Vector3.Dot(Vector3.up, new Vector3(0, targetLocal.y, 0));
        float dotYaw = targetLocal.x == 0 ? 1 : Vector3.Dot(Vector3.right, new Vector3(targetLocal.x, 0, 0));
        float pitch = dotPitch > steeringThreshold ? 0 : targetLocal.y / Mathf.Abs(targetLocal.y);
        float yaw = dotYaw > steeringThreshold ? 0 : targetLocal.x / Mathf.Abs(targetLocal.x);
                
        return new Vector3(-pitch, yaw, 0);
    }
}

public class SteerFromTarget : P
{
    public SteerFromTarget(Transform actor, Transform target, float speed)
    {
        action = () =>
        {
            Vector3 rotation = CalcRotationDirection(actor, target);
            actor.localEulerAngles += rotation * speed * Time.deltaTime;
        };
    }

    private float steeringThreshold = 0.99f;

    public Vector3 CalcRotationDirection(Transform actor, Transform target)
    {
        //convert target to local space
        Vector3 targetLocal = actor.InverseTransformPoint(target.position).normalized;
        targetLocal *= -1f;
        //determine if rotation in pitch or yaw access needed to face target straight on, as well as which way to rotate
        float dotPitch = targetLocal.y == 0 ? 1 : Vector3.Dot(Vector3.up, new Vector3(0, targetLocal.y, 0));
        float dotYaw = targetLocal.x == 0 ? 1 : Vector3.Dot(Vector3.right, new Vector3(targetLocal.x, 0, 0));
        float pitch = dotPitch > steeringThreshold ? 0 : targetLocal.y / Mathf.Abs(targetLocal.y);
        float yaw = dotYaw > steeringThreshold ? 0 : targetLocal.x / Mathf.Abs(targetLocal.x);

        return new Vector3(-pitch, yaw, 0);
    }
}