using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [Range(0.01f, 1f)] public float lerpPercentage = 0.1f;
    public float offset = 1f;
            
    
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + target.up * offset, lerpPercentage);
        transform.rotation = target.rotation;
    }
}
