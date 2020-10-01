using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//formulas for calculating rotation in X/Y space
//avoids gimbol lock...
public static class Steer
{
    public static Vector3 TowardTarget(Transform actor, Vector3 target, float steeringThreshold = 0.99f)
    {
        //convert target to local space
        Vector3 targetLocal = actor.InverseTransformPoint(target).normalized;

        //determine if rotation in pitch or yaw access needed to face target straight on, as well as which way to rotate
        float dotPitch = targetLocal.y == 0 ? 1 : Vector3.Dot(Vector3.up, new Vector3(0, targetLocal.y, 0));
        float dotYaw = targetLocal.x == 0 ? 1 : Vector3.Dot(Vector3.right, new Vector3(targetLocal.x, 0, 0));
        float pitch = dotPitch > steeringThreshold ? 0 : targetLocal.y / Mathf.Abs(targetLocal.y);
        float yaw = dotYaw > steeringThreshold ? 0 : targetLocal.x / Mathf.Abs(targetLocal.x);

        return new Vector3(-pitch, yaw, 0);
    }

    public static Vector3 AwayFromTarget(Transform actor, Vector3 target, float steeringThreshold = 0.99f)
    {
        //convert target to local space
        Vector3 targetLocal = actor.InverseTransformPoint(target).normalized;
        targetLocal *= -1f;

        //determine if rotation in pitch or yaw access needed to face target straight on, as well as which way to rotate
        float dotPitch = targetLocal.y == 0 ? 1 : Vector3.Dot(Vector3.up, new Vector3(0, targetLocal.y, 0));
        float dotYaw = targetLocal.x == 0 ? 1 : Vector3.Dot(Vector3.right, new Vector3(targetLocal.x, 0, 0));
        float pitch = dotPitch > steeringThreshold ? 0 : targetLocal.y / Mathf.Abs(targetLocal.y);
        float yaw = dotYaw > steeringThreshold ? 0 : targetLocal.x / Mathf.Abs(targetLocal.x);

        return new Vector3(-pitch, yaw, 0);
    }
}
