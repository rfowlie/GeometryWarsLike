using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsEX
{
    //y = Vit - 1/2gt^2
    public static float Jump(float jumpTime, float jumpForce, float gravity)
    {
        return (jumpForce * jumpTime) - 0.5f * gravity * (jumpTime * jumpTime);
    }
}

