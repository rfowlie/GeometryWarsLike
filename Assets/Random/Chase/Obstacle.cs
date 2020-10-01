using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float effectRadius = 2f;
    public float effectStrength = 2f;

    public float EffectAmount(float distance)
    {
        return effectRadius / Mathf.Pow(distance, effectStrength);
    }
}
