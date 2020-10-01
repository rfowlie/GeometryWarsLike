using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{
    public float speed = 3f;

    private void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.position += ((Vector3.forward * v) + (Vector3.right * h)) * speed * Time.deltaTime;
    }
}
